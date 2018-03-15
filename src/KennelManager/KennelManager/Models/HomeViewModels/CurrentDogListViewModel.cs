using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KennelManager.Models.HomeViewModels
{
    public class CurrentDogListViewModel
    {
        public List<Dog> CurrentDogs { get; set; }
        public List<DogsByLocation> LocationList { get; set; }
    }
}
