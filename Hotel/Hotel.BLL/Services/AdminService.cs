using Hotel.BLL.Interfaces;
using Hotel.BLL.DTOs;
using Hotel.BLL.Enums;
using Hotel.DAL.Context;
using Hotel.DAL.Entities;
using Hotel.DAL.Interfaces;
using Hotel.DAL.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Hotel.BLL.IdentityLogic;
using Microsoft.AspNet.Identity;
using Hotel.BLL.Models;

namespace Hotel.BLL.Services
{
    public class AdminService : IAdminService
    {
        private UserManager<ApplicationUser> userManager;
        private IUoW<ApplicationDbContext> db { get; }
        public AdminService(IUoW<ApplicationDbContext> db)
        {
            this.db = db ?? new UoW();
        }


        // To interact with idetity framework users
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

        public AdminService()
        {
            db = new UoW();
        }


        public IEnumerable<ApplicationUserDTO> GetUsers(string role = null)
        {
            var list = new List<ApplicationUserDTO>();
            if (role is null)
                list = db.Users.Select(BLLService.BLLMapper.ApplicationUserMap).ToList();
            else
            {
                list = db.Users.ToList().Where(x => UserManager.IsInRole(x.Id, role))
                    .Select(BLLService.BLLMapper.ApplicationUserMap).ToList();
                list.ForEach(x => x.Role = role);
            }
            return list.OrderByDescending(x => x.UserName);
        }

        public IEnumerable<UnluckyRequestDTO> GetAllUnluckyRequests()
        {
            return db.UnluckyRequests.GetAll()
                .Select(BLLService.BLLMapper.UnluckyRequestMap);
        }

        public OperationMessage AnswerToUnluckyRequest(int requestId, int roomId
            , DateTime start, DateTime finish)
        {
            var request = db.UnluckyRequests.Get(requestId);

            if (request is null || request.IsDeleted == true) //check for all errors
                return new OperationMessage() { Text = "Unpredicted problem reload page", IsPositive = false } ;

            var room = db.Rooms.Get(roomId);

            if (room is null || room.IsDeleted == true)
                return new OperationMessage() { Text = "Unpredicted problem reload page", IsPositive = false };

            if (BLLService.GetStatus(room, start, finish) != RoomStatus.AVAILABLE)
                return new OperationMessage() { Text = "Do not change the date before cliking", IsPositive = false };

            db.Bookings.Create(new Booking()
            {
                DeadLine = DateTime.Today.AddDays(2),
                BeginningDate = start,
                EndingDate = finish,
                User = request.User,
                Room = room,
                IsConfirmed = false
            });
            db.UnluckyRequests.Get(requestId).IsDeleted = true;
            db.Commit();
            return new OperationMessage() { Text = "Proposition has been sent!", IsPositive = true };
        }

        public IEnumerable<string> GetPossiblePeopleCounts()
        {
            return (from i in db.Rooms.GetAll().ToList()
                    where i.IsAccessible
                    orderby i.PeopleCount
                    select i.PeopleCount.ToString()).Distinct();
        }

        public OperationMessage AddToRole(string UserName, string Role)
        {
            var user = UserManager.FindByName(UserName);
            if (user is null)
                return new OperationMessage() { Text = "User does not exist", IsPositive = false };

            if (UserManager.IsInRole(user.Id, Role))
                return new OperationMessage() { Text = $"This user is {Role}", IsPositive = false };

            if (!UserManager.RemoveFromRole(user.Id, "user").Succeeded)
                return new OperationMessage() { Text = "Unexpected user", IsPositive = false };

            if (UserManager.AddToRole(user.Id, Role).Succeeded)
                return new OperationMessage() { Text = $"The user {UserName} has just got the {Role} permissions", IsPositive = true };

            return new OperationMessage() { Text = "ERROR", IsPositive = false }; 
        }

