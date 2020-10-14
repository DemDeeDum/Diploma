using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Hotel.WEB.Models.Booking;

namespace Hotel.WEB.App_Code
{
    public static class PriceListCreator
    {
        public static MvcHtmlString CreatePriceList(this HtmlHelper html,
           List<RoomClassViewModel> roomClasses, string stylesToNames, byte maxPeopleCount)
        {
            var table = new TagBuilder("table"); //creating a table
            var tr = new TagBuilder("tr");
            var td = new TagBuilder("td");
            td.MergeAttribute("colspan", "2");
            td.SetInnerText("Price per day");
            tr.InnerHtml += td.ToString();
            table.InnerHtml += tr.ToString();
            foreach (var roomClass in roomClasses) //filling by info
            {
                var trClass = new TagBuilder("tr");
                var tdName = new TagBuilder("td");
                tdName.AddCssClass(stylesToNames);
                tdName.MergeAttribute("style", $"color: {roomClass.DisplayColor}");
                var tdPrice = new TagBuilder("td");
                tdName.SetInnerText(roomClass.Name);
                tdPrice.SetInnerText(string.Format("{0}$ - {1}$",
                    roomClass.Price, roomClass.Price * maxPeopleCount));
                trClass.InnerHtml += tdName.ToString() + tdPrice.ToString();
                table.InnerHtml += trClass.ToString();
            }

            return new MvcHtmlString(table.ToString());
        }
    }
}