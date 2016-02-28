using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json.Linq;
using VoucherWorld.Model;
using VoucherWorld.ViewModel;

namespace VoucherWorld.Utilities
{
    public class UserAPI
    {
        public static async Task<string> LoginTask(string userName, string password)
        {
            string temp = await StaticMethod.PostHttpAsString(
                 "http://voucherworld.azurewebsites.net/api/users/signin?username=" 
                 + userName 
                 + "&password=" + password);


            try
            {
                JArray jArray = JArray.Parse(temp);
                StaticData.CurrentUser = jArray.ToObject<User>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return temp;
        }

        public static async Task<string> Register(User unUser)
        {
            string link = "http://voucherworld.azurewebsites.net/api/users/signup";
            WebRequest request = WebRequest.Create(new Uri(link, UriKind.Absolute));

            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            //request.Headers["Connection"] = "Keep-Alive";
            //request.Headers[HttpRequestHeader.Referer] = link;
            //request.Headers[HttpRequestHeader.Host] = "radiovietnam.vn";

            Stream postStream = await request.GetRequestStreamAsync();
            string postData = "Name=" + StaticData.CurrentUser.Name
                              + "&UserName=" + StaticData.CurrentUser.UserName
                              + "&Password=" + StaticData.CurrentUser.Password
                              + "&Email=" + StaticData.CurrentUser.Email
                              + "&Address=" + StaticData.CurrentUser.Address
                              + "&PhoneNumber=" + StaticData.CurrentUser.PhoneNumber
                              + "&IsFacebookUser=" + StaticData.CurrentUser.IsFacebookUser;


            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            postStream.Write(byteArray, 0, byteArray.Length);
            postStream.Close();

            WebResponse response = await request.GetResponseAsync();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
            string result = reader.ReadToEnd();

            return result;
        }

        public static async Task<string> AnswerScalingQuestionTask(string answer, string userId, string routeId, DependencyObject sender)
        {
            StaticMethod.ShowProgress(sender, "Submitting your answer", 0, true, true);

            string link = "http://voucherworld.azurewebsites.net/api/questions/scaling?userId=" + userId + "&placeId=" +
                          routeId + "&answer=" + answer;
            WebRequest request = WebRequest.Create(new Uri(link, UriKind.Absolute));

            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";

            //Stream postStream = await request.GetRequestStreamAsync();
            //string postData = "answer=" + answer;

            //byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            //postStream.Write(byteArray, 0, byteArray.Length);
            //postStream.Close();

            WebResponse response = await request.GetResponseAsync();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
            string result = reader.ReadToEnd();

            StaticMethod.ShowProgress(sender, "Submitting your answer", 0, true, false);

            return result;
        }

        public static async Task<string> AnswerOpenEndedQuestionTask(string answer, string userId, string routeId, DependencyObject sender)
        {
            StaticMethod.ShowProgress(sender, "Submitting your answer", 0, true, true);

            string link = "http://voucherworld.azurewebsites.net/api/questions/openended?userId=" + userId + "&routeId=" +
                          routeId + "&answer=" + HttpUtility.UrlEncode(answer);
            WebRequest request = WebRequest.Create(new Uri(link, UriKind.Absolute));

            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";

            //Stream postStream = await request.GetRequestStreamAsync();
            //string postData = "answer=" + answer;

            //byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            //postStream.Write(byteArray, 0, byteArray.Length);
            //postStream.Close();

            WebResponse response = await request.GetResponseAsync();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
            string result = reader.ReadToEnd();

            StaticMethod.ShowProgress(sender, "Submitting your answer", 0, true, false);

            return result;
        }
    }
}
