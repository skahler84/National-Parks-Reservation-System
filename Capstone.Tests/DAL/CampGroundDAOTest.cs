using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone.Tests
{
    [TestClass]
    public class CampGroundDAOTest : CapstoneInitialize
    {
        [TestMethod]
        public void GetCampGroundsTest_ShouldReturnAllCampGrounds()
        {
            //Arrange
            CampgroundDAO dao = new CampgroundDAO(ConnectionString);
            //Act
            IList<CampGround> campGrounds = dao.GetCampGrounds(ParkId);
            //Assert
            Assert.AreEqual(1, campGrounds.Count);
        }
    }
}
