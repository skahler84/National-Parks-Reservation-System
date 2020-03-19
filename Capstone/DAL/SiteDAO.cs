using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class SiteDAO : ISiteDAO
    {
        private string connectionString;

        // Single Parameter Constructor

        public SiteDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }        
        //searches for reservation based on the user's choice of dates and campgroundId
        public IList<Site> SearchReservation (int campGroundId, DateTime arriveDate, DateTime departDate)
        {
            List<Site> sites = new List<Site>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //sql statement that returns the top 5 sites available based on campground and dates chosen by the user, also accounts for when the parks are open and what reservations already exist
                    SqlCommand cmd = new SqlCommand(@"SELECT TOP 5 * FROM site INNER JOIN campground ON campground.campground_id = site.campground_id WHERE site.campground_id = @campgroundID AND campground.open_from_mm <= datepart(month,@arriveDate) AND campground.open_to_mm >= datepart(month,@departDate) AND site.site_id NOT IN (SELECT site_id FROM reservation WHERE from_date < @departDate AND to_date > @arriveDate);", connection);
                    cmd.Parameters.AddWithValue("@campGroundId", campGroundId);
                    cmd.Parameters.AddWithValue("@arriveDate", arriveDate);
                    cmd.Parameters.AddWithValue("@departDate", departDate);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site site = ConvertReaderToSite(reader);
                        sites.Add(site);
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine("Error in reservation search");
                Console.WriteLine(e.Message);
                throw;
            }
            return sites;
        }

        private Site ConvertReaderToSite(SqlDataReader reader)
        {
            Site site = new Site();

            site.siteId = Convert.ToInt32(reader["site_id"]);
            site.campgroundId = Convert.ToInt32(reader["campground_id"]);
            site.siteNumber = Convert.ToInt32(reader["site_number"]);
            site.maxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
            site.accessible = Convert.ToInt32(reader["accessible"]);
            site.utilities = Convert.ToInt32(reader["utilities"]);
            site.maxRvLength = Convert.ToInt32(reader["max_rv_length"]);

            return site;
        }
    }
}
