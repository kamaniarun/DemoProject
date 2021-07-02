using EntityLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoProject.Models
{
    public class ProductDisplayModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Category is Required")]
        public long Categoryid { get; set; }
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Product Name is Required")]
        public string ProductName { get; set; }

        public bool? IsActive { get; set; }

        public long SrNo { get; set; }
    }


    public class CategoryDisplayModel
    {
        public string categoryname { get; set; }

        public List<Product> productlist { get; set; }
    }
}