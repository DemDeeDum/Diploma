using Hotel.BLL.Enums;
using Hotel.BLL.Infrastructure;
using Hotel.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Services
{
    public static class BLLService
    {
        public static BLLMapper BLLMapper { get; }
        
        static BLLService()
        {
            BLLMapper = new BLLMapper();
        }

        /// <summary>
        /// Define the status of the room, checking IsAsccessible and IsDeleted fields
        /// </summary>
        /// <param name="obj">Room to define status</param>
        /// <param name="start">Start date</param>
        /// <param name="finish">Finish date</param>
        /// <returns>Room status in the RoomStatus enum</returns>
        public static RoomStatus GetStatus(Room obj, DateTime start, DateTime finish)
        {
            if (!obj.IsAccessible || obj.IsDeleted)
                return RoomStatus.INACCESSIBLE;
            else if (obj.Bookings.ToList().Any
                (z => (z.BeginningDate >= start && z.BeginningDate <= finish)
                || (z.EndingDate >= start && z.EndingDate <= finish)
                || (z.BeginningDate <= start && z.EndingDate >= finish)))
            {
                if (obj.Bookings.Any(x => x.BeginningDate <= DateTime.Today
                && x.EndingDate >= DateTime.Today))
                    return RoomStatus.OCCUPIED;
                else
                    return RoomStatus.BOOKED;
            }
            return RoomStatus.AVAILABLE;
        }

    }
}
