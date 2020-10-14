using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hotel.WEB.App_Code
{
    public static class SmallHelper
    {
        /// <summary>
        /// Creating a date string which could be read by input[type="date"].
        /// </summary>
        /// <param name="html"></param>
        /// <param name="date">Date to convert</param>
        /// <returns>The readable for an input[type="date"] date string</returns>
        public static string CreateDateString(this HtmlHelper html,
           DateTime date)
        {
            return string.Format("{0}-{1}-{2}",date.Year,
                date.Month < 10 ?"0" + date.Month.ToString()
                : date.Month.ToString(),
                date.Day < 10 ? "0" + date.Day.ToString()
                : date.Day.ToString());
        }

        /// <summary>
        /// Create img tag.
        /// </summary>
        /// <param name="html"></param>
        /// <param name="address">The relative address of the image</param>
        /// <param name="stylesToImg"></param>
        /// <returns>MvcHtmlString of img tag.</returns>
        public static MvcHtmlString CreateImage(this HtmlHelper html,
           string address, string stylesToImg)
        {
            var img = new TagBuilder("img");
            img.AddCssClass(stylesToImg);
            img.MergeAttribute("alt", "room-photo");
            img.MergeAttribute("src", address);
            return new MvcHtmlString(img.ToString());
        }

        public static MvcHtmlString CreateStatus(this HtmlHelper html,
          string Status, string stylesToStatus)
        {
            var span = new TagBuilder("span");

            span.SetInnerText(Status);
            switch (Status.ToLower())
            {
                case "available":
                    span.MergeAttribute
                        ("style", "color:green;");
                    break;
                case "booked":
                    span.MergeAttribute
                        ("style", "color:lightgray;");
                    break;
                case "occupied":
                    span.MergeAttribute
                        ("style", "color:orange;");
                    break;
                case "inaccessible":
                    span.MergeAttribute
                        ("style", "color:red;");
                    break;
                default:
                    span.MergeAttribute
                            ("style", "color:red;");
                    return new MvcHtmlString(span.ToString());
            }
            span.AddCssClass(stylesToStatus);
            return new MvcHtmlString(span.ToString());
        }

        public static MvcHtmlString CreateBookingRoomButton(this HtmlHelper html,
          string Status, string stylesToSubmit, string formId)
        {

            var submit = new TagBuilder("input");
            submit.MergeAttribute("value", "Pay and book it!");
            submit.MergeAttribute("type", "submit");
            submit.MergeAttribute("form", formId);
            submit.AddCssClass(stylesToSubmit);
            if (Status.ToLower() != "available")
                submit.MergeAttribute("disabled", "disabled");


            return new MvcHtmlString(submit.ToString());
        }

    }
}