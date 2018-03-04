using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class BreedRepository : IBreedRepository
    {
        private string _connectionString;
        private string selectBreedQuery = "SELECT Id, Breed FROM DogBreed \n";
        private string byId = "WHERE Id = @id";
        private string deleteBreedQuery = "DELETE DogBreed \n";
        private string updateBreedQuery = "UPDATE DogBreed SET Breed = @breed\n";
        private string insertBreedQuery = "INSERT into DogBreed (Breed) values(@breed)";

        public BreedRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Breed newBreed)
        {
            throw new NotImplementedException();
        }

        public void Delete(Breed deleteBreed)
        {
            throw new NotImplementedException();
        }

        public void Edit(Breed updatedBreed)
        {
            throw new NotImplementedException();
        }

        public Dog GetBreedById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Dog> GetBreedList()
        {
            throw new NotImplementedException();
        }
    }
}
