using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.WEB.Models.Booking
{
    public class UserFormViewModel
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public DateTime BegginingDate { get; set; }
        public DateTime EndingDate { get; set; }
    }
}