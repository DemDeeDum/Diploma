using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel.WEB.Models.Common
{
    public class Page<TListMember, TInputModel>
        where TInputModel : class, new()
        where TListMember : class
    {
        public Page()
        {
            ItemList = new List<TListMember>();
            InputModel = new TInputModel();
        }
        public void CalculatePositions(out int StartPosition
            , out int FinishPosition,
            int ItemsCount, int PageSize, int CurrentPageId)
        {
            PageNumber = CurrentPageId;
            PageCount = ItemsCount / PageSize + 
                (ItemsCount % PageSize == 0 ? 0 : 1);
            if ( PageCount < CurrentPageId || CurrentPageId < 1)
            {
                PageNumber = -1;
                StartPosition = -1;
                FinishPosition = -1;
                return;
            }
            else
            {
                StartPosition = PageSize * CurrentPageId - PageSize;
                FinishPosition = PageSize * CurrentPageId;
            }
        }

        public List<TListMember> ItemList { get; set; }
        public SearchSettings SearchInfo { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
        public TInputModel InputModel { get; set; }
    }
}