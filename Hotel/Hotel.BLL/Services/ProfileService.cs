using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.BLL.DTOs;
using Hotel.BLL.Infrastructure;
using Hotel.BLL.Interfaces;
using Hotel.DAL.Context;
using Hotel.DAL.Interfaces;
using Hotel.DAL.UoW;

namespace Hotel.BLL.Services
{
    public class ProfileService : IProfileService
    {
        private IUoW<ApplicationDbContext> db { get; }
        public ProfileService(IUoW<ApplicationDbContext> db)
        {
            this.db = db ?? new UoW();
        }

        public ProfileService() //для возможности создания экземляра класса в контроллерах
        {
            db = new UoW();
        }

        public IEnumerable<BookingDTO> GetUserBookingToConfirm(string userId)
        {
            return db.Bookings.GetAll().ToList().Where(x => x.ApplicationUserId == userId
            && !x.IsConfirmed)
                .Select(BLLService.BLLMapper.BookingMap);
        }

        public IEnumerable<BookingDTO> GetActiveUserBooking(string userId)
        {
            return db.Bookings.GetAll().ToList().Where(x => x.ApplicationUserId == userId
            && x.IsConfirmed && x.EndingDate >= DateTime.Today)
                .Select(BLLService.BLLMapper.BookingMap);
        }

        public IEnumerable<BookingDTO> GetPastUserBooking(string userId)
        {
            return db.Bookings.GetAll().ToList().Where(x => x.ApplicationUserId == userId
            && x.IsConfirmed && x.EndingDate < DateTime.Today)
                .Select(BLLService.BLLMapper.BookingMap);
        }

        public void PayForRoom(int bookingId)
        {
            var booking = db.Bookings.Get(bookingId);
            if (booking is null || booking.IsConfirmed || booking.IsDeleted)
                return;
            booking.IsConfirmed = true;
            db.Bookings.Update(booking);
            db.Commit();
            return;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
