using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    
    public interface ICampGroundDAO
    {
        /// <summary>
        /// Returns a list of all campgrounds.
        /// </summary>
        /// <returns></returns>
        IList<CampGround> GetCampGrounds(int parkId);
        /// <summary>
        /// Returns a single campground
        /// </summary>
        /// <param name="campGroundId"></param>
        /// <returns></returns>
        CampGround GetSingleCampGround(int campGroundId);        
    }
}
