using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Transactions;

namespace Capstone.Tests
{
    [TestClass]
    public class CapstoneInitialize
    {
        /// <summary>
        /// Initializes the database for the test
        /// </summary>
        private TransactionScope transaction;
        /// <summary>
        /// The connection string for the initializer
        /// </summary>
        protected string ConnectionString { get; } = "Server=.\\SQLEXPRESS;Database=npcampground;Trusted_Connection=True;";
        /// <summary>
        /// The park id for the tests
        /// </summary>
        protected int ParkId { get; private set; }
        /// <summary>
        /// The campground id for the tests
        /// </summary>
        protected int CampGroundId { get; private set; }
        /// <summary>
        /// the site id for the tests
        /// </summary>
        protected int SiteId { get; private set; }
        /// <summary>
        /// the reservation id for the tests
        /// </summary>
        protected int ReservationId { get; private set; }

        [TestInitialize]
        public void Setup()
        {
            //begins the transaction
            transaction = new TransactionScope();
            
            //gets the sql
            string sql = File.ReadAllText("Test-Info.sql");

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ParkId = Convert.ToInt32(reader["parkId"]);
                    CampGroundId = Convert.ToInt32(reader["campgroundId"]);
                    SiteId = Convert.ToInt32(reader["siteId"]);
                    ReservationId = Convert.ToInt32(reader["reservationId"]);
                }
            }
        }
        /// <summary>
        /// Cleans up the databse after each test
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            transaction.Dispose();
        }
        /// <summary>
        /// gets the row count for a given table
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        protected int GetRowCount(string table)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {table}", connection);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count;
            }
        }
    }
}
