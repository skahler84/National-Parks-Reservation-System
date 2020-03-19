using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{

    public class ParkDAO : IParkDAO
    {
        private string connectionString;

        // Single Parameter Constructor
        public ParkDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns a list of all of the parks.
        /// </summary>
        /// <returns></returns>
        public IList<Park> GetParks()
        {
            List<Park> parks = new List<Park>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //sql statement that returns a list of all the parks in the database
                    SqlCommand cmd = new SqlCommand("SELECT * FROM park;", connection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Park park = ConvertReaderToParks(reader);                        
                        parks.Add(park);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred communicating with the database. ");
                Console.WriteLine(e.Message);
                throw;
            }
            return parks;
        }

        public IList<Park> GetParkDetails(int parkId)
        {
            List<Park> parks = new List<Park>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //sql statement that selects the park chosen by the user so that it's details can be displayed
                    SqlCommand cmd = new SqlCommand("SELECT * from park WHERE park_id = @park_id", connection);
                    cmd.Parameters.AddWithValue("park_id", parkId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        Park park = ConvertReaderToParks(reader);
                        parks.Add(park);
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine("Error");
                Console.WriteLine(e.Message);
                throw;
            }
            return parks;
        }

        private Park ConvertReaderToParks(SqlDataReader reader)
        {
            Park park = new Park();

            park.park_id = Convert.ToInt32(reader["park_id"]);
            park.name = Convert.ToString(reader["name"]);
            park.location = Convert.ToString(reader["location"]);
            park.establish_date = Convert.ToDateTime(reader["establish_date"]);
            park.area = Convert.ToInt32(reader["area"]);
            park.visitors = Convert.ToInt32(reader["visitors"]);
            park.description = Convert.ToString(reader["description"]);

            return park;
        }
    }
}


