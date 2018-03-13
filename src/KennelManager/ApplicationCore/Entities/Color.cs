using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Color
    {
        public int Id { get; set; }
        public int DogId { get; set; }

        [Display(Name="Color")]
        public string Name { get; set; }
    }
}
