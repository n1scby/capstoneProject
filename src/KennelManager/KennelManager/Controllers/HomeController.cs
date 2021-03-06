﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KennelManager.Models;
using ApplicationCore.Interfaces;
using Microsoft.Extensions.Logging;
using ApplicationCore.Entities;
using KennelManager.Models.HomeViewModels;

namespace KennelManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDogRepository _dogRepo;
        private readonly ILogger _logger;

        public HomeController(IDogRepository dogRepo, ILogger<HomeController> logger)
        {
            _dogRepo = dogRepo;
            _logger = logger;
        }

        public IActionResult Index()
        {
            CurrentDogListViewModel currentDogListVM = new CurrentDogListViewModel();
            currentDogListVM.CurrentDogs = _dogRepo.GetDogList("Adopted");
            currentDogListVM.LocationList = new List<DogsByLocation>();

            var results = currentDogListVM.CurrentDogs.GroupBy(
            d => d.LocationId,
            d => d.Id, 
            (key, g) => new { LocationId = key, Dogs = g.ToList() })
            .OrderBy(d => d.LocationId);   

            foreach (var loc in results)
            {
                DogsByLocation dbl = new DogsByLocation();
                dbl.DogList = new List<Dog>();
                dbl.Location = loc.LocationId;
                foreach (int id in loc.Dogs)
                {
                    dbl.DogList.Add(_dogRepo.GetDogById(id));
                }
                currentDogListVM.LocationList.Add(dbl);
            }

            return View(currentDogListVM);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "My contact page.";

            return View();
        }

        public IActionResult DisplayDog(int id)
        {
                       
            return View(_dogRepo.GetDogById(id));
        }

        public IActionResult DogStatusHistory(int id)
        {
            ViewData["DogName"] = _dogRepo.GetDogById(id).Name;
            return View(_dogRepo.GetStatusesByDogId(id));
        }

        public IActionResult MixedPureBreed()
        {
            List<Dog> currentDogList = _dogRepo.GetDogList("Adopted");   // all dogs except those adopted
            int mixedCount = 0;
            int pureCount = 0;

            foreach(Dog dog in currentDogList)
            {
                if (dog.MixedBreed)
                {
                    mixedCount++;
                }
                else
                {
                    pureCount++;
                }
            }
            int totalCount = pureCount + mixedCount;
            MixedPureBreedViewModel mixedPureVM = new MixedPureBreedViewModel();
            if (totalCount > 0)
            {
                mixedPureVM.MixedPercent = (float)Math.Round(((float)mixedCount / (float)totalCount) * 100, 2);
                mixedPureVM.PurePercent = (float)Math.Round(((float)pureCount / totalCount) * 100, 2);
            }


            return View(mixedPureVM);
        }



        public IActionResult DogsByBreed()
        {
            List<BreedCount> breedCountList = new List<BreedCount>();
            List<Dog> currentDogList = _dogRepo.GetDogList("Adopted");

            foreach(Dog dog in currentDogList)
            {
                if (dog.PrimaryBreed == dog.SecondaryBreed)
                {
                    dog.SecondaryBreed = "";  //prevent from counting the breed twice
                }
            }

            // group by primary breed and get count
            var countList = currentDogList.GroupBy(i => i.PrimaryBreed);
            foreach (var grp in countList)
            {
                if (grp.Key != "")
                {
                    BreedCount bc = new BreedCount();
                    bc.BreedName = grp.Key;
                    bc.Count = grp.Count();
                    breedCountList.Add(bc);
                }
            }

            // group by secondary breed and get count
            var countList2 = currentDogList.GroupBy(i => i.SecondaryBreed);
            foreach (var grp2 in countList2)
            {
                if (grp2.Key != "")
                {
                    BreedCount bc = new BreedCount();
                    bc.BreedName = grp2.Key;
                    bc.Count = grp2.Count();

                    // look for breed in the list
                    BreedCount findBreedCount = breedCountList.Find(x => x.BreedName == bc.BreedName);
                    if (findBreedCount != null)  // if breed is found add to count
                    {
                        findBreedCount.Count += bc.Count;
                    }
                    else  // not found, then add to list
                    {
                        breedCountList.Add(bc);
                    }
                }

            }

            BreedCount newBC = new BreedCount();
            newBC.BreedName = "* Other *";
            foreach (BreedCount brdCnt in breedCountList.ToList())
            {
                if (brdCnt.Count == 1)
                {
                    newBC.Count += 1;
                 //   breedCountList.Remove(brdCnt);
                }

            }
            if (newBC.Count > 0)
            {
                breedCountList.Add(newBC);
            }

            return View(breedCountList.OrderBy(x => x.BreedName));   //order by breedname
        }



        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
