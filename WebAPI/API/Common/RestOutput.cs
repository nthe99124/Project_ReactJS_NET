using System;
using System.Collections.Generic;

namespace API.Common
{
    public class RestOutput<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<T> Data { get; set; }
        public int PageSize = 5;
        public long TotalRecords { get; set; }
        private long pageCount;

        public long PageCount
        {
            get { return pageCount; }
            set
            {
                //pageCount = (TotalRecords / PageSize = Convert.ToInt32(TotalRecords / PageSize)) ? TotalRecords / PageSize : TotalRecords / PageSize + 1;
                if (TotalRecords == 0)
                {
                    pageCount = 0;
                }
                else if (TotalRecords / PageSize == Convert.ToInt32(TotalRecords / PageSize))
                {
                    pageCount = TotalRecords / PageSize;
                }
                else pageCount = Convert.ToInt32(TotalRecords / PageSize) + 1;
            }
        }

    }
}
