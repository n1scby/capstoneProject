using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using KennelManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace KennelManager.Controllers
{
    public class DogManagerController : Controller
    {
        private readonly IDogRepository _dogRepo;
        private readonly IBreedRepository _breedRepo;
        private readonly ILogger _logger;


        public DogManagerController(IDogRepository dogRepo, IBreedRepository breedRepo, ILogger<DogManagerController> logger)
            
            
        {
            _dogRepo = dogRepo;
            _breedRepo = breedRepo;
            _logger = logger;
        }

        // GET: DogManager
        public ActionResult Index()
        {
            return View(_dogRepo.GetDogList());
        }

        // GET: DogManager/Details/5
        public ActionResult Details(int id)
        {
            return View(_dogRepo.GetDogById(id));
        }

        // GET: DogManager/Create
        public ActionResult Create()
        {
            DogViewModel dogVM = new DogViewModel();
            dogVM.Breeds = new SelectList(_breedRepo.GetBreedList(),"Id", "Name");
            dogVM.ThisDog = new Dog();
            return View(dogVM);
        }

        // POST: DogManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DogViewModel newDog, IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(newDog);
                }
                
                    // Remove deleted colors and set DogId
                    foreach (Color clr in newDog.ThisDog.Colors.ToList())
                    {
                        if (clr.Name == "" || clr.Name == null)
                        {
                            newDog.ThisDog.Colors.Remove(clr);
                        }
                       
                    }

                Status newStatus = new Status()
                {
                    DogStatus = "Arrival",
                    Date = newDog.currentStatus.Date
                };

                newDog.ThisDog.Statuses = new List<Status>();
                newDog.ThisDog.Statuses.Add(newStatus);


                    _dogRepo.Add(newDog.ThisDog);


                
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError("Error on Create: " + ex.Message);
                return View(newDog);
            }
        }

        // GET: DogManager/Edit/5
        public ActionResult Edit(int id)
        {
            DogViewModel dogVM = new DogViewModel();
            dogVM.ThisDog = _dogRepo.GetDogById(id);
            if (dogVM.ThisDog.Statuses.Count > 0)
            {
                dogVM.currentStatus = dogVM.ThisDog.Statuses.Last();
            } else
            {
                dogVM.currentStatus = new Status();
                dogVM.currentStatus.Date = DateTime.Now;
                dogVM.currentStatus.DogStatus = "";
                dogVM.currentStatus.DogId = dogVM.ThisDog.Id;
                dogVM.currentStatus.Id = 0;
             }
            dogVM.Breeds = new SelectList(_breedRepo.GetBreedList(), "Id", "Name");
            if (dogVM.ThisDog.Images.Count == 0)
            {
                Image newImage = new Image()
                {
                    Name = "noPhoto.jpg",
                    DogId = dogVM.ThisDog.Id
                };

                dogVM.ThisDog.Images.Add(newImage);
               

            }


            return View(dogVM);
        }

        // POST: DogManager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DogViewModel editDog, IFormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(editDog);
                }
                // TODO: Add update logic here
                // Remove deleted colors and set DogId
                foreach (Color clr in editDog.ThisDog.Colors.ToList())
                {
                    if (clr.Name == "" || clr.Name == null)
                    {
                        editDog.ThisDog.Colors.Remove(clr);
                    }
                    if (clr.DogId == 0)
                    {
                        clr.DogId = editDog.ThisDog.Id;
                    }
                }


                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError("Error on Update: " + ex.Message);

                return View(editDog);
            }
        }

        // GET: DogManager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DogManager/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError("Error on Delete: " + ex.Message);

                return View();
            }
        }
    }
}