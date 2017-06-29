using NewsServices.Comman;
using NewsServices.Models;
using NewsServices.Models.EntityFrameworkModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NewsServices.Controllers
{
    public class NewsController : ApiController
    {
        
        #region News List
        [HttpPost]
        public List<newsListReponse> NewsList(Filter filt)
        {
            string appVersion = (string)ConfigurationManager.AppSettings["AppVersion"];
            List<newsListReponse> response = new List<newsListReponse>();
            newsdbEntities db = new newsdbEntities();
            if (string.IsNullOrEmpty(filt.SearchText))
            {
                var resp = db.NewsDetails.Where(t => t.CreatedTs > filt.HistoryFrom).OrderByDescending(t => t.CreatedTs).ToList();

                foreach (var n in resp)
                {
                    bool selfLike = false, selfDisLike = false;
                    int likeCount = 0, disLikeCount = 0;
                    List<Like> Newslikes = db.Likes.Where(l => l.NewsId == n.NewsId).ToList();
                    if (Newslikes != null)
                    {
                        selfLike = Newslikes.Any(lk => lk.IsLike == true && lk.UserRegistrationId == filt.UserRegistrationId);
                        selfDisLike = Newslikes.Any(lk => lk.IsLike == false && lk.UserRegistrationId == filt.UserRegistrationId);
                        likeCount = Newslikes.Where(lk => lk.IsLike == true).Count();
                        disLikeCount = Newslikes.Where(lk => lk.IsLike == false).Count();
                    }


                    response.Add(new newsListReponse()
                    {
                        CategoryId = n.CategoryId,
                        CreatedTs = n.CreatedTs,
                        NewsById = n.NewsById,
                        NewsDescription = n.NewsDescription,
                        NewsId = n.NewsId,
                        NewsPhotoUrl = n.NewsPhotoUrl,
                        NewsTitle = n.NewsTitle,
                        selfLike = selfLike,
                        selfDisLike = selfDisLike,
                        LikeCount = likeCount.ToString(),
                        DisLikeCount = disLikeCount.ToString(),
                        AppVersion=appVersion
                    });
                }
            }
            else
            {
                var resp = db.NewsDetails.Where(n => n.NewsTitle.Contains(filt.SearchText) || n.NewsDescription.Contains(filt.SearchText) && n.CreatedTs > filt.HistoryFrom).OrderByDescending(t => t.CreatedTs).ToList();
                if (resp != null)
                {
                    foreach (var n in resp)
                    {
                        bool selfLike = false, selfDisLike = false;
                        int likeCount = 0, disLikeCount = 0;
                        List<Like> Newslikes = db.Likes.Where(l => l.NewsId == n.NewsId).ToList();
                        if (Newslikes != null)
                        {
                            selfLike = Newslikes.Any(lk => lk.IsLike == true && lk.UserRegistrationId == filt.UserRegistrationId);
                            selfDisLike = Newslikes.Any(lk => lk.IsLike == false && lk.UserRegistrationId == filt.UserRegistrationId);
                            likeCount = Newslikes.Where(lk => lk.IsLike == true).Count();
                            disLikeCount = Newslikes.Where(lk => lk.IsLike == false).Count();
                        }


                        response.Add(new newsListReponse()
                        {
                            CategoryId = n.CategoryId,
                            CreatedTs = n.CreatedTs,
                            NewsById = n.NewsById,
                            NewsDescription = n.NewsDescription,
                            NewsId = n.NewsId,
                            NewsPhotoUrl = n.NewsPhotoUrl,
                            NewsTitle = n.NewsTitle,
                            selfLike = selfLike,
                            selfDisLike = selfDisLike,
                            LikeCount = likeCount.ToString(),
                            DisLikeCount = disLikeCount.ToString(),
                            AppVersion = appVersion
                        });
                    }

                }
            }

            return response;
        }
        #endregion

        #region News Details
        [HttpGet]
        [Route("api/news/GetNewsDetails/{newsId}")]
        public NewsDetail GetNewsDetails(Guid newsId)
        {
            newsdbEntities db = new newsdbEntities();
            List<Comment> comments = new List<Comment>();

            NewsDetail newsEntity = db.NewsDetails.FirstOrDefault(n => n.NewsId == newsId);//GET NEWS ENTITY FROM DATABASE

            List<Comment> cmm = db.Comments.Where(c => c.NewsId == newsId).ToList();//lIST OF ALL COMMENTS
            List<Like> lkk = db.Likes.Where(lk => lk.NewsId == newsId).ToList();//LIST OF LIKES

            newsEntity.Comments = cmm;
            newsEntity.Likes = lkk;

            return newsEntity;
        }

        #endregion

        #region Comment
        [HttpGet]
        [Route("api/news/GetNewsComment/{newsId}")]
        public List<Comment> GetNewsComment(Guid newsId)
        {
            newsdbEntities db = new newsdbEntities();
            List<Comment> comments = new List<Comment>();

           // NewsDetail newsEntity = db.NewsDetails.FirstOrDefault(n => n.NewsId == newsId);//GET NEWS ENTITY FROM DATABASE

            List<Comment> cmm = db.Comments.Where(c => c.NewsId == newsId).ToList();//lIST OF ALL COMMENTS
           // List<Like> lkk = db.Likes.Where(lk => lk.NewsId == newsId).ToList();//LIST OF LIKES

          //  newsEntity.Comments = cmm;
            //newsEntity.Likes = lkk;

            return cmm;
        }

        public AddCommentResponseModel addComment(AddCommentRequestModel cmm)
        {
            AddCommentResponseModel resp = new AddCommentResponseModel();
            newsdbEntities db = new newsdbEntities();
            Comment comment = new Comment();
            comment.CommentId = Guid.NewGuid();
            comment.CommentText = cmm.CommentText;
            comment.TimeStamp = cmm.TimeStamp;
            comment.UserDetail = (from user in db.UserDetails where user.UserRegistrationId == cmm.userRegistrationId select user).FirstOrDefault();
            comment.NewsDetail = (from news in db.NewsDetails where news.NewsId == cmm.newsId select news).FirstOrDefault();

            db.Comments.Add(comment);
            int success = db.SaveChanges();
            if (Convert.ToBoolean(success))
            {
                resp.CommentId = comment.CommentId;
                resp.Success = true;
            }
            return resp;
        }
        #endregion

        #region like
        public Like NewsLikeDislike(LikeRequestModel req)
        {
            bool isLike = false;
            bool isDislike = false;
            if (req.LikeDislike == 1)
            {
                isLike = true;
            }
            else
            {
                isDislike = true;
            }
            newsdbEntities db = new newsdbEntities();
            Like like = new Like();
            var likeEntity = db.Likes.FirstOrDefault(l => l.NewsDetail.NewsId == req.newsId && l.UserDetail.UserRegistrationId == req.userRegistrationId);
            if (likeEntity != null)
            {
                likeEntity.IsLike = isLike;
                likeEntity.IsDislike = isDislike;
                likeEntity.TimeStamp = req.TimeStamp;
                db.Entry(likeEntity).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return likeEntity;
            }
            else
            {
                like.IsDislike = isDislike;
                like.IsLike = isLike;
                like.LikeId = Guid.NewGuid();
                like.NewsDetail = (from n in db.NewsDetails where n.NewsId == req.newsId select n).FirstOrDefault();
                like.NewsId = req.newsId;
                like.TimeStamp = req.TimeStamp;
                like.UserRegistrationId = req.userRegistrationId;
                like.UserDetail = (from n in db.UserDetails where n.UserRegistrationId == req.userRegistrationId select n).FirstOrDefault();

                db.Likes.Add(like);
                db.SaveChanges();

                return like;
            }

        }

        #endregion

        #region Add News
        public NewsDetail addNews(AddNewsRequestModel req)
        {
            newsdbEntities db = new newsdbEntities();
            NewsDetail news = new NewsDetail();
            news.Likes = null;
            news.Comments = null;

            news.NewsTitle = req.NewsTitle;
            news.NewsPhotoUrl = Utility.base64toThumbanailUrl(req.NewsPhoto);
            news.NewsId = Guid.NewGuid();
            news.NewsDescription = req.NewsDescription;
            news.NewsById = req.NewsById;
            news.CreatedTs = req.CreatedTs;
            news.CategoryId = req.CategoryId;
            db.NewsDetails.Add(news);
            int saved = db.SaveChanges();
            return news;
        }


        #endregion

    }

}
