using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Hotel.WEB.Models.Booking
{
   
    public class RequestViewModel
    {
        [Display(Name = "Select avaible people count")]
        public byte PeopleCount { get; set; }
        [Display(Name = "Select avaible room class")]
        public string RoomClassName { get; set; }
        [Display(Name = "Choose booking start date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Choose booking finish date")]
        public DateTime FinishDate { get; set; }
        public List<SelectListItem> RoomClassNames{ get; set; }
        public List<SelectListItem> PossiblePeopleCounts { get; set; }
    }
}