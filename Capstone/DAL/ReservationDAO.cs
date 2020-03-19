using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data.SqlTypes;

namespace Capstone.DAL
{
    public class ReservationDAO : IReservationDAO
    {
        private string connectionString;

        // Single Parameter Constructor
        public ReservationDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        // Creates reservation based on the user inputs for dates, siteId, and reservation name
        public int CreateReservation (Reservation reservation)
        {
            int reservationId = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //sql statement that inserts the requested information into the database for the new reservation
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO reservation (site_id, name, from_date, to_date) VALUES (@siteIdChoice, @reservationName, @arriveDate, @departDate)", connection);
                    cmd.Parameters.AddWithValue("@siteIdChoice", reservation.siteID);
                    cmd.Parameters.AddWithValue("@reservationName", reservation.name);
                    cmd.Parameters.AddWithValue("@arriveDate", reservation.fromDate);
                    cmd.Parameters.AddWithValue("@departDate", reservation.toDate);
                    cmd.ExecuteNonQuery();

                    //sql statement to create the new confirmation number
                    cmd = new SqlCommand("SELECT MAX(reservation_id) FROM reservation", connection);

                    reservationId = Convert.ToInt32(cmd.ExecuteScalar());

                    Console.WriteLine($"The reservation has been made the the confirmation ID is {reservationId}");
                }
            } catch (Exception e)
            {
                Console.WriteLine("There was an error");
                Console.WriteLine(e.Message);
            }
            return reservationId;
        }

        private Reservation ConvertReaderToReservation(SqlDataReader reader)
        {
            Reservation reservation = new Reservation();

            reservation.name = Convert.ToString(reader["name"]);
            reservation.reservationId = Convert.ToInt32(reader["reservation_id"]);
            reservation.siteID = Convert.ToInt32(reader["site_id"]);
            reservation.fromDate = Convert.ToDateTime(reader["from_date"]);
            reservation.toDate = Convert.ToDateTime(reader["to_date"]);
            reservation.createDate = Convert.ToDateTime(reader["create_date"]);

            return reservation;
        }        
    }
}
