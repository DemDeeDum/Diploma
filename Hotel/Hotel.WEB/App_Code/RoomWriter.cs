using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hotel.WEB.Models.Booking;

namespace Hotel.WEB.App_Code
{
    public static class RoomWriter
    {
        public static MvcHtmlString CreateRooms(this HtmlHelper html,
            List<RoomViewModel> rooms, string styleToRoomContainer,
            string styleToRoomInfo, string styleToRoomStatus, 
            string styleToRoomClass, DateTime start, DateTime finish)
        {
            string result = "";
            
            var article = new TagBuilder("article"); 
            article.AddCssClass(styleToRoomContainer);
            var spanGeneralInfo = new TagBuilder("span");
            spanGeneralInfo.AddCssClass(styleToRoomInfo);
;
            foreach(var room in rooms) //just filling by info
            {
                var submit = new TagBuilder("a");
                submit.MergeAttribute("href", string.Format("/Booking/BookingRoom/{0}/?start={1}" +
                    "&finish={2}",
                    room.Id, start, finish));
                var spanClass = new TagBuilder("span");
                spanClass.AddCssClass(styleToRoomClass);
                spanClass.MergeAttribute("style", $"color: {room.ClassDisplayColor};");
                spanClass.SetInnerText(room.RoomClassName);

                var spanRoomStatus = new TagBuilder("span");
                spanRoomStatus.AddCssClass(styleToRoomStatus);
                spanRoomStatus.SetInnerText(room.AppartmentStatus);
                switch(room.AppartmentStatus.ToLower()) //check for status
                {
                    case "available":
                        spanRoomStatus.MergeAttribute
                            ("style", "color:green;");
                        break;
                    case "booked":
                        spanRoomStatus.MergeAttribute
                            ("style", "color:lightgray;");
                        break;
                    case "occupied":
                        spanRoomStatus.MergeAttribute
                            ("style", "color:orange;");
                        break;
                    case "inaccessible":
                        spanRoomStatus.MergeAttribute
                            ("style", "color:red;");
                        break;
                }

                spanGeneralInfo.SetInnerText(string.Format(
                    "№{2} for {0} {1} {3}$", room.PeopleCount,
                    room.PeopleCount == 1 ? "person" : "people", room.Number,
                    room.Price));

                article.InnerHtml += spanClass.ToString()
                    + spanGeneralInfo.ToString() + spanRoomStatus.ToString();
                submit.InnerHtml = article.ToString();
                result += submit.ToString();
                article.InnerHtml = "";
                spanRoomStatus.InnerHtml = "";
                spanClass.InnerHtml = "";
                spanGeneralInfo.InnerHtml = "";
            }

            return new MvcHtmlString(result) ;
        }
    }
}