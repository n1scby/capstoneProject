using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Status
    {
        public int Id { get; set; }
        public int DogId { get; set; }
        public string DogStatus { get; set; }
        public DateTime Date { get; set; }

    }
}
