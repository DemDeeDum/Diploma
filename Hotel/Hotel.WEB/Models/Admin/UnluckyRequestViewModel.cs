using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.WEB.Models.Admin
{
    public class UnluckyRequestViewModel
    {
        public int UnluckyRequestId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string RoomClassName { get; set; }
        public byte PeopleCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
    }
}