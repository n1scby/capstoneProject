﻿using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IBreedRepository
    {
        void Add(Breed newBreed);
        void Delete(Breed deleteBreed);
        Dog GetBreedById(int id);
        void Edit(Breed updatedBreed);
        List<Dog> GetBreedList();

    }
}
