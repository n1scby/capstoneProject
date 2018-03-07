﻿using System;
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
           // dogVM.ThisDog = new Dog();
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
                    // Remove deleted colors and set DogId
                    foreach (Color clr in newDog.ThisDog.Colors.ToList())
                    {
                        if (clr.Name == "" || clr.Name == null)
                        {
                            newDog.ThisDog.Colors.Remove(clr);
                        }
                        if (clr.DogId == 0)
                        {
                            clr.DogId = newDog.ThisDog.Id;
                        }
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
            DogViewModel dogVM = new DogViewModel();
            dogVM.Breeds = new SelectList(_breedRepo.GetBreedList(), "Id", "Name");


            return View();
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
            catch
            {
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
            catch
            {
                return View();
            }
        }
    }
}