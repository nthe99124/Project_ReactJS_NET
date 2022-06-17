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
        private long _pageCount;
        public long PageCount
        {
            get => _pageCount;
            set
            {
                if (TotalRecords == 0)
                {
                    _pageCount = 0;
                }
                else if (TotalRecords / PageSize == Convert.ToInt32(TotalRecords / PageSize))
                {
                    _pageCount = TotalRecords / PageSize;
                }
                else _pageCount = Convert.ToInt32(TotalRecords / PageSize) + 1;
            }
        }

    }
}
