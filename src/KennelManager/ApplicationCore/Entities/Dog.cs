using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CurrentStatus { get; set; }
        public List<Status> Statuses { get; set; }
        public string Gender { get; set; }
        public string Altered { get; set; }
        public Age DogAge { get; set; }
        public double Weight { get; set; }

        [Display(Name = "Location")]
        public string LocationId { get; set; }
        public List<Image> Images { get; set; }

        [Display(Name = "Mixed Breed")]
        public bool MixedBreed { get; set; }

        [Display(Name = "Primary Breed")]
        public string PrimaryBreed { get; set; }

        [Display(Name = "Secondary Breed")]
        public string SecondaryBreed { get; set; }

        public string Description { get; set; }
        public List<Color> Colors { get; set; }

        
    }
}
