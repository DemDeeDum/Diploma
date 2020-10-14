using Hotel.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hotel.WEB.Models.Common;
using AutoMapper;
using Hotel.BLL.DTOs;
using Hotel.BLL.Services;

namespace Hotel.WEB.Filters
{
    public class LogFilter : FilterAttribute, IActionFilter
    {
        private ILogService logService;
        public LogFilter(ILogService service)
        {
            logService = service ?? new LogService();
        }
        public LogFilter()
        {
            logService = new LogService();
        }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var UserName = filterContext.HttpContext.User.Identity.Name;
            var URL = filterContext.HttpContext.Request.RawUrl;
            var Browser = filterContext.HttpContext.Request.Browser;
            var exeption = filterContext.Exception;
            logService.AddLog(Mapper.Map<LogViewModel, LogDTO>(new LogViewModel()
            {
                UserName = UserName == "" ? "-" : UserName,
                BrowserName = Browser.Browser,
                BrowserVersion = Browser.MinorVersionString,
                JavasriptVersion = Browser.JScriptVersion.ToString(),
                IsMobile = Browser.IsMobileDevice,
                Platform = Browser.Platform,
                Exeption = exeption is null ? "-" : exeption.Message,
                URL = URL,
                Date = DateTime.Now
            }));
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }
}