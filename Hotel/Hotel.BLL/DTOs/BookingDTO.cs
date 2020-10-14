using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.DTOs
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public DateTime BegginingTime { get; set; }
        public DateTime EndingTime { get; set; }
        public string RoomClassName { get; set; }
        public string RoomColorToDisplay { get; set; }
        public int RoomNumber { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime DeadLine { get; set; }
        public byte PeopleCount { get; set; }
    }
}