        public OperationMessage DownGradeFromStaff(string UserName)
        {
            var user = UserManager.FindByName(UserName);
            if (user is null)
                return new OperationMessage() { Text = "User does not exist", IsPositive = false };

            if (UserManager.IsInRole(user.Id, "user"))
                return new OperationMessage() { Text = $"This user is user", IsPositive = false };

            if (!UserManager.RemoveFromRole(user.Id, "staff").Succeeded)
                return new OperationMessage() { Text = "Unexpected user", IsPositive = false };

            if (UserManager.AddToRole(user.Id, "user").Succeeded)
                return new OperationMessage() { Text = $"The user {UserName} has just lost the staff permissions", IsPositive = true };

            return new OperationMessage() { Text = "ERROR", IsPositive = false };
        }

        public OperationMessage DeleteUser(string UserName)
        {
            var user = UserManager.FindByName(UserName);
            if (user is null)
                return new OperationMessage() { Text = "User does not exist", IsPositive = false };

            if (UserManager.IsInRole(user.Id, "deleted"))
                return new OperationMessage() { Text = "This user is deleted already", IsPositive = false };

            UserManager.RemoveFromRole(user.Id, "user");
            UserManager.RemoveFromRole(user.Id, "admin");
            UserManager.RemoveFromRole(user.Id, "staff");

            if (UserManager.AddToRole(user.Id, "deleted").Succeeded)
                return new OperationMessage() { Text = $"User {UserName} has just been deleted", IsPositive = true };

            return new OperationMessage() { Text = "ERROR", IsPositive = false };
        }

        public OperationMessage RestoreUser(string UserName)
        {
            var user = UserManager.FindByName(UserName);
            if (user is null)
                return new OperationMessage() { Text = "User does not exist", IsPositive = false };

            if (!UserManager.IsInRole(user.Id, "deleted"))
                return new OperationMessage() { Text = "This user is not deleted", IsPositive = false };

            if (!UserManager.RemoveFromRole(user.Id, "deleted").Succeeded)
                return new OperationMessage() { Text = "Unexpected error", IsPositive = false };

            if (UserManager.AddToRole(user.Id, "user").Succeeded)
                return new OperationMessage() { Text = $"User {UserName} has just been restored", IsPositive = true };

            return new OperationMessage() { Text = "ERROR", IsPositive = false };
        }

        public IEnumerable<string> GetPossibleRoomClassNames()
        {
            return (from i in db.Rooms.GetAll().ToList()
                    where i.IsAccessible
                    orderby i.RoomClass.Price
                    select i.RoomClass.Name).Distinct();
        }

        public IEnumerable<RoomDTO> SearchRooms(string ClassName, byte PeopleCount,
            DateTime start, DateTime finish)
        {
            return db.Rooms.GetAll().ToList().Where(x => 
            x.PeopleCount == PeopleCount
            && x.RoomClass.Name == ClassName
            && BLLService.GetStatus(x, start,finish) == RoomStatus.AVAILABLE)
                .Select(BLLService.BLLMapper.RoomMap);
        }

        public IEnumerable<LogDTO> GetLastLogs()
        {
            var count = db.Logs.GetCount();
            if (count == 0)
                return new List<LogDTO>();
            return db.Logs.GetRange(count <= 200 ? 0 : count - 200, count - 1)
                .Select(BLLService.BLLMapper.LogMap);
        }

        public IEnumerable<LogDTO> GetLogs(string userName, DateTime? start
            , DateTime? finish, bool? searchExept, string URL)
        {
            var list = db.Logs.GetAll();

            if (!(URL is null))
                list = list.Where(x => x.URL.Contains(URL));

            if (!(userName is null))
                list = list.Where(x => x.UserName.Contains(userName));

            if (start.HasValue)
                list = list.Where(x => x.Date > start);

            if (finish.HasValue)
                list = list.Where(x => x.Date < finish);

            if (searchExept.HasValue && searchExept.Value)
                list = list.Where(x => x.Exeption != "-");

            return list.Select(BLLService.BLLMapper.LogMap);
        }
        public void DeleteFromAdminRole(string UserName)
        {
            var user = UserManager.FindByName(UserName);
            if (UserManager.RemoveFromRole(user.Id, "admin").Succeeded)
                UserManager.AddToRole(user.Id, "user");
        }
        public void Dispose()
        {
            db.Dispose();
        }


    }
}
