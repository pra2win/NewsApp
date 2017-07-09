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
                        IsActive=n.isActive
                    });
                }
            }

            return response;
        }

        [Route("ConfirmNews/{newsId}")]
        public bool ConfirmNews(Guid newsId)
        {
            newsdbEntities db = new newsdbEntities();
            {
               var news= db.NewsDetails.FirstOrDefault(n => n.NewsId == newsId);
                news.isActive = true;
                db.Entry(news).State = EntityState.Modified;
                int hasUpdate=db.SaveChanges();
                return hasUpdate > 0;
            };
        }

    }
}
