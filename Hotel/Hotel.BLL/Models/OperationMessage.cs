using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Models
{
    public class OperationMessage
    {
        public bool IsPositive { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return $"{Text}${IsPositive}";
        }
    }
}
