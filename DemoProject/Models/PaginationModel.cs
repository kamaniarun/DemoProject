using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoProject.Models
{
    public class PaginationModel
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public PaginationModel CreatePager(int CurrentPageNumber, int Total)
        {
            PaginationModel obj = new PaginationModel();
            obj.CurrentPage = CurrentPageNumber;
            obj.ItemsPerPage = 10;
            obj.TotalItems = Total;
            return obj;
        }
    }
}