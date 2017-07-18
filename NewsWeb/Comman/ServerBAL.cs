using NewsWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace NewsWeb.Comman
{
    public class ServerBAL
    {
        private static string newsAddUrl = ConfigurationManager.AppSettings["baseUrl"].ToString();
        private static Uri baseUri = new Uri(newsAddUrl);

        public static UserDetailResponseModel AuthenticateUser(LoginReq req)
        {
            UserDetailResponseModel resp = new UserDetailResponseModel();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = baseUri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(req));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = client.PostAsync("api/Account/AuthenticateUser", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var value = response.Content.ReadAsStringAsync();

                    resp = JsonConvert.DeserializeObject<UserDetailResponseModel>(value.Result);
                }
                return resp;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static NewsDetail AddNews(AddNewsRequestModel request)
        {
            NewsDetail resp = new NewsDetail();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = baseUri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(request));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = client.PostAsync("api/news/addNews", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var value = response.Content.ReadAsStringAsync();

                    resp = JsonConvert.DeserializeObject<NewsDetail>(value.Result);
                }
                return resp;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<generalNewsListReponse> NewsList()
        {
            var result = new List<generalNewsListReponse>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = baseUri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

              // var content = new StringContent(JsonConvert.SerializeObject(request));
                //content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = client.PostAsync("api/manager/GeneralNewsList", null).Result;
                if (response.IsSuccessStatusCode)
                {
                    var value = response.Content.ReadAsStringAsync();

                    result = JsonConvert.DeserializeObject<List<generalNewsListReponse>>(value.Result);
                }
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool ApproveNews(Guid newsId)
        {
            bool result = false;
            try
            {
                var client = new HttpClient();
                client.BaseAddress = baseUri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // var content = new StringContent(JsonConvert.SerializeObject(request));
                //content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = client.PostAsync("api/manager/ConfirmNews/"+newsId, null).Result;
                if (response.IsSuccessStatusCode)
                {
                    var value = response.Content.ReadAsStringAsync();

                    result = JsonConvert.DeserializeObject<bool>(value.Result);
                }
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static generalNewsListReponse GetNewsDetails(Guid newsId)
        {
            generalNewsListReponse res = new generalNewsListReponse();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = baseUri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(request));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = client.PostAsync("api/Account/RegisterUser/", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var value = response.Content.ReadAsStringAsync();

                    result = JsonConvert.DeserializeObject<CreateUser>(value.Result);
                }
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }

        public static CreateUser CreateNewUser(CreateUser request)
        {
            CreateUser result = new CreateUser();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = baseUri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(JsonConvert.SerializeObject(request));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = client.PostAsync("api/Account/RegisterUser/", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var value = response.Content.ReadAsStringAsync();

                    result = JsonConvert.DeserializeObject<CreateUser>(value.Result);
                }
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}