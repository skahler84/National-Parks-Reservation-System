using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone.Tests
{
    [TestClass]
    public class ReservationDAOTests : CapstoneInitialize
    {
        [TestMethod]
        public void CreateReservationTest_ShouldMakeCountIncrease1()
        {
            //Arrange
            ReservationDAO dao = new ReservationDAO(ConnectionString);
            int rowCount = GetRowCount("reservation");

            Reservation reservation = new Reservation()
            {
                siteID = SiteId,
                name = "Test Reservation Name",
                fromDate = Convert.ToDateTime("06-06-2020"),
                toDate = Convert.ToDateTime("06-10-2020")
            };
            //Act
            dao.CreateReservation(reservation);
            //Assert
            int newRowCount = GetRowCount("reservation");
            Assert.AreEqual(rowCount + 1, newRowCount);
        }
    }
}