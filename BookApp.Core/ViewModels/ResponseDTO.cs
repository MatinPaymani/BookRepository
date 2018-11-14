using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookApp.Core.ViewModels
{
    public class ResponseDTO<T>
    {
        public int current { get; set; }
        public int rowCount { get; set; }
        public List<T> rows { get; set; }
        public int total { get; set; }
    }
}