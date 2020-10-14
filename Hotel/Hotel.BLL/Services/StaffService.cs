// <copyright file="StaffService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.BLL.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Hotel.BLL.DTOs;
    using Hotel.BLL.Enums;
    using Hotel.BLL.Interfaces;
    using Hotel.BLL.Models;
    using Hotel.DAL.Context;
    using Hotel.DAL.Entities;
    using Hotel.DAL.UnitOfWork.Interfaces;

    /// <summary>
    /// Service for staff operations.
    /// </summary>
    public class StaffService : IStaffService
    {
        /// <summary>
        /// Provides access to db.
        /// </summary>
        private readonly IUnitOfWork<ApplicationDbContext> unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffService"/> class.
        /// </summary>
        /// <param name="unitOfWork">Provides access to db.</param>
        public StaffService(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool IsRoomNumber(int number)
        {
            return db.Rooms.GetAll().Where(x => x.Number == number).Count() != 0;
        }

        public OperationMessage AddNewRoom(RoomDTO room)
        {
            if (room is null)
                return new OperationMessage()
                { IsPositive = false, Text = "Parameter \"room\" was null" };

            if (IsRoomNumber(room.Number))
                return new OperationMessage()
                { IsPositive = false, Text = $"Room which has number {room.Number} exists" };

            db.Rooms.Create(new DAL.Entities.Room()
            {
                Number = room.Number,
                IsAccessible = room.AppartmentStatus != Enums.RoomStatus.INACCESSIBLE,
                PeopleCount = room.PeopleCount,
                RoomClass = db.RoomClasses.set.Where(x => x.Name == room.RoomClassName).Single()
            });
            db.Commit();
            return new OperationMessage() { Text = $"Room №{room.Number} has been added", IsPositive = true };

        }


        public IEnumerable<string> GetPossibleRoomClassNames()
        {
            return (from i in db.Rooms.GetAll().ToList()
                    orderby i.RoomClass.Price
                    select i.RoomClass.Name).Distinct();
        }

        public IEnumerable<RoomDTO> GetRooms(int number, string className
            , int peopleCount, bool inAccessible)
        {
            if (number != 0)
                return db.Rooms.GetAll().Where(x => x.Number == number)
                    .Select(BLLService.BLLMapper.RoomMap);

            IEnumerable<Room> list = db.Rooms.GetAll().OrderBy(x => x.Number).ToList();
            if (className != null && className != "")
                list = list.Where(x => x.RoomClass.Name == className);

            if (peopleCount != 0)
                list = list.Where(x => x.PeopleCount == peopleCount);

            if (inAccessible)
                list = list.Where(x => !x.IsAccessible);

            var result = new List<RoomDTO>();
            foreach(var room in list)
            {
                var roomDTO = BLLService.BLLMapper.RoomMap(room);
                if (!room.IsAccessible)
                    roomDTO.AppartmentStatus = RoomStatus.INACCESSIBLE;
                result.Add(roomDTO);
            }

            return result;
        }

        public RoomDTO GetCertainRoom(int id)
        {
            var room = db.Rooms.Get(id);
            if (room is null)
                return null;
            return BLLService.BLLMapper.RoomMap(room);
        }

        public OperationMessage ChangeRoom(RoomDTO roomDTO)
        {
            if (roomDTO is null)
                return new OperationMessage()
                { IsPositive = false, Text = "Parameter \"room\" was null" };

            var room = db.Rooms.Get(roomDTO.Id);
            if (room.Number != roomDTO.Number && IsRoomNumber(room.Number))
                    return new OperationMessage()
                    { IsPositive = false, Text = $"Room which has number {room.Number} exists" };
            room.Number = roomDTO.Number;

            if(room.RoomClass.Name != roomDTO.RoomClassName)
            {
                var roomClass = db.RoomClasses.GetAll().Where(x => x.Name == roomDTO.RoomClassName).Single();
                room.RoomClass = roomClass ?? room.RoomClass;
            }

            if( roomDTO.PeopleCount > 0 && roomDTO.PeopleCount < 11)
            {
                room.PeopleCount = roomDTO.PeopleCount;
            }

            room.IsAccessible = roomDTO.AppartmentStatus == RoomStatus.AVAILABLE;

            db.Rooms.Update(room);
            db.Commit();
            return new OperationMessage()
            { IsPositive = true, Text = $"Room which has number {room.Number} has been changed!" };
        }

    }
}
