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
    
    public partial class Favourite
    {
        public System.Guid FavId { get; set; }
        public System.Guid UserRegistrationId { get; set; }
        public int CategoriesId { get; set; }
        public string CategoriesName { get; set; }
    
        public virtual UserDetail UserDetail { get; set; }
    }
}
