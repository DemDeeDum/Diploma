using System;
using Hotel.BLL.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.BLL.Models;

namespace Hotel.BLL.Interfaces
{
    public interface IAdminService : IDisposable
    {
        /// <summary>
        /// Get all requests
        /// </summary>
        ///<returns>Enumerable UnluckyRequestDTO</returns>
        IEnumerable<UnluckyRequestDTO> GetAllUnluckyRequests();
        /// <summary>
        /// Get all possible people counts
        /// </summary>
        /// <returns>Enumerable string</returns>
        IEnumerable<string> GetPossiblePeopleCounts();
        /// <summary>
        /// Get all possible room class names
        /// </summary>
        /// <returns>Enumerable string</returns>
        IEnumerable<string> GetPossibleRoomClassNames();
        /// <summary>
        /// Get rooms by search parameters
        /// </summary>
        /// <param name="ClassName">Class name</param>
        /// <param name="PeopleCount">People counts</param>
        /// <param name="start">Start date</param>
        /// <param name="finish">Finish date</param>
        /// <returns>Enumerable RoomDTO</returns>
        IEnumerable<RoomDTO> SearchRooms(string ClassName, byte PeopleCount,
            DateTime start, DateTime finish);
        /// <summary>
        /// Answer to request by creating Booking
        /// </summary>
        /// <param name="requestId">Request Id</param>
        /// <param name="roomId">Room Id</param>
        /// <param name="start">Start date</param>
        /// <param name="finish">Finish date</param>
        /// <returns>Operation status</returns>
        OperationMessage AnswerToUnluckyRequest(int requestId, int roomId
            , DateTime start, DateTime finish);
        /// <summary>
        /// Get all users having set role
        /// </summary>
        /// <param name="role">Role</param>
        /// <returns>Enumerable ApplicationUserDTO</returns>
        IEnumerable<ApplicationUserDTO> GetUsers(string role = null);
        /// <summary>
        /// Add the user to the defined role 
        /// And delete from user role
        /// </summary>
        /// <param name="Role">Role name</param>
        /// <param name="UserName">User name</param>
        /// <returns>Operation status</returns>
        OperationMessage AddToRole(string UserName, string Role);
        /// <summary>
        /// Soft delete/ban user
        /// </summary>
        /// <param name="UserName">User name</param>
        /// <returns>Operation status</returns>
        OperationMessage DeleteUser(string UserName);
        /// <summary>
        /// Dismiss user from a staff role.
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        OperationMessage DownGradeFromStaff(string UserName);
        OperationMessage RestoreUser(string UserName);
        IEnumerable<LogDTO> GetLastLogs();
        IEnumerable<LogDTO> GetLogs(string userName, DateTime? start
    , DateTime? finish, bool? searchExept, string URL);

        void DeleteFromAdminRole(string UserName);
    }
}
