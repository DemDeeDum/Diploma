using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.DAL.Interfaces;
using Hotel.DAL.Context;
using Hotel.DAL.UoW;
using System.Threading;
using Hotel.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Hotel.BLL.BookingKiller
{
    /// <summary>
    /// Soft delete booking which was not paid until deadline. 
    /// Soft delete Unlucky request which did not get an answer or its sender has been deleted.
    /// Restore requests created by senders which have been restored
    /// </summary>
    public class Destroyer : IDisposable
    {
        private Thread destroyer;
        private IUoW<ApplicationDbContext> db;
        private UserManager<ApplicationUser> userManager;
        public Destroyer()
        {
            db = new UoW();
            destroyer = new Thread(Processing);
            destroyer.IsBackground = true; 
            destroyer.Priority = ThreadPriority.Lowest; //to save resources for main thread
            destroyer.Start();
        }

        public UserManager<ApplicationUser> UserManager
        {
            get
            {
                if (userManager is null)
                    userManager = new UserManager<ApplicationUser>
                        (new UserStore<ApplicationUser>
                        (new ApplicationDbContext()));
                return userManager;
            }
        }

        private void Processing()
        {
            while (true)
            {
                var listBookings = db.Bookings.GetAll().ToList().
                    Where(x => x.DeadLine <= DateTime.Now && !x.IsConfirmed); //Booking which are not paid
                foreach (var booking in listBookings)
                    db.Bookings.Get(booking.Id).IsDeleted = true;
                db.Commit();
                try
                {
                    var listRequests = db.UnluckyRequests.GetAll().ToList()
                       .Where(x => x.StartDate <= DateTime.Today.AddDays(3) //unlucky requests whose start dates will be in the near future
                       || UserManager.IsInRole(x.User.Id, "deleted")).ToList(); //requests from users which are banned or deleted
                    foreach (var request in listRequests)
                        db.UnluckyRequests.Get(request.Id).IsDeleted = true;
                    db.Commit();
                }
                catch
                {

                }
                try
                {
                    var listRestore = db.UnluckyRequests.set.ToList().Where(x => x.IsDeleted
                    && x.StartDate.AddDays(3) > DateTime.Today
                    && !UserManager.IsInRole(x.User.Id, "deleted")).ToList(); //requests from users which have been restored
                    foreach (var request in listRestore)
                        db.UnluckyRequests.Get(request.Id).IsDeleted = false;
                    db.Commit();
                }
                catch
                {

                }
                Thread.Sleep(5000); //once per 5sec
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
