using NewsServices.Comman;
using NewsServices.Models;
using NewsServices.Models.EntityFrameworkModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NewsServices.Controllers
{
    [RoutePrefix("api/manager")]
    public class ManagerController : ApiController
    {
        [Route("GeneralNewsList")]
        public List<generalNewsListReponse> GeneralNewsList()
        {
            List<generalNewsListReponse> response = new List<generalNewsListReponse>();
            newsdbEntities db = new newsdbEntities();
            {
                var resp = db.NewsDetails.OrderByDescending(t => t.CreatedTs).ToList();

                foreach (var n in resp)
                {
                    string postedBy = string.Empty;
                    var postedByEntity = db.UserDetails.FirstOrDefault(u => u.UserRegistrationId == n.NewsById);
                    if (postedByEntity != null)
                    {
                        postedBy = postedByEntity.FirstName + " " + postedByEntity.LastName;
                    }

                    response.Add(new generalNewsListReponse()
                    {
                        CategoryId = n.CategoryId,
                        CreatedTs = n.CreatedTs,
                        NewsById = n.NewsById,
                        NewsDescription = n.NewsDescription,
                        NewsId = n.NewsId,
                        NewsPhotoUrl = n.NewsPhotoUrl,
                        NewsTitle = n.NewsTitle,
                        CategoryName = Enum.GetName(typeof(NewsCategories), n.CategoryId),
                        NewsByName = postedBy,
                        IsActive = n.isActive
                    });
                }
            }

            return response;
        }

        [Route("ConfirmNews/{newsId}/{notify}")]
        public bool ConfirmNews(Guid newsId, bool notify)
        {
            newsdbEntities db = new newsdbEntities();
            {
                var news = db.NewsDetails.FirstOrDefault(n => n.NewsId == newsId);
                news.isActive = true;
                news.IsNotify = notify;
                news.CreatedTs = Utility.GetIndianDateTime();
                db.Entry(news).State = EntityState.Modified;
                int hasUpdate = db.SaveChanges();

                dynamic dataTosend = new object();
                dataTosend.CategoryId = news.CategoryId;
                dataTosend.NewsId = news.NewsId;
                dataTosend.NewsPhotoUrl = news.NewsPhotoUrl;
                dataTosend.NewsDescription = news.NewsDescription;
                dataTosend.CreatedTs = news.CreatedTs;
                dataTosend.NewsTitle = news.NewsTitle;

                if (notify)
                {
                    var noti = Utility.SendFcmNotificationMessage("TalkOut", news.NewsTitle, "NewsApp", dataTosend);
                }
                return hasUpdate > 0;
            };
        }

        [Route("EditNews/{newsId}")]
        public generalNewsListReponse EditNews(Guid newsId)
        {
            generalNewsListReponse resp = new generalNewsListReponse();
            newsdbEntities db = new newsdbEntities();
            {
                var newsData = db.NewsDetails.FirstOrDefault(n => n.NewsId == newsId);

                resp.IsActive = newsData.isActive;
                resp.CreatedTs = newsData.CreatedTs;
                resp.CategoryId = newsData.CategoryId;
                resp.CategoryName = Enum.GetName(typeof(NewsCategories), newsData.CategoryId);
                resp.NewsById = newsData.NewsById;
                resp.NewsByName = "";
                resp.NewsDescription = newsData.NewsDescription;
                resp.NewsId = newsData.NewsId;
                resp.NewsPhotoUrl = newsData.NewsPhotoUrl;
                resp.NewsTitle = newsData.NewsTitle;

            }
            return resp;
        }

        [Route("UpdateNews")]
        public generalNewsListReponse UpdateNews(NewsUpdateModel updateEntity)
        {
            generalNewsListReponse resp = new generalNewsListReponse();
            newsdbEntities db = new newsdbEntities();
            {
                var newsData = db.NewsDetails.FirstOrDefault(n => n.NewsId == updateEntity.NewsId);
                newsData.NewsTitle = updateEntity.NewsTitle;
                newsData.NewsDescription = updateEntity.NewsDescription;
                newsData.CategoryId = updateEntity.CategoryId;
                db.Entry(newsData).State = EntityState.Modified;
                int hasUpdate = db.SaveChanges();

                resp.IsActive = newsData.isActive;
                resp.CreatedTs = newsData.CreatedTs;
                resp.CategoryId = newsData.CategoryId;
                resp.CategoryName = Enum.GetName(typeof(NewsCategories), newsData.CategoryId);
                resp.NewsById = newsData.NewsById;
                resp.NewsByName = "";
                resp.NewsDescription = newsData.NewsDescription;
                resp.NewsId = newsData.NewsId;
                resp.NewsPhotoUrl = newsData.NewsPhotoUrl;
                resp.NewsTitle = newsData.NewsTitle;
                return resp;
            }
        }

        [Route("GetUsers")]
        public List<User> GetUsers()
        {
            var users = new List<User>();
            newsdbEntities db = new newsdbEntities();
            var _users = db.UserDetails.ToList();

            foreach (var u in _users)
            {
                var news = db.NewsDetails.Where(n => n.NewsById == u.UserRegistrationId);
                users.Add(new Models.User()
                {
                    creatTs = u.creatTs,
                    EmailId = u.EmailId,
                    FirstName = u.FirstName,
                    Gender = u.Gender,
                    isActive = u.isActive,
                    LastName = u.LastName,
                    MobileNo = u.MobileNo,
                    ThumbnailUrl = u.ThumbnailUrl,
                    UserId = u.UserId,
                    userType = u.userType,
                    UserRegistrationId = u.UserRegistrationId,
                    TotalNewsApproved = news.Where(n => n.isActive == true).Count(),
                    TotalNewsUploaded = news.Count()
                });
            }

            return users;

        }
    }
}
