using ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KennelManager.Models
{
    public class DogViewModel
    {
        public Dog ThisDog { get; set; }
        public SelectList Breeds { get; set; }

    }
}
