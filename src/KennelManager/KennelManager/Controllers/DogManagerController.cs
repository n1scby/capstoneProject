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

namespace KennelManager.Controllers
{
    public class DogManagerController : Controller
    {
        private readonly IDogRepository _dogRepo;
        private readonly IBreedRepository _breedRepo;


        public DogManagerController(IDogRepository dogRepo, IBreedRepository breedRepo)
        {
            _dogRepo = dogRepo;
            _breedRepo = breedRepo;
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
                if (ModelState.IsValid)
                {
                    foreach (Color clr in newDog.ThisDog.Colors)
                    {
                        Console.WriteLine(clr.Name);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DogManager/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DogManager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
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
            catch
            {
                return View();
            }
        }
    }
}