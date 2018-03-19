using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using KennelManager.Models;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IHostingEnvironment _hostEnv;


        public DogManagerController(IDogRepository dogRepo, IBreedRepository breedRepo, ILogger<DogManagerController> logger, IHostingEnvironment hostEnv)
            
            
        {
            _dogRepo = dogRepo;
            _breedRepo = breedRepo;
            _logger = logger;
            _hostEnv = hostEnv;
        }

        // GET: DogManager
        public ActionResult Index()
        {
            return View(_dogRepo.GetDogList(" "));
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
            dogVM.Breeds = new SelectList(_breedRepo.GetBreedList(),"Name", "Name");
            dogVM.ThisDog = new Dog();
            return View(dogVM);
        }

        // POST: DogManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DogViewModel newDog, IFormFile pic, IFormCollection collection)
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
                newDog.ThisDog.CurrentStatus = newStatus.DogStatus;

                newDog.ThisDog.Images = new List<Image>();
                if (pic != null)
                {
                    Image newImage = new Image();
                    var filename = Path.Combine(_hostEnv.WebRootPath, "images", Path.GetFileName(pic.FileName));
                    //pic.CopyTo(new FileStream(filename, FileMode.Create));

                    using (var fileStream = new FileStream(filename, FileMode.Create))
                    {
                        pic.CopyTo(fileStream);
                    }
                    newImage.Name = pic.FileName;
                    newImage.DogId = newDog.ThisDog.Id;
                    //Image noImage = newDog.ThisDog.Images.Find(p => p.Name == "noPhoto.jpg");
                    //if (noImage != null)
                    //{
                    //    newDog.ThisDog.Images.Remove(noImage);
                    //}

                    newDog.ThisDog.Images.Add(newImage);
                }


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
            dogVM.Breeds = new SelectList(_breedRepo.GetBreedList(), "Name", "Name");
            if (dogVM.ThisDog.Images.Count == 0)
            {
                //Image newImage = new Image()
                //{
                //    Name = "noPhoto.jpg",
                //    DogId = dogVM.ThisDog.Id
                //};

                //dogVM.ThisDog.Images.Add(newImage);
               

            }


            return View(dogVM);
        }

        // POST: DogManager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DogViewModel editDog, IFormFile pic,  IFormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(editDog);
                }

                // Remove deleted colors and set DogId
                foreach (Color clr in editDog.ThisDog.Colors.ToList())
                {
                    //if (clr.Name == "" || clr.Name == null)
                    //{
                    //    editDog.ThisDog.Colors.Remove(clr);
                    //}
                    if (clr.DogId == 0)
                    {
                        clr.DogId = editDog.ThisDog.Id;
                    }
                }

                // add status to array
                editDog.ThisDog.Statuses = new List<Status>();
                string lastStatus = _dogRepo.GetDogById(editDog.ThisDog.Id).CurrentStatus;
              
                if (editDog.currentStatus.DogStatus != lastStatus )
                {
                    editDog.ThisDog.Statuses.Add(editDog.currentStatus);
                }

                editDog.ThisDog.CurrentStatus = editDog.currentStatus.DogStatus;




                if (pic != null)
                {
                    Image newImage = new Image();
                    var filename = Path.Combine(_hostEnv.WebRootPath, "images", Path.GetFileName(pic.FileName));

                    using(var fileStream = new FileStream(filename, FileMode.Create))
                    {
                        pic.CopyTo(fileStream);
                    }

                  //  pic.CopyTo(new FileStream(filename, FileMode.Create));
                    newImage.Name = pic.FileName;
                    newImage.DogId = editDog.ThisDog.Id;
                    //Image noImage = new Image();
                    //noImage=editDog.ThisDog.Images.Find(p => p.Name == "noPhoto.jpg");
                    //if (noImage != null)
                    //{
                    //    editDog.ThisDog.Images.Remove(noImage);
                    //}
                    if (editDog.ThisDog.Images == null)
                    {
                        editDog.ThisDog.Images = new List<Image>();
                    }
                    editDog.ThisDog.Images.Add(newImage);
                }

                _dogRepo.Edit(editDog.ThisDog);


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
            return View(_dogRepo.GetDogById(id));
        }

        // POST: DogManager/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Dog deleteDog, IFormCollection collection)
        {
            try
            {
                _dogRepo.Delete(deleteDog);

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