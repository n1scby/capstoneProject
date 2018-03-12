using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Infrastructure
{
    public class BreedRepository : IBreedRepository
    {
        private string _connectionString;
        private string selectBreedQuery = "SELECT Id, Breed FROM DogBreed \n";
        private string byId = "WHERE Id = @id";
        private string deleteBreedQuery = "DELETE DogBreed \n";
        private string updateBreedQuery = "UPDATE DogBreed SET Breed = @breed \n";
        private string insertBreedQuery = "INSERT into DogBreed (Breed) values(@breed)";

        public BreedRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Breed newBreed)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {

                SqlCommand cmd = new SqlCommand(insertBreedQuery, conn);
                cmd.Parameters.AddWithValue("@breed", newBreed.Name);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }

            }
        }


        public void Delete(Breed deleteBreed)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {

                SqlCommand cmd = new SqlCommand(deleteBreedQuery + byId, conn);
                cmd.Parameters.AddWithValue("@id", deleteBreed.Id);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }

            }
        }

        public void Edit(Breed updatedBreed)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(updateBreedQuery + byId, conn);
                cmd.Parameters.AddWithValue("@breed", updatedBreed.Name);
                
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }

            }
        }

        public Breed GetBreedById(int id)
        {
            Breed breed = new Breed();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {

                SqlCommand cmd = new SqlCommand(selectBreedQuery + byId, conn);
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        breed = new Breed()
                        {
                            Id = int.Parse(reader[0].ToString()),
                            Name = reader[1].ToString(),
                            
                        };


                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
            return breed;

        }

        public List<Breed> GetBreedList()
        {
            List<Breed> breeds = new List<Breed>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {

                SqlCommand cmd = new SqlCommand(selectBreedQuery, conn);
 
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Breed breed = new Breed()
                        {
                            Id = int.Parse(reader[0].ToString()),
                            Name = reader[1].ToString(),

                        };

                        breeds.Add(breed);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
            return breeds;


        }
    }
    
}
