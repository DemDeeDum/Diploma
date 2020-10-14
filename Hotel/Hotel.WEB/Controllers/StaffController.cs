using Hotel.BLL.Interfaces;
using Hotel.WEB.Models.Booking;
using Hotel.WEB.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Hotel.BLL.DTOs;
using Hotel.BLL.Models;


namespace Hotel.WEB.Controllers
{
    [Authorize (Roles = "staff")]
    public class StaffController : Controller
    {
        private IStaffService staffService;
        public StaffController(IStaffService staffService)
        {
            this.staffService = staffService;
        }

        public Page<RoomViewModel, RoomViewModel> GetPage(
            RoomViewModel certainRoom = null)
        {
            var page = new Page<RoomViewModel, RoomViewModel>();

            if (!(certainRoom is null))
                page.InputModel = certainRoom;

            return page;
        }
        
        [HttpGet]
        public ActionResult AddRooms()
        {
            ViewBag.RoomClasses = new List<SelectListItem>
                (staffService.GetPossibleRoomClassNames()
                .Select(Mapper.Map<string, SelectListItem>));
            return View();
        }

        [HttpPost]
        public ActionResult AddRooms(RoomViewModel room)
        {
            ViewBag.RoomClasses = new List<SelectListItem>
                (staffService.GetPossibleRoomClassNames()
                .Select(Mapper.Map<string, SelectListItem>));
            if (ModelState.IsValid)
                ViewBag.Message = Mapper.Map<OperationMessage, LogicMessage>(staffService.AddNewRoom
                        (Mapper.Map<RoomViewModel, RoomDTO>(room)));
            return View();
        }

        public ActionResult NumberValidation(string number)
        {
            if (int.TryParse(number, out int res) &&
                staffService.IsRoomNumber(res))
                return Content("<span>Room which has this number exists</span>");
            return Content("");
        }

        public ActionResult SubmitChangeState(string number)
        {
            if (int.TryParse(number, out int res) &&
                staffService.IsRoomNumber(res))
                return Content("<input type=\"submit\" " +
                    "value=\"Create\"" +
                    "class=\"btn btn-dark\" disabled id=\"change-room\" />");
            return Content("<input type=\"submit\" " +
                    "value=\"Create\"" +
                    "class=\"btn btn-dark\" id=\"change-room\" />");
        }

        public ActionResult ManageRooms()
        {
            ViewBag.RoomClasses = new List<SelectListItem>
                  (staffService.GetPossibleRoomClassNames()
                  .Select(Mapper.Map<string, SelectListItem>)).ToList();
            return View();
        }



        [HttpPost]
        public ActionResult Rooms(string Number, string PeopleCount,
            string RoomClassName, string SearchInAccessible)
        {
            int number = 0;
            int peopleCount = 0;
            if (Number != null && Number != "")
            {
                if (int.TryParse(Number, out int res))
                {
                    number = res;
                }
                else
                {
                    ViewBag.NumberError = "Incorrect number input";
                }
            }

            if (PeopleCount != null && PeopleCount != "")
            {
                if (int.TryParse(PeopleCount, out int res))
                {
                    peopleCount = res;
                }
                else
                {
                    ViewBag.PeopleError = "Incorrect people count input";
                }
            }
            return View(staffService.GetRooms(number, RoomClassName, peopleCount
                , SearchInAccessible == "on")
                .Select(Mapper.Map<RoomDTO, RoomViewModel>).ToList());
        }

        public ActionResult EditRoom(int? id)
        {
            ViewBag.RoomClasses = new List<SelectListItem>
               (staffService.GetPossibleRoomClassNames()
               .Select(Mapper.Map<string, SelectListItem>));
            if (id.HasValue)
                return View(
                    Mapper.Map<RoomDTO, RoomViewModel>
                    (staffService.GetCertainRoom(id.Value)));
            return Redirect("/Staff/ManageRooms");
        }

        [HttpPost]
        public ActionResult EditRoom(RoomViewModel room)
        {
            ViewBag.RoomClasses = new List<SelectListItem>
               (staffService.GetPossibleRoomClassNames()
               .Select(Mapper.Map<string, SelectListItem>));
            if (ModelState.IsValid)
            {
                ViewBag.Message = Mapper.Map<OperationMessage, LogicMessage>
                    (staffService.ChangeRoom
                    (Mapper.Map<RoomViewModel, RoomDTO>(room)));
            }
            return View(room);
        }
    }
}