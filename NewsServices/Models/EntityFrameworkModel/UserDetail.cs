//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewsServices.Models.EntityFrameworkModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserDetail
    {
        public UserDetail()
        {
            this.Comments = new HashSet<Comment>();
            this.Favourites = new HashSet<Favourite>();
            this.Likes = new HashSet<Like>();
        }
    
        public System.Guid UserRegistrationId { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Gender { get; set; }
        public string ThumbnailUrl { get; set; }
        public bool isActive { get; set; }
        public System.DateTime creatTs { get; set; }
        public int userType { get; set; }
    
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Favourite> Favourites { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
