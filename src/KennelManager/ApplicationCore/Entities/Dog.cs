using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Status> Statuses { get; set; }
        public string Gender { get; set; }
        public string Altered { get; set; }
        public Age DogAge { get; set; }
        public double Weight { get; set; }
        public string LocationId { get; set; }
        public List<Image> Images { get; set; }
        public bool MixedBeed { get; set; }
        public string PrimaryBreed { get; set; }
        public string SecondaryBreed { get; set; }
        public string Description { get; set; }
        public List<Color> Colors { get; set; }

    }
}
