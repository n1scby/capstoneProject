using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IDogRepository
    {
        void Add(Dog newDog);
        void Delete(Dog deleteDog);
        Dog GetDogById(int id);
        void Edit(Dog updatedDog);
        List<Dog> GetDogList(string status);

    }
}
