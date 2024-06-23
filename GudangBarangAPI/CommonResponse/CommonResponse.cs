using System;

namespace GudangBarangAPI.CommonResponse
{
    public class CommonResponse<T>
    {
        public Status Status 
        {
            get; set;
        }

        public T Data
        {
            get; set;
        }

        public Paging Paging
        {
            get; set;
        }
    }

    public class Status
    {
        public int Code
        {
            get; set;
        }

        public string Message
        {
            get; set;
        }
    }

    public class Paging
    {
        public int CurrentPage
        {
            get; set;
        }
        public int PageSize
        {
            get; set;
        }

        public int TotalPages
        {
            get; set;
        }

        public int TotalItems
        {
            get; set;
        }
    }
}