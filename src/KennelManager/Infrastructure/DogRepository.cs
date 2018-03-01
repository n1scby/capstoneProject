using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class DogRepository : IDogRepository
    {
        private string _connectionString;
        private string selectQuery = "SELECT Id, FROM Dog \n";
        private string byId = "WHERE Id = @id";
        private string deleteQuery = "DELETE Dog \n";
        private string updateQuery = "UPDATE Dog SET \n";
        private string insertQuery = "INSERT into Dog () values()";


        public void Add(Dog newDog)
        {
            throw new NotImplementedException();
        }

        public void Delete(Dog deleteDog)
        {
            throw new NotImplementedException();
        }

        public void Edit(Dog updatedDog)
        {
            throw new NotImplementedException();
        }

        public Dog GetDogById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Dog> GetDogList()
        {
            throw new NotImplementedException();
        }
    }
}
