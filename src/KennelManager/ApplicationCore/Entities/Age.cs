using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Age
    {
        [Display(Name = "Age")]
        public int Number { get; set; }
        public string UOM { get; set; }
    }
}
