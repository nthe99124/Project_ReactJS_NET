using System;
using System.Collections.Generic;

namespace API.Common
{
    public class RestOutput<T>
    {
        public string Message { get; set; }
        public List<T> Data { get; set; }
        public int PageSize { get; set; } = 5;
        public long TotalRecords { get; set; }

        public RestOutput(List<T> data, long pageCount = 0)
        {
            Data = data;
            PageCount = pageCount;
        }

        public RestOutput(long totalRecords, long pageCount = 0)
        {
            TotalRecords = totalRecords;
            PageCount = pageCount;
        }

        public RestOutput(List<T> data, long totalRecords, long pageCount = 0)
        {
            Data = data;
            TotalRecords = totalRecords;
            PageCount = pageCount;
        }

        private long _pageCount;
        // làm sao để mặc định pageCount được set???? luôn cần set pageCount thì mới vào được propertype
        public long PageCount
        {
            get => _pageCount;
            set
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
            }
        }
    }
}
