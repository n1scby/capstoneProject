using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Infrastructure
{
    public class DogRepository : IDogRepository
    {
        private string _connectionString;
        private string selectDogQuery = "SELECT Id, Name, Gender, Altered, Age, AgeUOM, Weight, LocationId, MixedBreed, PrimaryBreed, SecondaryBreed, Description FROM Dog \n";
        private string byId = "WHERE Id = @id";
        private string deleteDogQuery = "DELETE Dog \n";
        private string updateDogQuery = "UPDATE Dog SET Name = @name, Gender = @gender, Altered = @altered, Age = @age, AgeUOM = @ageUOM, Weight = @weight, LocationId = @locationId, MixedBreed = @mixedBreed, PrimaryBreed = @primaryBreed, SecondaryBreed = @secondaryBreed, Description = @description\n";
        private string insertDogQuery = "INSERT into Dog (Name, Gender, Altered, Age, AgeUOM, Weight, LocationId, MixedBreed, PrimaryBreed, SecondaryBreed, Description) values(@name, @gender, @altered, @age, @ageUOM, @weight, @locationId, @mixedBreed, @primaryBreed, @secondaryBreed, @description);" +
                                        " SELECT CAST(scope_identity() AS int)";

        private string selectImageQuery = "SELECT Id, DogId, Image from DogImage ";
        private string updateImageQuery = "UPDATE DogImage SET Image = @image";
        private string deleteImageQuery = "DELETE DogImage";
        private string insertImageQuery = "Insert into DogImage (DogId, Image) values(@dogId, @image)";

        private string byDogId = "WHERE DogId = @dogId";

        private string selectStatusQuery = "SELECT Id, DogId, Status, StatusDate from DogStatus ";
        private string updateStatusQuery = "UPDATE DogStatus SET Status = @status";
        private string deleteStatusQuery = "DELETE DogStatus";
        private string insertStatusQuery = "Insert into DogStatus (DogId, Status, StatusDate) values(@dogId, @status, @statusDate)";

        private string selectColorQuery = "SELECT Id, DogId, Color from DogColor ";
        private string updateColorQuery = "UPDATE DogColor SET Color = @color";
        private string deleteColorQuery = "DELETE DogColor";
        private string insertColorQuery = "Insert into DogColor (DogId, Color) values(@dogId, @color)";

        public DogRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        public void Add(Dog newDog)
        {
            int newId = 0;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(insertDogQuery, conn);
                cmd.Parameters.AddWithValue("@name", newDog.Name);
                cmd.Parameters.AddWithValue("@gender", newDog.Gender);
                cmd.Parameters.AddWithValue("@altered", newDog.Altered);
                cmd.Parameters.AddWithValue("@age", newDog.DogAge.Number);
                cmd.Parameters.AddWithValue("@ageUOM", newDog.DogAge.UOM);
                cmd.Parameters.AddWithValue("@locationId", newDog.LocationId ?? "");
                cmd.Parameters.AddWithValue("@weight", newDog.Weight);
                cmd.Parameters.AddWithValue("@mixedBreed", newDog.MixedBeed);
                cmd.Parameters.AddWithValue("@primaryBreed", newDog.PrimaryBreed);
                cmd.Parameters.AddWithValue("@secondaryBreed", newDog.SecondaryBreed ?? "");
                cmd.Parameters.AddWithValue("@description", newDog.Description ?? "");

                try
                {
                    conn.Open();
                    newId = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw ex;
                }

            }

            if (newId == 0)
            {
                return;
            }
            // add colors
            foreach (Color clr in newDog.Colors)
            {
                AddColor(clr, newId);
            }

            //add status
            foreach (Status sts in newDog.Statuses)
            {
                AddStatus(sts, newId);
            }

            // add images
            foreach (Image img in newDog.Images)
            {
                AddImage(img, newId);
            }

        }

        public void Delete(Dog deleteDog)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(deleteDogQuery + byId, conn);
                cmd.Parameters.AddWithValue("@id", deleteDog.Id);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    throw ex;
                }
            }
        }


        public void Edit(Dog updatedDog)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(updateDogQuery + byId, conn);
                cmd.Parameters.AddWithValue("@name", updatedDog.Name);
                cmd.Parameters.AddWithValue("@gender", updatedDog.Gender);
                cmd.Parameters.AddWithValue("@altered", updatedDog.Altered);
                cmd.Parameters.AddWithValue("@age", updatedDog.DogAge.Number);
                cmd.Parameters.AddWithValue("@ageUOM", updatedDog.DogAge.UOM);
                cmd.Parameters.AddWithValue("@locationId", updatedDog.LocationId ?? "");
                cmd.Parameters.AddWithValue("@weight", updatedDog.Weight);
                cmd.Parameters.AddWithValue("@mixedBreed", updatedDog.MixedBeed);
                cmd.Parameters.AddWithValue("@primaryBreed", updatedDog.PrimaryBreed);
                cmd.Parameters.AddWithValue("@secondarBreed", updatedDog.SecondaryBreed);
                cmd.Parameters.AddWithValue("@description", updatedDog.Description ?? "");
                cmd.Parameters.AddWithValue("@id", updatedDog.Id);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw ex;
                }

            }

            foreach(Color clr in updatedDog.Colors)
            {
                UpdateColor(clr, updatedDog.Id);
            }

            foreach (Status sts in updatedDog.Statuses)
            {
                UpdateStatus(sts, updatedDog.Id);
            }

            foreach (Image img in updatedDog.Images)
            {
                UpdateImage(img, updatedDog.Id);
            }

        }

        public Dog GetDogById(int id)
        {
            Dog dog = new Dog();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(selectDogQuery + byId, conn);
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        dog = new Dog()
                        {
                            Id = int.Parse(reader[0].ToString()),
                            Name = reader[1].ToString(),
                            Gender = reader[2].ToString(),
                            //   Altered = (int.Parse(reader[3].ToString()) == 1) ? true : false,
                            Altered = reader[3].ToString(),
                            DogAge = new Age()
                            {
                                Number = int.Parse(reader[4].ToString()),
                                UOM = reader[5].ToString()
                            },
                            LocationId = reader[6].ToString(),
                            Weight = double.Parse(reader[7].ToString()),
                            MixedBeed = (int.Parse(reader[8].ToString()) == 1) ? true : false,
                            PrimaryBreed = reader[9].ToString(),
                            SecondaryBreed = reader[10].ToString(),
                            Description = reader[11].ToString()

                        };

                        dog.Images = GetImagesByDogId(dog.Id);
                        dog.Colors = GetColorsByDogId(dog.Id);
                        dog.Statuses = GetStatusesByDogId(dog.Id);

                    }
                } 
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw ex;
                }

            }
            return dog;
        }

        

        public List<Dog> GetDogList()
        {
            List<Dog> dogs = new List<Dog>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(selectDogQuery, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        Dog dog = new Dog();

                        dog.Id = int.Parse(reader[0].ToString());
                        dog.Name = reader[1].ToString();
                        dog.Gender = reader[2].ToString();
                        //   Altered = (int.Parse(reader[3].ToString()) == 1) ? true : false,
                        dog.Altered = reader[3].ToString();
                        dog.DogAge = new Age()
                        {
                            Number = int.Parse(reader[4].ToString()),
                            UOM = reader[5].ToString()
                        };
                        dog.Weight = double.Parse(reader[6].ToString());
                        dog.LocationId = reader[7].ToString();
                        dog.MixedBeed = reader.GetBoolean(8);
                        dog.PrimaryBreed = reader[9].ToString();
                        dog.SecondaryBreed = reader[10].ToString();
                        dog.Description = reader[11].ToString();

                       

                        dog.Images = GetImagesByDogId(dog.Id);
                        dog.Colors = GetColorsByDogId(dog.Id);
                        dog.Statuses = GetStatusesByDogId(dog.Id);

                        dogs.Add(dog);

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("GetDogList:" + ex.Message);
                 
                }

            }
            return dogs;
        }


        public List<Image> GetImagesByDogId(int dogId)
        {
            List<Image> images = new List<Image>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(selectImageQuery + byDogId, conn);
                cmd.Parameters.AddWithValue("@dogId", dogId);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        Image image = new Image()
                        {
                            Id = int.Parse(reader[0].ToString()),
                            DogId = int.Parse(reader[1].ToString()),
                            Name = reader[2].ToString()                                                      
                        };

                        images.Add(image);
                       

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw new Exception("GetImagesById:" + ex.Message);
                }

            }
            return images;
        }


        public List<Status> GetStatusesByDogId(int dogId)
        {
            List<Status> statuses = new List<Status>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(selectStatusQuery + byDogId, conn);
                cmd.Parameters.AddWithValue("@dogId", dogId);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        Status status = new Status()
                        {
                            Id = int.Parse(reader[0].ToString()),
                            DogId = int.Parse(reader[1].ToString()),
                            DogStatus = reader[2].ToString(),
                            Date = DateTime.Parse(reader[3].ToString())
                        };

                        statuses.Add(status);


                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("GetStatusesById:" + ex.Message);
                    throw;
                }

            }
            return statuses;
        }

        public List<Color> GetColorsByDogId(int dogId)
        {
            List<Color> colors = new List<Color>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(selectColorQuery + byDogId, conn);
                cmd.Parameters.AddWithValue("@dogId", dogId);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        Color color = new Color()
                        {
                            Id = int.Parse(reader[0].ToString()),
                            DogId = int.Parse(reader[1].ToString()),
                            Name = reader[2].ToString()
                        };

                        colors.Add(color);


                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("GetColorsById:" + ex.Message);
                    throw;
                }

            }
            return colors;
        }





        public void AddImage(Image newImage, int dogId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                               
                    SqlCommand cmd = new SqlCommand(insertImageQuery, conn);
                    cmd.Parameters.AddWithValue("@dogId", dogId);
                    cmd.Parameters.AddWithValue("@image", newImage.Name);

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

        public void AddStatus(Status newStatus, int dogId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                
                    SqlCommand cmd = new SqlCommand(insertStatusQuery, conn);
                    cmd.Parameters.AddWithValue("@dogId", dogId);
                    cmd.Parameters.AddWithValue("@status", newStatus.DogStatus);
                    cmd.Parameters.AddWithValue("@date", newStatus.Date);

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

        public void AddColor(Color newColor, int dogId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {

                    SqlCommand cmd = new SqlCommand(insertColorQuery, conn);
                    cmd.Parameters.AddWithValue("@dogId", dogId);
                    cmd.Parameters.AddWithValue("@color", newColor.Name);

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


        public void UpdateImage(Image updateImage, int dogId)
        {
            if (updateImage.Id == 0)
            {
                AddImage(updateImage, dogId);
                return;
            }

            if (updateImage.Name == "")
            {
                DeleteImage(updateImage.Id);
                return;
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {

                SqlCommand cmd = new SqlCommand(updateImageQuery + byId, conn);
                cmd.Parameters.AddWithValue("@image", updateImage.Name);
                cmd.Parameters.AddWithValue("@id", updateImage.Id);

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


        public void UpdateStatus(Status updateStatus, int dogId)
        {
            if (updateStatus.Id == 0)
            {
                AddStatus(updateStatus, dogId);
                return;
            }

            if (updateStatus.DogStatus == "")
            {
                DeleteStatus(updateStatus.Id);
                return;
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {

                SqlCommand cmd = new SqlCommand(updateStatusQuery + byId, conn);
                cmd.Parameters.AddWithValue("@status", updateStatus.DogStatus);
                cmd.Parameters.AddWithValue("@date", updateStatus.Date);
                cmd.Parameters.AddWithValue("@id", updateStatus.Id);

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

        public void UpdateColor(Color updateColor, int dogId)
        {
            if (updateColor.Id == 0)
            {
                AddColor(updateColor, dogId);
                return;
            }

            if (updateColor.Name == "")
            {
                DeleteColor(updateColor.Id);
                return;
            }

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {

                SqlCommand cmd = new SqlCommand(updateColorQuery + byId, conn);
                cmd.Parameters.AddWithValue("@color", updateColor.Name);
                cmd.Parameters.AddWithValue("@id", updateColor.Id);

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







        public void DeleteImage(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {

                SqlCommand cmd = new SqlCommand(deleteImageQuery + byId, conn);
                cmd.Parameters.AddWithValue("@id", id);

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

        public void DeleteStatus(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {

                SqlCommand cmd = new SqlCommand(deleteStatusQuery + byId, conn);
                cmd.Parameters.AddWithValue("@id", id);

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

        public void DeleteColor(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {

                SqlCommand cmd = new SqlCommand(deleteColorQuery + byId, conn);
                cmd.Parameters.AddWithValue("@id", id);

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


    }
}
