using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class DogsByLocation
    {
        public string Location { get; set; }
        public List<Dog> DogList { get; set; }
    }
}
