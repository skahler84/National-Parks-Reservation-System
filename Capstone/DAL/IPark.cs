using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL

{
    public interface IParkDAO
    {
        /// <summary>
        /// Returns a list of all parks.
        /// </summary>
        /// <returns></returns>
        IList<Park> GetParks();
        /// <summary>
        /// Returns all of the park details
        /// </summary>
        /// <param name="parkId"></param>
        /// <returns></returns>
        IList<Park> GetParkDetails(int parkId);
    }

}
