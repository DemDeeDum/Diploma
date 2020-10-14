using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hotel.BLL.DTOs;
using Hotel.BLL.Models;
using Hotel.DAL.Entities;

namespace Hotel.BLL.Interfaces
{
    public interface IBookingPageService : IDisposable
    {
        /// <summary>
        /// Get all rooms non-deleted ones
        /// </summary>
        /// <returns>Enumerable RoomDTO</returns>
        IEnumerable<RoomDTO> GetAllRooms();
        /// <summary>
        /// Get room count
        /// </summary>
        /// <returns>Room count</returns>
        int GetRoomCount();
        /// <summary>
        /// Get a gap in collection of room
        /// </summary>
        /// <param name="StartPosition">Gap start position</param>
        /// <param name="FinishPosition">Gap finish position</param>
        /// <returns>Enumerable RoomDTO</returns>
        IEnumerable<RoomDTO> GetRoomRange(int StartPosition, int FinishPosition);
        /// <summary>
        /// Get all possible people counts
        /// </summary>
        /// <returns>Enumerable string, each member contains numeral</returns>
        IEnumerable<string> GetPossiblePeopleCounts();
        /// <summary>
        /// Get all possible room class names
        /// </summary>
        /// <returns>Enumerable string</returns>
        IEnumerable<string> GetPossibleRoomClassNames();
        /// <summary>
        /// Get all room classes
        /// </summary>
        /// <returns>Enumerable RoomClassDTO</returns>
        IEnumerable<RoomClassDTO> GetAllRoomClasses();
        /// <summary>
        /// Get all rooms which are available in a set period of time
        /// </summary>
        /// <param name="start">Start date</param>
        /// <param name="finish">Finish date</param>
        /// <returns>Room count</returns>
        int GetRoomCount(DateTime start, DateTime finish);
        /// <summary>
        /// Get sorted gap of the room collection
        /// </summary>
        /// <param name="StartPosition">Start position</param>
        /// <param name="FinishPosition">Finish position</param>
        /// <param name="order">The set of filters</param>
        /// <returns>Enumerableо RoomDTO</returns>
        IEnumerable<RoomDTO> GetRoomRange(int StartPosition
           , int FinishPosition, OrderCreator order);
        /// <summary>
        /// Find a fit room and then book it
        /// If the fit room is not found, the unlucky request will be created
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="ClassName">Class name</param>
        /// <param name="PeopleCount">People count</param>
        /// <param name="start">Start date</param>
        /// <param name="finish">Finish date</param>
        void SearchFitRoom(string userId, string ClassName, byte PeopleCount,
           DateTime start, DateTime finish);
        /// <summary>
        /// Make a booking of the certain room for the certain user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="roomId">Room id</param>
        /// <param name="start">Start date</param>
        /// <param name="finish">Finish date</param>
        /// <param name="IsConfirmed">Is paid?</param>
        void CreateBooking(string userId, int roomId, DateTime start, DateTime finish,
             bool IsConfirmed = true);
        /// <summary>
        /// Get a whole info about the certain room
        /// </summary>
        /// <param name="id">Room id</param>
        /// <returns></returns>
        RoomWholeInfo GetCertainRoom(int id);
        /// <summary>
        /// Get room status during the period of time
        /// </summary>
        /// <param name="id">Room id</param>
        /// <param name="start">Start date</param>
        /// <param name="finish">Finish date</param>
        /// <returns>Status string</returns>
        string GetRoomStatus(int id, DateTime start, DateTime finish);
    }
}
