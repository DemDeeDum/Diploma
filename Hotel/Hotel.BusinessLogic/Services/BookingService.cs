// <copyright file="BookingService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Hotel.BLL.DTOs;
    using Hotel.BLL.Enums;
    using Hotel.BLL.Interfaces;
    using Hotel.BLL.Models;
    using Hotel.DAL.Context;
    using Hotel.DAL.Entities;
    using Hotel.DAL.UnitOfWork.Interfaces;

    /// <summary>
    /// Service for booking logic.
    /// </summary>
    public class BookingService : IBookingPageService
    {
        /// <summary>
        /// Provides access to a db.
        /// </summary>
        private readonly IUnitOfWork<ApplicationDbContext> unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingService"/> class.
        /// </summary>
        /// <param name="unitOfWork">Provides access to a db.</param>
        public BookingService(
            IUnitOfWork<ApplicationDbContext> unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoomDTO> GetAllRooms()
        {
            return db.Rooms.GetAll().Select(BLLService.BLLMapper.RoomMap);
        }

        public int GetRoomCount()
        {
            return db.Rooms.GetCount();
        }

        public int GetRoomCount(DateTime start, DateTime finish)
        {
            return db.Rooms.GetAll().ToList()
                .Where(x => BLLService.GetStatus(x, start, finish)
                == RoomStatus.AVAILABLE).Count();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public IEnumerable<RoomDTO> GetRoomRange(int StartPosition
            , int FinishPosition)
        {
            return db.Rooms.GetRange(StartPosition, FinishPosition)
                .Select(BLLService.BLLMapper.RoomMap).OrderBy(x => x.Number);
        }

   

        public IEnumerable<RoomDTO> GetRoomRange(int StartPosition
           , int FinishPosition, OrderCreator order)
        {

            Func<IEnumerable<Room>, IOrderedEnumerable<Room>> orderator =
                !order.Descending? (Func<IEnumerable<Room>, IOrderedEnumerable<Room>> )  //Creating invocation list to sort later
                ( x => x.OrderBy(y => y.Number)) 
                : x => x.OrderByDescending(y => y.Number);
            if (order.RoomClassFilter)
                orderator += !order.Descending ? (Func<IEnumerable<Room>, IOrderedEnumerable<Room>>)
                    (x => x.OrderBy(y => y.RoomClass.Price))
                : x => x.OrderByDescending(y => y.RoomClass.Price);
            if (order.PriceFilter)
                orderator += !order.Descending ? (Func<IEnumerable<Room>, IOrderedEnumerable<Room>>)
                    (x => x.OrderBy(y => y.PeopleCount * y.RoomClass.Price))
                : x => x.OrderByDescending(y => y.PeopleCount * y.RoomClass.Price);
            if (order.PeopleFilter)
                orderator += !order.Descending ? (Func<IEnumerable<Room>, IOrderedEnumerable<Room>>)
                    (x => x.OrderBy(y => y.PeopleCount))
                : x => x.OrderByDescending(y => y.PeopleCount);


            var list = db.Rooms.GetAll().ToList();
            foreach (var x in orderator.GetInvocationList()) //sort by all conditions excluding status one
                list = ((IOrderedEnumerable<Room>)x.DynamicInvoke(list)).ToList();

            var bufList = new List<RoomDTO>();
            foreach(var room in list) //converting to RoomDTO and getting the status to each room
            {
                var roomDTO = BLLService.BLLMapper.RoomMap(room);
                roomDTO.AppartmentStatus = BLLService.GetStatus(room, order.BeginningTime, order.EndingTime);
                bufList.Add(roomDTO);
            }
            if (order.StatusFilter) //sort by status
                bufList = bufList.Where(x => x.AppartmentStatus == RoomStatus.AVAILABLE).ToList();
            var result = new List<RoomDTO>(); 
            for (int i = StartPosition; i < FinishPosition && i < bufList.Count; i++)
                result.Add(bufList[i]); //get needed positions
            return result;
        }

        public IEnumerable<string> GetPossiblePeopleCounts()
        {
            return (from i in db.Rooms.GetAll().ToList()
                    orderby i.PeopleCount
                    select i.PeopleCount.ToString()).Distinct();
        }

        public IEnumerable<string> GetPossibleRoomClassNames()
        {
            return (from i in db.Rooms.GetAll().ToList()
                    orderby i.RoomClass.Price
                    select i.RoomClass.Name).Distinct();
        }

        public IEnumerable<RoomClassDTO> GetAllRoomClasses()
        {
            return db.RoomClasses.GetAll().Select(BLLService.BLLMapper.RoomClassMap)
                .OrderByDescending(x=>x.Price);
        }

        public RoomWholeInfo GetCertainRoom(int id)
        {
            var room = db.Rooms.Get(id);
            if (room is null)
                return null;
            return BLLService.BLLMapper.RoomWholeInfoMap(room);
        }

        public string GetRoomStatus(int id, DateTime start, DateTime finish)
        {
            var room = db.Rooms.Get(id);
            if (room is null)
                return null;
            return BLLService.GetStatus(room, start, finish).ToString();
        }

        public void SearchFitRoom(string userId, string ClassName, byte PeopleCount, 
            DateTime start, DateTime finish)
        {
            var list = db.Rooms.GetAll().ToList().Where(
                x => x.PeopleCount == PeopleCount
                && x.RoomClass.Name == ClassName
                && BLLService.GetStatus(x, start, finish) == RoomStatus.AVAILABLE).ToList(); //get available rooms
            if (list.Count == 0) //if found nothing create unlucky request
                db.UnluckyRequests.Create(new UnluckyRequest()
                {
                    FinishDate = finish,
                    StartDate = start,
                    User = db.Users.Find(userId),
                    PeopleCount = PeopleCount,
                    RoomClassName = ClassName
                });
            else //else create booking
                CreateBooking(userId, list[0].Id, start, finish, false);
        }

        public void CreateBooking(string userId, int roomId, DateTime start, DateTime finish,
            bool IsConfirmed = true)
        {
            db.Bookings.Create(new Booking()
            {
                BeginningDate = start,
                EndingDate = finish,
                User = db.Users.Find(userId),
                Room = db.Rooms.Get(roomId),
                IsConfirmed = IsConfirmed,
                DeadLine = DateTime.Today.AddDays(2)
            });
            db.Commit();
        }
    }
}
