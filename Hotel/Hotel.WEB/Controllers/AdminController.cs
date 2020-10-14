using AutoMapper;
using Hotel.BLL.DTOs;
using Hotel.BLL.Interfaces;
using Hotel.BLL.Models;
using Hotel.WEB.Models.Admin;
using Hotel.WEB.Models.Booking;
using Hotel.WEB.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Hotel.WEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private IAdminService adminService;
        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }
        // GET: Admin
        /// <summary>
        /// Add the user to an admin role.
        /// Delete from the user role.
        /// </summary>
        /// <param name="name">User name</param>
        /// <returns>Main view</returns>
        public ActionResult UpdradeToAdmin(string name)
        {
            return RedirectToAction("UserManagment", new { message = adminService.AddToRole(name, "admin").ToString() });
        }
        /// <summary>
        /// Add the user to an admin role.
        /// Delete from the user role.
        /// </summary>
        /// <param name="name">User name</param>
        /// <returns>Main view</returns>
        public ActionResult UpdradeToStaff(string name)
        {
            return RedirectToAction("UserManagment", new { message = adminService.AddToRole(name, "staff").ToString() });
        }
        /// <summary>
        /// Soft delete to user.
        /// </summary>
        /// <param name="name">User Name</param>
        /// <returns></returns>
        public ActionResult DeleteUser(string name)
        {
            return RedirectToAction("UserManagment", new { message = adminService.DeleteUser(name).ToString() });
        }

        public ActionResult DowngradeFromStaff(string name)
        {
            return RedirectToAction("UserManagment", new { message = adminService.DownGradeFromStaff(name).ToString() });
        }

        public ActionResult RestoreUser(string name)
        {
            return RedirectToAction("UserManagment", new { message = adminService.RestoreUser(name).ToString() });
        }

        public ActionResult UserManagment(string message, string userRole = "user")
        {
            if (!(message is null))
            {
                var list = message.Split('$').ToList();
                var logicMessage = new LogicMessage()
                {
                    Text = list[0],
                    IsPositive = bool.Parse(list[1])
                };
                ViewBag.Message = logicMessage;
            }
            ViewData["userCategory"] = new List<SelectListItem>() {
                new SelectListItem() { Text = "Users", Value = "user"},
                new SelectListItem() {Text = "Staff", Value = "staff"},
                new SelectListItem() {Text = "Deleted", Value = "deleted"}
            };
            return View(new UserManagmentViewModel()
            {
                Admins = adminService.GetUsers("admin") //admins
                .Select(Mapper.Map<ApplicationUserDTO, UserViewModel>).ToList(),
                Users = adminService.GetUsers(userRole) //users
                .Select(Mapper.Map<ApplicationUserDTO, UserViewModel>).ToList()
            });
        }

        public ActionResult UserList(string userRole)
        {
            return PartialView(adminService.GetUsers(userRole)
                .Select(Mapper.Map<ApplicationUserDTO, UserViewModel>).ToList());
        }

        /// <summary>
        /// Creating page for settings. 
        /// It could be default.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private Page<UnluckyRequestViewModel, RequestViewModel> PageCreator(
            RequestViewModel request = null)
        {
            Page<UnluckyRequestViewModel, RequestViewModel> page
                = new Page<UnluckyRequestViewModel, RequestViewModel>();

            if (request is null)
                request = new RequestViewModel()
                {
                    StartDate = DateTime.Today.AddDays(10),
                    FinishDate = DateTime.Today.AddDays(20),
                    PeopleCount = 1,
                    RoomClassName = "royal"
                };

            page.ItemList = adminService.GetAllUnluckyRequests()
                .Select(Mapper.Map<UnluckyRequestDTO, UnluckyRequestViewModel>).ToList();

            page.InputModel = request;

            ViewBag.Rooms = adminService.SearchRooms(request.RoomClassName,
                request.PeopleCount, request.StartDate, request.FinishDate)
                .Select(Mapper.Map<RoomDTO, RoomViewModel>).ToList();

            page.InputModel.RoomClassNames = new List<SelectListItem>
                (adminService.GetPossibleRoomClassNames()
                .Select(Mapper.Map<string, SelectListItem>));

            page.InputModel.PossiblePeopleCounts = new List<SelectListItem>
                (adminService.GetPossiblePeopleCounts()
                .Select(Mapper.Map<string, SelectListItem>));



            return page;
        }

        /// <summary>
        /// Validate the radio buttons.
        /// </summary>
        /// <param name="roomId">Room id</param>
        /// <param name="UserName">User id</param>
        /// <returns>The list of string - error text</returns>
        private List<string> ValidateRadios(int? roomId, int? UserName)
        {
            var errorList = new List<string>();

            if (roomId != null && roomId.Value == -1)
                errorList.Add("You did not select a room");

            if (UserName != null && UserName.Value == -1)
                errorList.Add("You did not select a request");

            return errorList;
        }

        /// <summary>
        /// Validate dates.
        /// </summary>
        /// <param name="StartDate">Start date</param>
        /// <param name="FinishDate">Finish date</param>
        /// <returns>The list of string - error text</returns>
        private List<string> ValidateDates(DateTime StartDate,
            DateTime FinishDate)
        {
            var errorList = new List<string>();

            if (StartDate <= DateTime.Today)
                errorList.Add("Beginning date must be at least tomorrow");

            if (FinishDate < StartDate)
                errorList.Add("Finish date must be bigger than beginning one");

            if (FinishDate.Year - 2 > DateTime.Today.Year)
                errorList.Add("Enter not such a distant date, please");

            return errorList;
        }

        public ActionResult DeleteFromAdmin(string name)
        {
            adminService.DeleteFromAdminRole(name);
            return RedirectToAction("UserManagment");
        }

        [HttpGet]
        public ActionResult RequestManagment()
        {
            return View(PageCreator());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequestManagment(
            FormCollection items,
            Page<UnluckyRequestViewModel, RequestViewModel> page,
            string action, DateTime? searchfinishdate, DateTime? searchbegindate
            , int? roomCount, int? requestCount, LogicMessage message)
        {
            if (roomCount is null || requestCount is null ||
                searchbegindate is null || searchfinishdate is null) //check for unexpected
            {
                ViewBag.Message = new LogicMessage()
                {
                    IsPositive = false,
                    Text = "Input data was invalid"
                };
                return View(PageCreator());
            }

            page.InputModel.StartDate = searchbegindate.Value;
            page.InputModel.FinishDate = searchfinishdate.Value;

            int? roomId = null;
            int? requestId = null;

            var ErrorList = new List<string>();

            ErrorList.AddRange(ValidateDates(page.InputModel.StartDate
, page.InputModel.FinishDate));

            if (action == "Answer")
            {
                requestId = -1;
                for (int i = 0; i < requestCount; i++) //searching choosed radios
                    if (items[$"{i}u"] == "on")
                    {
                        if (int.TryParse(items[$"{i}ureqid"], out int res))
                            requestId = res;
                        break;
                    }
                roomId = -1;
                for (int i = 0; i < roomCount; i++)
                    if (items[$"{i}r"] == "on")
                    {
                        if (int.TryParse(items[$"{i}rid"], out int res))
                            roomId = res;
                        break;
                    }
                ErrorList.AddRange(ValidateRadios(roomId, requestId));
                if (ErrorList.Count == 0)
                {
                    ViewBag.Message = Mapper.Map < OperationMessage, LogicMessage > (adminService.AnswerToUnluckyRequest
                        (requestId.Value, roomId.Value,
                        page.InputModel.StartDate,
                        page.InputModel.FinishDate));
                }
            }

            if (ErrorList.Count == 0)
                return View(PageCreator(page.InputModel));

            ViewBag.Errors = ErrorList;
            return View(PageCreator());
        }


        public ActionResult Logs ()
        {
            return View(new Page<LogViewModel, LogPageInputViewModel>()
            {
                ItemList = adminService.GetLastLogs()
                .Select(Mapper.Map<LogDTO, LogViewModel>).ToList()
            });
        }

        [HttpPost]
        public ActionResult LogList(string StartDate, string FinishDate, string UserName,
            string SearchErrors, string URL)
        {
            DateTime? start = new DateTime?();
            DateTime? finish = new DateTime?();
            if (StartDate != null && DateTime.TryParse(StartDate, out DateTime resS))
                start = resS;

            if (FinishDate != null && DateTime.TryParse(FinishDate, out DateTime resF))
                finish = resF;

            return View(new List<LogViewModel>(adminService.GetLogs
                (UserName, start, finish,!( SearchErrors is null), URL)
                .Select(Mapper.Map<LogDTO, LogViewModel>)))  ;
        }
    }
}