using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.WEB.Models.Common
{
    public class SearchSettings
    {
        public bool PeopleFilter { get; set; }
        public bool RoomClassFilter { get; set; }
        public bool PriceFilter { get; set; }
        public bool StatusFilter { get; set; }
        public bool Descending { get; set; }
        public DateTime BeginningTime { get; set; }
        public DateTime EndingTime { get; set; }
    }
}