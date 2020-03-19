using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class CampgroundDAO : ICampGroundDAO
    {
        private string connectionString;

        // Single Parameter Constructor
        public CampgroundDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns a list of all of the campgrounds.
        /// </summary>
        /// <returns></returns>
        ///                
        public IList<CampGround> GetCampGrounds(int parkId)
        {
            List<CampGround> campGrounds = new List<CampGround>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //sql statement that returns the campgrounds that are within the park selected by the user
                    SqlCommand cmd = new SqlCommand("SELECT * FROM campground WHERE park_id = @park_id", connection);
                    cmd.Parameters.AddWithValue("@park_id", parkId);                    
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        CampGround campGround = ConvertReaderToCampGrouds(reader);                        
                        campGrounds.Add(campGround);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred");
                Console.WriteLine(e.Message);
                throw;
            }
            return campGrounds;
        }

        public CampGround GetSingleCampGround(int campGroundId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //sql statement that references an individual campgroundId so that the cost can be calculated
                    SqlCommand cmd = new SqlCommand("SELECT * FROM campground WHERE campground_id = @campground_id;", connection);
                    cmd.Parameters.AddWithValue("@campground_id", campGroundId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        CampGround campGround = ConvertReaderToCampGrouds(reader);
                        return campGround;
                    }
                    else
                    {                        
                        return null;
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine("Could not find campground");
                Console.WriteLine(e.Message);
            }
            return null;
        }
        
        private CampGround ConvertReaderToCampGrouds(SqlDataReader reader)
        {
            CampGround campGround = new CampGround();

            campGround.name = Convert.ToString(reader["name"]);
            campGround.campground_id = Convert.ToInt32(reader["campground_id"]);
            campGround.park_id = Convert.ToInt32(reader["park_id"]);
            campGround.open_from_mm = Convert.ToInt32(reader["open_from_mm"]);
            campGround.open_to_mm = Convert.ToInt32(reader["open_to_mm"]);
            campGround.daily_fee = Convert.ToDecimal(reader["daily_fee"]);

            return campGround;
        }
    }
}


