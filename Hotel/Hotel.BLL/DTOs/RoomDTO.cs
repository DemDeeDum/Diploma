using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.BLL.Enums;

namespace Hotel.BLL.DTOs
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public byte PeopleCount { get; set; }
        public string RoomClassName { get; set; }
        public RoomStatus AppartmentStatus { get; set; }
        public string ClassDisplayColor { get; set; }
        public float Price { get; set; }
    }
}
