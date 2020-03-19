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
    public class SiteDAOTests : CapstoneInitialize
    {
        [TestMethod]
        public void SearchReservationTest_ReturnsListOfSites()
        {
            //Arrange
            var arriveDate = new DateTime(2020, 08, 20);
            var departDate = new DateTime(2020, 08, 24);            
            SiteDAO dao = new SiteDAO(ConnectionString);
            //Act
            IList<Site> sites = dao.SearchReservation(CampGroundId, arriveDate, departDate);
            //Assert
            Assert.AreEqual(1, sites.Count);
        }

        [TestMethod]
        public void SearchReservationTest_DatesAlreadyUsed_ReturnsNone()
        {
            //Arrange
            var arriveDate = new DateTime(2015, 02, 21);
            var departDate = new DateTime(2015, 02, 24);
            SiteDAO dao = new SiteDAO(ConnectionString);
            //Act
            IList<Site> sites = dao.SearchReservation(CampGroundId, arriveDate, departDate);
            //Assert
            Assert.AreEqual(0, sites.Count);
        }

        [TestMethod]
        public void SearchReservationTest_StartsBeforeEndsDuring_ReturnsNone()
        {
            //Arrange
            var arriveDate = new DateTime(2020, 02, 20);
            var departDate = new DateTime(2020, 02, 22);
            SiteDAO dao = new SiteDAO(ConnectionString);
            //Act
            IList<Site> sites = dao.SearchReservation(CampGroundId, arriveDate, departDate);
            //Assert
            Assert.AreEqual(0, sites.Count);
        }

        [TestMethod]
        public void SearchReservationTest_StartsDuringEndsAfter_ReturnsNone()
        {
            //Arrange
            var arriveDate = new DateTime(2020, 02, 22);
            var departDate = new DateTime(2020, 02, 25);
            SiteDAO dao = new SiteDAO(ConnectionString);
            //Act
            IList<Site> sites = dao.SearchReservation(CampGroundId, arriveDate, departDate);
            //Assert
            Assert.AreEqual(0, sites.Count);
        }

        [TestMethod]
        public void SearchReservationTest_StartsBeforeEndsAfter_ReturnsNone()
        {
            //Arrange
            var arriveDate = new DateTime(2020, 02, 20);
            var departDate = new DateTime(2020, 02, 25);
            SiteDAO dao = new SiteDAO(ConnectionString);
            //Act
            IList<Site> sites = dao.SearchReservation(CampGroundId, arriveDate, departDate);
            //Assert
            Assert.AreEqual(0, sites.Count);
        }
    }
}
