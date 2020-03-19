using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
   public class CampGround
    {
        /// <summary>
        /// The name of the campground
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// The Campground Id
        /// </summary>
        public int campground_id { get; set; }
        /// <summary>
        /// The Park Id
        /// </summary>
        public int park_id { get; set; }
        /// <summary>
        /// The month the park opens
        /// </summary>
        public int open_from_mm { get; set; }
        /// <summary>
        /// The month the park closes
        /// </summary>
        public int open_to_mm { get; set; }
        /// <summary>
        /// The daily cost to stay in the campground
        /// </summary>
        public decimal daily_fee { get; set; }
    }
}
