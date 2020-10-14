using Hotel.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Interfaces
{
    public interface IProfileService : IDisposable
    {
        /// <summary>
        /// Get all non-paid bookings for user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Enumerable BookingDTO</returns>
        IEnumerable<BookingDTO> GetUserBookingToConfirm(string userId);
        /// <summary>
        /// Get all paid bookings for user
        /// Rent time is not finished here
        /// It could not start too
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Enumerable BookingDTO</returns>
        IEnumerable<BookingDTO> GetActiveUserBooking(string userId);
        /// <summary>
        /// Get all bookings for user where the rent time is finished 
        /// They were paid 
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Enumerable BookingDTO</returns>
        IEnumerable<BookingDTO> GetPastUserBooking(string userId);
        /// <summary>
        /// Pay the booking
        /// </summary>
        /// <param name="bookingId">User id</param>
        void PayForRoom(int bookingId);
    }
}
