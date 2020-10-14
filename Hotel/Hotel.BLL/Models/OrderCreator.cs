using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Models
{
    /// <summary>
    /// Хранение всех фильтров поиска в одном местею
    /// </summary>
    public class OrderCreator 
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
