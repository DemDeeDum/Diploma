using Hotel.BLL.Interfaces;
using Hotel.DAL.Context;
using Hotel.DAL.Interfaces;
using Hotel.DAL.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Services
{
    public class LogService : ILogService
    {
        private IUoW<ApplicationDbContext> db { get; }
        public LogService(IUoW<ApplicationDbContext> db)
        {
            this.db = db ?? new UoW();
        }

        public LogService()
        {
            db = new UoW();
        }

        void ILogService.AddLog(DTOs.LogDTO obj)
        {
            var log = BLLService.BLLMapper.LogMap(obj);      
            try
            {
                db.Logs.Create(log);
                db.Commit();
            }
            catch
            {

            }
        }

        void IDisposable.Dispose()
        {
            db.Dispose();
        }
    }
}
