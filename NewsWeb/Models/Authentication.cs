using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlanetNewsAdmin.Models
{
   
    public class LoginReq
    {
        [Required]
        [Display(Name = "User name")]
        public string UserId { get; set; }

        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
    public class UserDetailResponseModel
    {
        public bool Success { get; set; }
        public Guid UserRegistrationId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Gender { get; set; }
        public string Thumbnail { get; set; }
        public string Initials { get; set; }
        public int userType { get; set; }

    }
}