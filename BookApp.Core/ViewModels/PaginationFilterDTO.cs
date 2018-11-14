using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookApp.Core.ViewModels
{

    public class PaginationFilterDTO
    {
        public int RowCount { get; set; }
        public int Current { get; set; }
        public string Search { get; set; }
        public string SortBy { get; set; }
        public string SortDirection { get; set; }
        public int TotalItems { get; set; }
    }
}