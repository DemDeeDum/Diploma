using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.WEB.Models.Booking
{
    public class RoomWholeInformationViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string RoomClassName { get; set; }
        public string RoomClassInfo { get; set; }
        public string RoomClassColorToDisplay { get; set; }
        public byte PeopleCount { get; set; }
        public float Price { get; set; }
        public string RoomClassImageAddress { get; set; }
    }
}