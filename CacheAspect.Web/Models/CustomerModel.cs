using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CacheAspect.Web.Models
{
    public class IndexModel
    {
        public string CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
    }

    public class DetailModel
    {
        [Display(Name = "Id")]
        public string CustomerId { get; set; }

        [Display(Name = "Company")]
        public string CompanyName { get; set; }

        [Display(Name = "Contact name")]
        public string ContactName { get; set; }

        [Display(Name = "Contact title")]
        public string ContactTitle { get; set; }
    }
}