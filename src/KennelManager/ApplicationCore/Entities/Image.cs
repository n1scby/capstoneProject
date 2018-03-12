using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public int DogId { get; set; }

        [Display(Name = "Image Name")]
        public string Name { get; set; }
    }
}
