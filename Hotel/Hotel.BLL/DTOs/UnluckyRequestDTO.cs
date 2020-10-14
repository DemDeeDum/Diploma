using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.DTOs
{
    public class UnluckyRequestDTO
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
