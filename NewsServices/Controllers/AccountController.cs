using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewsServices.Models;
using NewsServices.Models.EntityFrameworkModel;
using NewsServices.Comman;

namespace NewsServices.Controllers
{
    public class AccountController : ApiController
    {
        #region Security api
        [HttpPost]
        [AllowAnonymous]
        public UserDetailResponseModel RegisterUser([FromBody] UserDetailRequestModel userDetailsRequest)
        {
            try
            {
                bool isSuccess = false;
                UserDetailResponseModel response = new UserDetailResponseModel();
                UserDetail dbUserDetails = new UserDetail();
                dbUserDetails.UserRegistrationId = Guid.NewGuid();
                dbUserDetails.creatTs = DateTime.UtcNow;
                dbUserDetails.isActive = true;
                dbUserDetails.EmailId = userDetailsRequest.EmailId;
                dbUserDetails.FirstName = Utility.ToTitleCase(userDetailsRequest.FirstName);
                dbUserDetails.LastName = Utility.ToTitleCase(userDetailsRequest.LastName);
                dbUserDetails.MobileNo = userDetailsRequest.MobileNo;
                dbUserDetails.Password = userDetailsRequest.Password;
                dbUserDetails.Gender = Utility.ToTitleCase(userDetailsRequest.Gender);
                dbUserDetails.ThumbnailUrl = Utility.base64toThumbanailUrl(userDetailsRequest.Thumbnail);
                dbUserDetails.UserId = userDetailsRequest.UserId;
                dbUserDetails.userType = userDetailsRequest.userType == 0 ? 1 : userDetailsRequest.userType;

                using (var dbContext = new newsdbEntities())
                {
                    dbContext.UserDetails.Add(dbUserDetails);
                    int success = dbContext.SaveChanges();
                    isSuccess = Convert.ToBoolean(success);
                }
                if (isSuccess)
                {
                    response.EmailId = dbUserDetails.EmailId;
                    response.FirstName = dbUserDetails.FirstName;
                    response.LastName = dbUserDetails.LastName;
                    response.MobileNo = dbUserDetails.MobileNo;
                    response.Success = isSuccess;
                    response.Thumbnail = dbUserDetails.ThumbnailUrl;
                    response.UserId = dbUserDetails.UserId;
                    response.Gender = dbUserDetails.Gender;
                    response.UserRegistrationId = dbUserDetails.UserRegistrationId;
                    response.userType = dbUserDetails.userType;
                    response.Initials = Utility.GetEmployeeInitials(dbUserDetails.FirstName, dbUserDetails.LastName).ToUpper();

                    return response;
                }
                else
                {
                    return response;
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("unique"))
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "LoginId " + userDetailsRequest.UserId + " is already register!"));
                }
                else
                {
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex));
                }

            }
        }

        [HttpPost]
        [AllowAnonymous]
        public UserDetailResponseModel AuthenticateUser([FromBody] LoginRequestModel LoginRequest)
        {
            UserDetailResponseModel response = new UserDetailResponseModel();
            using (var dbContext = new newsdbEntities())
            {
                var userEntity = dbContext.UserDetails.FirstOrDefault(u => u.UserId.Equals(LoginRequest.UserId, StringComparison.OrdinalIgnoreCase) && u.Password == LoginRequest.Password);
                if (userEntity != null)
                {
                    response.EmailId = userEntity.EmailId;
                    response.FirstName = userEntity.FirstName;
                    response.Gender = userEntity.Gender;
                    response.Initials = Utility.GetEmployeeInitials(userEntity.FirstName, userEntity.LastName).ToUpper();
                    response.LastName = userEntity.LastName;
                    response.MobileNo = userEntity.MobileNo;
                    response.Success = true;
                    response.Thumbnail = userEntity.ThumbnailUrl;
                    response.UserId = userEntity.UserId;
                    response.UserRegistrationId = userEntity.UserRegistrationId;
                    response.userType = userEntity.userType;
                }
                return response;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public bool ChangePassword(changePasswordRequest Req)
        {
            using (var dbContext = new newsdbEntities())
            {
                var userEntity = dbContext.UserDetails.FirstOrDefault(u => u.UserRegistrationId == Req.UserRegisted && u.Password == Req.OldPassword);
                if (userEntity != null)
                {
                    userEntity.Password = Req.NewPassword;
                    dbContext.Entry(userEntity).State = System.Data.Entity.EntityState.Modified;
                    int success = dbContext.SaveChanges();
                    return Convert.ToBoolean(success);
                }
                else
                {
                    return false;
                }
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public bool ResetPasswordByEmail([FromBody] ResetPasswordByEmailRequest Req)
        {
            using (var dbContext = new newsdbEntities())
            {
                var userEntity = dbContext.UserDetails.FirstOrDefault(u => u.UserId == Req.UserId && u.EmailId == Req.EmailId);
                if (userEntity != null)
                {
                    string Subject = "News App Password Reseted.";
                    string body = "Hello " + userEntity.FirstName + " Please Use following credentials for login " + Environment.NewLine + "UserId : " + userEntity.UserId + Environment.NewLine + "Password : " + userEntity.Password;
                    bool isMailSent = Utility.SendEmail(userEntity.EmailId, Subject, body);
                    return isMailSent;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion
    }
}
