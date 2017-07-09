using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsWeb.Models
{
    public class AddNewsRequestModel
    {

        public int CategoryId { get; set; }
        public string NewsPhoto { get; set; }
        public string NewsTitle { get; set; }
        public string NewsDescription { get; set; }
        public Nullable<System.DateTime> CreatedTs { get; set; }
        public System.Guid NewsById { get; set; }
        public bool NotifyToAll { get; set; }
    }
    public class NewsDetail
    {

        public System.Guid NewsId { get; set; }
        public int CategoryId { get; set; }
        public string NewsPhotoUrl { get; set; }
        public string NewsTitle { get; set; }
        public string NewsDescription { get; set; }
        public Nullable<System.DateTime> CreatedTs { get; set; }
        public System.Guid NewsById { get; set; }


    }
    public class generalNewsListReponse
    {
        public System.Guid NewsId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string NewsPhotoUrl { get; set; }
        public string NewsTitle { get; set; }
        public string NewsDescription { get; set; }
        public Nullable<System.DateTime> CreatedTs { get; set; }
        public System.Guid NewsById { get; set; }
        public string NewsByName { get; set; }
        public bool IsActive { get; set; }
    }

}
