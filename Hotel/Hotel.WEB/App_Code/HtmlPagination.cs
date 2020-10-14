using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Hotel.WEB.App_Code
{
    public static class HtmlPaginationBuilder
    {
        public static MvcHtmlString CreatePagination(this HtmlHelper html,
            int pagesCount, int currentPage, string linkBeginning, string linkEnd, string stylesToActive,
            string stylesToDefault)
        {
            string nav = "";
            var div = new TagBuilder("div");
            div.AddCssClass(stylesToDefault);
            var activeDiv = new TagBuilder("div");
            activeDiv.AddCssClass(stylesToActive);
            var aFirst = new TagBuilder("a");

            aFirst.MergeAttribute("href", linkBeginning + "1" + linkEnd); //first button is forever
            aFirst.SetInnerText("1");
            if (currentPage == 1) //check if first is current
            {
                activeDiv.InnerHtml = aFirst.ToString();
                nav += activeDiv.ToString();
            }
            else
            {
                div.InnerHtml = aFirst.ToString();
                nav += div.ToString();
            }


            if (currentPage >= 4) //if current is big enough we could add dotes
            {
                div.InnerHtml = "...";
                nav += div.ToString();
            }

            if (currentPage > 2) //if current is bigger than two we could add the previous ref
            {
                var aSecond = new TagBuilder("a");
                aSecond.MergeAttribute("href", linkBeginning + (currentPage - 1).ToString() + linkEnd);
                aSecond.SetInnerText((currentPage - 1).ToString());
                div.InnerHtml = aSecond.ToString();
                nav += div.ToString();
            }

            if (currentPage >= 2) // adding current button if it is not the first
            {
                var aCurrent = new TagBuilder("a");
                aCurrent.MergeAttribute("href", linkBeginning + currentPage.ToString() + linkEnd);
                aCurrent.SetInnerText(currentPage.ToString());

                activeDiv.InnerHtml = aCurrent.ToString();

                nav += activeDiv.ToString();
            }

            if (pagesCount >= currentPage + 1) //if current button is not the last one we could add the next
            {
                var aNext = new TagBuilder("a");
                aNext.MergeAttribute("href", linkBeginning + (currentPage + 1).ToString() + linkEnd);
                aNext.SetInnerText((currentPage + 1).ToString());
                div.InnerHtml = aNext.ToString();
                nav += div.ToString();
            }
            else //else we could return it
                return new MvcHtmlString(nav.ToString());
            if (pagesCount > currentPage + 2) //if it is so far to end we could add the "after dotes"
            {
                div.InnerHtml = "...";
                nav += div.ToString();
            }
            if (!(pagesCount < currentPage + 2)) //adding last
            {
                var aLast = new TagBuilder("a");
                aLast.MergeAttribute("href", linkBeginning + pagesCount.ToString() + linkEnd);
                aLast.SetInnerText(pagesCount.ToString());
                div.InnerHtml = aLast.ToString();
                nav += div.ToString();
            }

            return new MvcHtmlString(nav.ToString());


        }
    }
}