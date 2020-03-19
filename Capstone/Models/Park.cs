using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
   public class Park
    {
        /// <summary>
        /// The Park Id
        /// </summary>
        public int park_id { get; set; }
        /// <summary>
        /// The name of the park
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// The location of the park
        /// </summary>
        public string location { get; set; }
        /// <summary>
        /// The date the park was established
        /// </summary>
        public DateTime establish_date { get; set; }
        /// <summary>
        /// The area of the park in sq km
        /// </summary>
        public int area { get; set; }
        /// <summary>
        /// The amount of annual visitors
        /// </summary>
        public int visitors { get; set; }
        /// <summary>
        /// The description of the park
        /// </summary>
        public string description { get; set; }      
    }
}
