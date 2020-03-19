using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Site
    {
        /// <summary>
        /// the site ID
        /// </summary>
        public int siteId { get; set; }
        /// <summary>
        /// the campgroundID
        /// </summary>
        public  int campgroundId {get; set; }
        /// <summary>
        /// the site number
        /// </summary>
        public int siteNumber { get; set; }
        /// <summary>
        /// the maximum occupancy of the site
        /// </summary>
        public int maxOccupancy { get; set; }
        /// <summary>
        /// whether or not it's accessible
        /// </summary>
        public int accessible { get; set; }
        /// <summary>
        /// the maximum length of the RV that can access the site
        /// </summary>
        public int maxRvLength { get; set; }
        /// <summary>
        /// whether or not it has utilities
        /// </summary>
        public int utilities { get; set; }
    }
}
