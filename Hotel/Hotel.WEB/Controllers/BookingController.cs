// <copyright file="BookingController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.WEB.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using Hotel.BLL.DTOs;
    using Hotel.BLL.Interfaces;
    using Hotel.BLL.Models;
    using Hotel.WEB.Models.Booking;
    using Hotel.WEB.Models.Common;
    using Microsoft.AspNet.Identity;

    public class BookingController : Controller
    {

        // GET: Booking
        private int PageSize = 10;
        private IBookingPageService BookingService { get; }
        public BookingController(IBookingPageService service)
        {
            BookingService = service;
        }

        /// <summary>
        /// Creating settings
        /// Could be used by default value
        /// </summary>
        /// <param name="PeopleFilter"></param>
        /// <param name="RoomClassFilter"></param>
        /// <param name="PriceFilter"></param>
        /// <param name="StatusFilter"></param>
        /// <param name="Descending"></param>
        /// <param name="BeginningTime"></param>
        /// <param name="EndingTime"></param>
        /// <returns></returns>
        private SearchSettings SettingsCreator(bool? PeopleFilter = null, 
            bool? RoomClassFilter = null,
        bool? PriceFilter = null, bool? StatusFilter = null, bool? Descending = null, 
        string BeginningTime = null,
        string EndingTime = null)
        {
            SearchSettings settings;
            if (PeopleFilter != null && DateTime.TryParse(BeginningTime, out DateTime start) &&
                DateTime.TryParse(EndingTime, out DateTime finish) && Descending != null &&
                PriceFilter != null && StatusFilter != null && RoomClassFilter != null)
                settings = new SearchSettings()
                {
                    PeopleFilter = PeopleFilter.Value,
                    RoomClassFilter = RoomClassFilter.Value,
                    PriceFilter = PriceFilter.Value,
                    Descending = Descending.Value,
                    StatusFilter = StatusFilter.Value,
                    BeginningTime = start,
                    EndingTime = finish
                };
            else
                settings = new SearchSettings()
                {
                    BeginningTime = DateTime.Today.AddDays(10),
                    EndingTime = DateTime.Today.AddDays(20)
                };
            return settings;
        }

        /// <summary>
        /// Check the dates for a correct value.
        /// </summary>
        /// <param name="start">Start date</param>
        /// <param name="finish">Finish date</param>
        /// <returns>The list of strings - errors</returns>
        private List<string> ValidateDates(DateTime start, DateTime finish)
        {
            var errorList = new List<string>();
            if (start <= DateTime.Today)
                errorList.Add("Beginning date must be at least tomorrow");

            if (finish < start)
                errorList.Add("Finish date must be bigger than beginning one");

            if (finish.Year - 2 > DateTime.Today.Year)
                errorList.Add("Enter not such a distant date, please");
            return errorList;
        }


        /// <summary>
        /// Creating page with search by parameters.
        /// </summary>
        /// <param name="id">Page id or the number of page</param>
        /// <param name="settings">Search settings</param>
        /// <returns>Current page with the content</returns>
        private Page<RoomViewModel, RequestViewModel> PageCreator(int id, 
            SearchSettings settings)
        {
            Page<RoomViewModel, RequestViewModel> page
                = new Page<RoomViewModel, RequestViewModel>();

            page.CalculatePositions(out int StartPosition
                , out int FinishPosition, BookingService.GetRoomCount()
                , PageSize, id);

            if (page.PageNumber == -1)
                return page;
            page.ItemList =
                    BookingService.GetRoomRange(StartPosition, FinishPosition,
                    Mapper.Map<SearchSettings, OrderCreator>(settings))
                    .Select(Mapper.Map<RoomDTO, RoomViewModel>).ToList(); //gettings sorted gap

            page.InputModel.RoomClassNames = new List<SelectListItem> //gettings possible values to display
                (BookingService.GetPossibleRoomClassNames()
                .Select(Mapper.Map<string, SelectListItem>));

            page.InputModel.PossiblePeopleCounts = new List<SelectListItem>
                (BookingService.GetPossiblePeopleCounts()
                .Select(Mapper.Map<string, SelectListItem>));

            page.SearchInfo = settings;


            ViewBag.RoomClasses = BookingService.GetAllRoomClasses()
                .Select(Mapper.Map<RoomClassDTO, RoomClassViewModel>).ToList();


            return page;
        }

        [HttpPost]
        public ActionResult Search(FormCollection items,
            DateTime searchbegindate, DateTime searchfinishdate)
        {
            SearchSettings settings = new SearchSettings()
            {
                BeginningTime = DateTime.Today.AddDays(10),
                EndingTime = DateTime.Today.AddDays(20)
            };
            var errorList = ValidateDates(searchbegindate, searchfinishdate);

            if (errorList.Count == 0)//getting filters
            {
                settings.PriceFilter = items["pricef"] == "true,false";
                settings.PeopleFilter = items["peoplecountf"] == "true,false";
                settings.RoomClassFilter = items["classf"] == "true,false";
                settings.StatusFilter = items["statusf"] == "true,false";
                settings.Descending = items["descendingf"] == "true,false";
                settings.BeginningTime = searchbegindate;
                settings.EndingTime = searchfinishdate;
            }
            else
                ViewBag.Errors = errorList;
            var page = PageCreator(1, settings);


            if (page.PageNumber == -1)
                return RedirectToAction("ErrorPage", "Public");

            return View("~/Views/Booking/BookingPage.cshtml", page);
        }

        [HttpGet]
        public ActionResult BookingPage(bool? PeopleFilter, bool? RoomClassFilter,
        bool? PriceFilter, bool? StatusFilter, bool? Descending, string BeginningTime,
        string EndingTime, int id = 1, bool booked = false, int roomNumber = 1)
        {
           
            var page = PageCreator(id, SettingsCreator(PeopleFilter, RoomClassFilter,
        PriceFilter, StatusFilter, Descending, BeginningTime,
        EndingTime));
            if (booked)
                ViewBag.Message = $"Room №{roomNumber} was booked!";

            if (page.PageNumber == -1)
                return RedirectToAction("ErrorPage", "Public");

            return View(page);
        }

        [Authorize (Roles ="user")]
        [HttpPost]
        public ActionResult BookingPage(Page<RoomViewModel, RequestViewModel> item,
            bool? PeopleFilter, bool? RoomClassFilter,
        bool? PriceFilter, bool? StatusFilter, bool? Descending, string BeginningTime,
        string EndingTime, int id = 1)
        {
            bool bad = false;
            //Validate Model
            if (item.InputModel.StartDate.Year - 1 > DateTime.Today.Year)
            {
                bad = true;
                ModelState.AddModelError("StartDate", "You cannot book a room in such a distant future");
            }
            if (item.InputModel.FinishDate.Year - 1 > DateTime.Today.Year)
            {
                bad = true;
                ModelState.AddModelError("FinishDate", "You cannot book a room in such a distant future");
            }
            if (DateTime.Today.AddDays(3) > item.InputModel.StartDate)
            {
                bad = true;
                ModelState.AddModelError("StartDate", "You can book at least 3 days in advance");
            }
            if(item.InputModel.StartDate > item.InputModel.FinishDate)
            {
                bad = true;
                ModelState.AddModelError("FinishDate", "Finish date must be equal or bigger than initial one");
            }

            if (!bad)
            {
                ViewBag.FeedBack = "Your request will be considered!";
                BookingService.SearchFitRoom(User.Identity.GetUserId(), item.InputModel.RoomClassName,
                    item.InputModel.PeopleCount, item.InputModel.StartDate, item.InputModel.FinishDate);
            }
            var page = PageCreator(id, SettingsCreator(PeopleFilter, RoomClassFilter,
        PriceFilter, StatusFilter, Descending, BeginningTime,
        EndingTime)) ;

            if (page.PageNumber == -1)
                return RedirectToAction("ErrorPage", "Public");
            return View(page);

        }

        [Authorize(Roles = "user")]
        [HttpGet]
        public ActionResult BookingRoom(int? id, string start, string finish)
        {
            if (id is null)
                return RedirectToAction("ErrorPage", "Public");
            var roomInfo = BookingService.GetCertainRoom(id.Value);
            if(roomInfo is null)
                return RedirectToAction("ErrorPage", "Public");

            if (!(start is null) && DateTime.TryParse(start, out DateTime startDate)
                && !(finish is null) && DateTime.TryParse(finish, out DateTime finishDate))
            {
                ViewBag.StartDate = startDate;
                ViewBag.FinishDate = finishDate;
                ViewBag.Status = BookingService.GetRoomStatus(id.Value, startDate, finishDate);
            }
            return View(Mapper.Map<RoomWholeInfo, RoomWholeInformationViewModel>(roomInfo));
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public ActionResult BookingRoom(int? id, string start, string finish, bool different = true)
        {
            if (!(start is null) && DateTime.TryParse(start, out DateTime startDate)
                && !(finish is null) && DateTime.TryParse(finish, out DateTime finishDate)
                && id != null && BookingService.GetCertainRoom(id.Value) != null) //if input is valid
            {
                var errorList = ValidateDates(startDate, finishDate); //if dates is correct
                if (errorList.Count > 0)
                {
                    ViewBag.Errors = errorList;
                    ViewBag.StartDate = startDate;
                    ViewBag.FinishDate = finishDate;
                    ViewBag.Status = BookingService.GetRoomStatus(id.Value, startDate, finishDate);
                    return View(Mapper.Map<RoomWholeInfo, RoomWholeInformationViewModel>(BookingService.GetCertainRoom(id.Value)));
                }

                BookingService.CreateBooking(User.Identity.GetUserId(), id.Value,
                startDate, finishDate);
            return Redirect($"/Booking/BookingPage/1/?booked=true" +
                        $"&roomNumber={BookingService.GetCertainRoom(id.Value).Number}");
            }
            return Redirect("/Booking/BookingPage");
        }

        [Authorize(Roles = "user")]
        public ActionResult UserForm(int? id, string start, string finish)
        {

            var startDate = DateTime.Parse(start);
            var finishDate = DateTime.Parse(finish);
            var errorList = ValidateDates(startDate, finishDate);
            if (errorList.Count == 0)
            {
                return PartialView(new UserFormViewModel()
                {
                    Status = BookingService.GetRoomStatus(id.Value, startDate, finishDate),
                    EndingDate = finishDate,
                    BegginingDate = startDate
                });
            }
            
            return PartialView(new UserFormViewModel()
            {
                Status = "invalid input",
                EndingDate = finishDate,
                BegginingDate = startDate
            });
        }

    }
}