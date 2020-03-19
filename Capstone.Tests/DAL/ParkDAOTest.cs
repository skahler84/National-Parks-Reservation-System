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
    public class ParkDAOTests : CapstoneInitialize
    {
        [TestMethod]
        public void GetParksTest_ShouldReturnAllParks()
        {
            //Arrange
            ParkDAO dao = new ParkDAO(ConnectionString);
            //Act
            IList<Park> parks = dao.GetParks();
            //Assert
            Assert.AreEqual(1, parks.Count);
        }

        [TestMethod]
        public void GetParkDetailsTest_ShouldReturnParkDetails()
        {
            //Arrange
            ParkDAO dao = new ParkDAO(ConnectionString);
            //Act
            IList<Park> parks = dao.GetParkDetails(ParkId);
            //Assert
            Assert.AreEqual(1, parks.Count);
        }
    }
}
