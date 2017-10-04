using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsServices.Models
{
    public class UserDetailRequestModel
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Gender { get; set; }
        public string Thumbnail { get; set; }
        public int userType { get; set; }
        public string FcmToken { get; set; }
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

    public class LoginRequestModel
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }
    public class changePasswordRequest
    {
        public Guid UserRegisted { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
    public class ResetPasswordByEmailRequest
    {
        public string UserId { get; set; }
        public string EmailId { get; set; }
    }
    public class Filter
    {
        public string SearchText { get; set; }
        public DateTime HistoryFrom { get; set; }
        public Guid UserRegistrationId { get; set; }

    }

    public class AddCommentRequestModel
    {
        public string CommentText { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Guid userRegistrationId { get; set; }
        public Guid newsId { get; set; }
    }
    public class AddCommentResponseModel
    {
        public bool Success { get; set; }
        public Guid CommentId { get; set; }
    }

    public class LikeRequestModel
    {
        public Guid userRegistrationId { get; set; }
        public Guid newsId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int LikeDislike { get; set; }
    }

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

    public class newsListReponse
    {
        public System.Guid NewsId { get; set; }
        public int CategoryId { get; set; }
        public string NewsPhotoUrl { get; set; }
        public string NewsTitle { get; set; }
        public string NewsDescription { get; set; }
        public Nullable<System.DateTime> CreatedTs { get; set; }
        public System.Guid NewsById { get; set; }
        public string LikeCount { get; set; }
        public string DisLikeCount { get; set; }
        public bool selfLike { get; set; }
        public bool selfDisLike { get; set; }
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
    public class NewsUpdateModel
    {
        public System.Guid NewsId { get; set; }
        public int CategoryId { get; set; }
        public string NewsTitle { get; set; }
        public string NewsDescription { get; set; }
    }

    public class NewsListModel
    {
        public List<newsListReponse> NewsList { get; set; }
        public string AppVersion { get; set; }
    }

    public class NewsDetailResponse
    {
        public System.Guid NewsId { get; set; }
        public int CategoryId { get; set; }
        public string NewsPhotoUrl { get; set; }
        public string NewsTitle { get; set; }
        public string NewsDescription { get; set; }
        public Nullable<System.DateTime> CreatedTs { get; set; }
        public System.Guid NewsById { get; set; }
        public Nullable<bool> IsNotify { get; set; }
        public bool isActive { get; set; }

        public List<CommentsModel> commentsList { get; set; }
        public string LikeCount { get; set; }
        public string DisLikeCount { get; set; }
        public bool selfLike { get; set; }
        public bool selfDisLike { get; set; }
    }
    public class CommentsModel
    {
        public string CommentByUserName { get; set; }
        public string CommentText { get; set; }
        public string CommentByThumbnailUrl { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    public class User
    {
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
        public int TotalNewsUploaded { get; set; }
        public int TotalNewsApproved { get; set; }
    }
    public enum NewsCategories
    {
        Global = 1,
        Politics = 2,
        Entertainment = 3,
        Sport = 4,
        LifeStyle = 5,
        Gadgets = 6,

    }
}