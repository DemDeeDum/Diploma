using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.BLL.DTOs;

namespace Hotel.BLL.Interfaces
{
    public interface ILogService : IDisposable
    {
        /// <summary>
        /// Add log to the database.
        /// </summary>
        /// <param name="obj">Log object</param>
        void AddLog(LogDTO obj);
    }
}
