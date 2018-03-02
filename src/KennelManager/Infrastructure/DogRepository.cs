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
        private string selectDogQuery = "SELECT Id, Name, Gender, Altered, Age, AgeUOM, Weight, LocationId, MixedBreed, Description FROM Dog \n";
        private string byId = "WHERE Id = @id";
        private string deleteDogQuery = "DELETE Dog \n";
        private string updateDogQuery = "UPDATE Dog SET Name = @name, Gender = @gender, Altered = @altered, Age = @age, AgeUOM = @ageUOM, Weight = @weight, LocationId = @locationId, MixedBreed = @mixedBreed, PrimaryBreed = @primaryBreed, SecondaryBreed = @secondaryBreed, Description = @description\n";
        private string insertDogQuery = "INSERT into Dog (Name, Gender, Altered, Age, AgeUOM, Weight, LocationId, MixedBreed, PrimaryBreed, SecondaryBreed, Description) values(@name, @gender, @altered, @age, @ageUOM, @weight, @locationId, @mixedBreed, @primaryBreed, @secondaryBreed, @description)";
        private string selectImageQuery = "SELECT Id, DogId, Image from DogImage ";
        private string byDogId = "WHERE DogId = @dogId";
        private string updateImageQuery = "UPDATE DogImage SET Image = @image";
        private string deleteImageQuery = "DELETE DogImage";
        private string insertImageQuery = "Insert into DogImage (DogId, Image) values(@dogId, @image)";

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
