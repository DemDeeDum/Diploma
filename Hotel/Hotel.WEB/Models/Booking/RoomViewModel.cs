using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel.WEB.Models.Booking
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Room number")]
        [Range(1,10000)]
        [Required]
        public int Number { get; set; }
        [Display(Name = "People count")]
        [Range(1, 10)]
        [Required]
        public byte PeopleCount { get; set; }
        [Display(Name = "Existed room classes")]
        [Required]
        public string RoomClassName { get; set; }
        [Display(Name = "Appartment status")]
        [Required]
        public string AppartmentStatus { get; set; }
        public string ClassDisplayColor { get; set; }
        public float Price { get; set; }
    }
}