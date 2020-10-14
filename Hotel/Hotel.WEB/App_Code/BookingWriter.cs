using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hotel.WEB.Models.Profile;

namespace Hotel.WEB.App_Code
{
    public static class BookingWriter
    {
        public enum BookingStatus {CONFIRM, ACTIVE,PAST}
        public static MvcHtmlString CreateBookings(this HtmlHelper html,
            string stylesToContainer, BookingStatus status,
            string stylesToClass, string stylesToPayed,
            string stylesToPast, string stylesToButtonConfirm,
            string stylesToButtonContainer,
            List<BookingViewModel> bookings)
        {
            string result = "";
            //Full filling booking with info
            foreach(var booking in bookings)
            {
                var div = new TagBuilder("div");
                div.AddCssClass(stylesToContainer);
                var form = new TagBuilder("form");
                form.MergeAttribute("action", "/Manage/Index");
                form.MergeAttribute("method", "post");
                var divClass = new TagBuilder("div");
                divClass.AddCssClass(stylesToClass);
                divClass.MergeAttribute
                    ("style", $"color:{booking.RoomColorToDisplay};");
                divClass.SetInnerText(
                    string.Format("{0} class", booking.RoomClassName));
                var divNumber = new TagBuilder("div");
                divNumber.SetInnerText(
                    string.Format("Room №{0}", booking.RoomNumber));
                var divPeople = new TagBuilder("div");
                divPeople.SetInnerText(
                    string.Format("For {0} {1}", booking.PeopleCount,
                    booking.PeopleCount == 1 ? "person" : "people"));

                var divDates = new TagBuilder("div");
                divDates.SetInnerText(
                    string.Format("{0} to {1}", booking.BegginingTime.ToShortDateString()
                    , booking.EndingTime.ToShortDateString()));

                form.InnerHtml = divClass.ToString() + divNumber.ToString()
                    + divPeople.ToString() + divDates.ToString();

                switch (status) //choose logic for status
                {
                    case BookingStatus.ACTIVE:
                        var divPayed = new TagBuilder("div");
                        divPayed.AddCssClass(stylesToPayed);
                        divPayed.SetInnerText("Paid!");
                        form.InnerHtml += divPayed.ToString();
                        break;
                    case BookingStatus.CONFIRM: //adding some interface for non-paid ones
                        var divDeadline = new TagBuilder("div");
                        divDeadline.SetInnerText(
                            string.Format("You must pay until {0}!",
                            booking.DeadLine.ToShortDateString()));
                        var id = new TagBuilder("input");
                        id.MergeAttribute("type", "hidden");
                        id.MergeAttribute("name", "bookingId");
                        id.MergeAttribute("value", booking.Id.ToString());
                        var divInput = new TagBuilder("div");
                        divInput.AddCssClass(stylesToButtonContainer);
                        var submit = new TagBuilder("input");
                        submit.MergeAttribute("type", "submit");
                        submit.AddCssClass(stylesToButtonConfirm);
                        submit.MergeAttribute("value", "Pay and book!");
                        divInput.InnerHtml = submit.ToString();
                        form.InnerHtml += divDeadline.ToString() +
                            divInput.ToString() + id.ToString(); ;
                        break;
                    case BookingStatus.PAST:
                        var divPast = new TagBuilder("div");
                        divPast.AddCssClass(stylesToPast);
                        divPast.SetInnerText("Hope you had great time!");
                        form.InnerHtml += divPast.ToString();
                        break;
                }
                div.InnerHtml = form.ToString();
                result += div.ToString();
            }


            return new MvcHtmlString(result);
        }
    }
}