using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public interface IReservationDAO
    {
        /// <summary>
        /// Creates a reservation with arrival/departure dates, the name it's reserved under and the confirmation number
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        int CreateReservation(Reservation reservation);
    }
}
