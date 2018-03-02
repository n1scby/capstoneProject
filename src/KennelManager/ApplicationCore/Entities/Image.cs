using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public int DogId { get; set; }
        public string Name { get; set; }
    }
}
