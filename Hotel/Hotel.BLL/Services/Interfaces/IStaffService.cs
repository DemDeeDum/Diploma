using Hotel.BLL.DTOs;
using Hotel.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Interfaces
{
    public interface IStaffService
    {
        IEnumerable<string> GetPossibleRoomClassNames();
        OperationMessage AddNewRoom(RoomDTO room);
        bool IsRoomNumber(int number);
        IEnumerable<RoomDTO> GetRooms(int number, string className
            , int peopleCount, bool inAccessible);
        RoomDTO GetCertainRoom(int id);
        OperationMessage ChangeRoom(RoomDTO roomDTO);
    }
}
