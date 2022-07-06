using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace API.Common
{
    public class RestOutput<T>
    {
        public string Message { get; set; }
        public List<T> Data { get; set; }
        public int PageSize { get; set; } = 5;
        public long TotalRecords { get; set; }

        public RestOutput(List<T> data)
        {
            Data = data;
        }

        public RestOutput(long totalRecords)
        {
            TotalRecords = totalRecords;
        }

        public RestOutput(List<T> data, long totalRecords)
        {
            Data = data;
            TotalRecords = totalRecords;
        }

        private long _pageCount;
        public long PageCount
        {
            get
            {
                if (TotalRecords == 0)
                {
                    _pageCount = 0;
                }
                else if (TotalRecords / PageSize == Convert.ToInt32(TotalRecords * 1.0 / PageSize))
                {
                    _pageCount = TotalRecords / PageSize;
                }
                else _pageCount = Convert.ToInt32(TotalRecords / PageSize) + 1;

                return _pageCount;
            }
        }
    }
}
