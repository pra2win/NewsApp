using NewsServices.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace NewsServices.Comman
{
    public static class Utility
    {

        public static string GetEmployeeInitials(string firstName, string lastName)
        {
            string initials = string.Empty;

            if (string.IsNullOrEmpty(firstName) == false)
            {
                Int32[] firstNameElemIndex = StringInfo.ParseCombiningCharacters(firstName);

                TextElementEnumerator charEnumForFirstName = StringInfo.GetTextElementEnumerator(firstName);
                while (charEnumForFirstName.MoveNext())
                {
                    initials = charEnumForFirstName.GetTextElement();
                    break;
                }
            }

            if (string.IsNullOrEmpty(lastName) == false)
            {
                Int32[] lastNameElemIndex = StringInfo.ParseCombiningCharacters(lastName);

                TextElementEnumerator charEnumForLastName = StringInfo.GetTextElementEnumerator(lastName);
                while (charEnumForLastName.MoveNext())
                {
                    initials += charEnumForLastName.GetTextElement();
                    break;
                }
            }
            return initials;
        }

        public static bool SendEmail(string To, string Subject, string body)
        {

            string from = ConfigurationManager.AppSettings["FromEmailId"].ToString();
            string pwd = ConfigurationManager.AppSettings["MachinePWD"].ToString();
            string UserId = ConfigurationManager.AppSettings["MachineUserId"].ToString();

            string smtpServer = ConfigurationManager.AppSettings["EmailServer"].ToString();

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(smtpServer);

            mail.From = new MailAddress(from);
            mail.To.Add(To);
            mail.Subject = Subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(UserId, pwd);
            try
            {
                SmtpServer.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                var exs = ex;
                return false;
            }
        }

        public static string base64toThumbanailUrl(string thumbnailBase64)
        {
            if (string.IsNullOrEmpty(thumbnailBase64))
            {
                return null;
            }
            try
            {
                string physicalFilePath = ConfigurationManager.AppSettings["UploadedFilePath"].ToString();
                string virtualFilePath = ConfigurationManager.AppSettings["FileVirtualPath"].ToString();

                string fileName = Guid.NewGuid().ToString("N").ToUpper() + ".jpg";
                string PhysicalfileName = physicalFilePath + fileName;

                var bytes = Convert.FromBase64String(thumbnailBase64);
                System.IO.Directory.CreateDirectory(physicalFilePath);

                using (var imageFile = new FileStream(PhysicalfileName, FileMode.Create))
                {
                    imageFile.Write(bytes, 0, bytes.Length);
                    imageFile.Flush();
                }

                return virtualFilePath + fileName;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return null;
            }
        }

        public static string ToTitleCase(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }
            else
            {
                return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
            }
        }

        public static DateTime GetIndianDateTime()
        {
           // DateTime serverTime = DateTime.Now; // gives you current Time in server timeZone
            //DateTime utcTime = serverTime.ToUniversalTime(); // convert it to Utc using timezone setting of server computer

            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzi); // convert from utc to local
            return localTime;
        }

        public static async Task<bool> SendFcmNotificationMessage(string title, string boday, string DevToken, dynamic dataObj)
        {

            //send fcm notification
            NotificationModel noti = new NotificationModel();
            //  dataObj.NotifyToAll = Notify;

            Notification header = new Notification();
            header.title = title;
            header.body = boday;
            noti.notification = header;
            noti.data = dataObj;
            noti.content_available = true;
            //temp dev token in 2
            noti.to = DevToken;

            string url = ConfigurationManager.AppSettings["FCMAPIURL"];
            string FCMAuthorization = ConfigurationManager.AppSettings["FCMAuthorization"];


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            //JavaScriptSerializer Js = new JavaScriptSerializer
            //{
            //    MaxJsonLength = int.MaxValue,
            //    RecursionLimit = 100
            //};
            //string serailizedString = Js.Serialize(noti);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("key", "=" + FCMAuthorization);
            var response = await client.PostAsJsonAsync(url, noti).ConfigureAwait(false);  // Blocking call!
            //if (response.IsSuccessStatusCode)
            //{
            //    // Parse the response body. Blocking!
            //   // var dataObjects = response.Content.ReadAsAsync<DataObject>().Result;
            //}
            return response.IsSuccessStatusCode;
        }
    }
}