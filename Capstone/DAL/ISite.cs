using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public interface ISiteDAO
    {
        /// <summary>
        /// Searches the sites for availability given the user's date, park and campground choices
        /// </summary>
        /// <param name="campgroundId"></param>
        /// <param name="arriveDate"></param>
        /// <param name="departDate"></param>
        /// <returns></returns>
        IList<Site> SearchReservation(int campgroundId, DateTime arriveDate, DateTime departDate);

    }
}
