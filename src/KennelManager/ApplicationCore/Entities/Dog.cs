using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Dog
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public List<Status> Statuses { get; set; }
        public string Gender { get; set; }
        public bool Altered { get; set; }
        public Age DogAge { get; set; }
        public double Weight { get; set; }
        public string LocationId { get; set; }
        public List<string> Images { get; set; }
        public bool MixedBeed { get; set; }
        public List<Breed> Breeds { get; set; }
        public string Description { get; set; }
        public List<string> Colors { get; set; }

    }
}
