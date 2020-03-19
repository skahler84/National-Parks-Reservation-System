using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Reservation
    {
        /// <summary>
        /// the reservationID
        /// </summary>
        public int reservationId { get; set; }
        /// <summary>
        /// the siteID
        /// </summary>
        public int siteID { get; set; }
        /// <summary>
        /// the reservation name
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// the beginning date of the reservation
        /// </summary>
        public DateTime fromDate { get; set; }
        /// <summary>
        /// the ending date of the reservation
        /// </summary>
        public DateTime toDate { get; set; }
        /// <summary>
        /// the date the reservation was created
        /// </summary>
        public DateTime createDate { get; set; }
    }
}
