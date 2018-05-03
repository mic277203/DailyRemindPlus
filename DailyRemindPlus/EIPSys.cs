using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading;

namespace DailyRemindPlus
{

    public static class ExtensionMethods
    {
        public static string GetQueryString(this object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return string.Join("&", properties.ToArray());
        }
    }
    /// <summary>
    /// 研发管理系统
    /// </summary>
    public class EIPSys
    {
        private static CookieContainer _cookieContainer = new CookieContainer();
        private const string LoginUrl = "http://192.168.1.7/EIPDevManager/Login/UceLogin?ExcuteProcInfo=eyJQcm9nSWQiOiJEZXZMb2dpbiIsIkhhbmRsZSI6Ii0yIiwiUGFnZUlkIjoiNTM2NDA1MDUxNTE0NDUyOTA0MSJ9";
        private const string DailyUrl = "http://192.168.1.7/EIPDevManager/QueryPage/GetQueryList?ExcuteProcInfo=eyJQcm9nSWQiOiJEZXZEYWlseSIsIkhhbmRsZSI6IjEwOTk3Njc1NTQiLCJQYWdlSWQiOiI0NjQwOTM0NDYyMzk4OTgyODU0In0=&readOptions=%7B%22take%22%3A20%2C%22skip%22%3A0%2C%22page%22%3A1%2C%22pageSize%22%3A20%2C%22filter%22%3A%7B%22logic%22%3A%22or%22%2C%22filters%22%3A%5B%7B%22field%22%3A%22A.PersonName%22%2C%22operator%22%3A%22contains%22%2C%22value%22%3A%22%E6%9E%97%E5%9F%B9%E5%8D%8E%22%7D%2C%7B%22field%22%3A%22A.PositionName%22%2C%22operator%22%3A%22contains%22%2C%22value%22%3A%22%E6%9E%97%E5%9F%B9%E5%8D%8E%22%7D%2C%7B%22field%22%3A%22A.BillDate%22%2C%22operator%22%3A%22contains%22%2C%22value%22%3Anull%7D%5D%7D%7D&_=1525232237392";

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public static bool Login(LoginModel model)
        {
            byte[] data = Encoding.UTF8.GetBytes(model.GetQueryString());
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(LoginUrl);
            myRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36";
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            myRequest.ContentLength = data.Length;
            myRequest.CookieContainer = _cookieContainer;
            myRequest.AllowAutoRedirect = true;

            using (var newStream = myRequest.GetRequestStream())
            {
                newStream.Write(data, 0, data.Length);
            }

            HttpWebResponse myResponse;

            try
            {
                myResponse = (HttpWebResponse)myRequest.GetResponse();
            }
            catch (WebException we)
            {
                myResponse = (HttpWebResponse)we.Response;
            }

            return myResponse.StatusCode == HttpStatusCode.OK;
        }

        /// <summary>
        /// 循环检测
        /// </summary>
        public static void Check()
        {
            var listDailyModel = GetDailyList();

            var str = DateTime.Now.ToString("yyyyMMdd");
            var result = listDailyModel.Any(p => p.BillDate == str);

            if (!result)
            {
                SendEmail();
            }
        }

        #region private Method

        /// <summary>
        /// 获取日报列表
        /// </summary>
        /// <returns></returns>
        private static List<DailyModel> GetDailyList()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(DailyUrl);
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36";
            request.ContentType = "application/json; charset=utf-8";
            request.CookieContainer = _cookieContainer;

            HttpWebResponse myResponse;

            try
            {
                myResponse = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException we)
            {
                myResponse = (HttpWebResponse)we.Response;
            }

            string result = string.Empty;

            if (myResponse.StatusCode == HttpStatusCode.OK)
            {
                using (var myResponseStream = myResponse.GetResponseStream())
                {
                    if (myResponseStream != null)
                    {
                        using (var myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8")))
                        {
                            result = myStreamReader.ReadToEnd();
                        }
                    }
                }
            }

            return GetDailyModelList(result);
        }

        /// <summary>
        /// 获取日报
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private static List<DailyModel> GetDailyModelList(string result)
        {
            if (string.IsNullOrEmpty(result))
                return null;

            var strDaily = result.Substring(1, result.IndexOf("]", StringComparison.Ordinal));

            var listModels = JsonConvert.DeserializeObject<List<DailyModel>>(strDaily);

            return listModels;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        private static void SendEmail()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("LPH", "345271593@qq.com"));
            message.To.Add(new MailboxAddress("Petter", "785626232@qq.com"));

            message.Subject = $"{DateTime.Now.ToString("yyyy-MM-dd")} 日报填写提醒";

            message.Body = new TextPart("plain")
            {
                Text = $"Dear,{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}检测到你没有填写日报,请安排时间填写"
            };

            try
            {
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect("smtp.qq.com", 465, true);
                    client.Authenticate("345271593", "yyzaqqmqadtwbjef");
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            finally
            {
                //暂时不做处理
            }
        }

        #endregion

        #region Model
        /// <summary>
        /// 登录模型
        /// </summary>
        public class LoginModel
        {
            /// <summary>
            /// 用户名
            /// </summary>
            public string UserName { get; set; }

            /// <summary>
            /// 密码
            /// </summary>
            public string Password { get; set; }
        }

        /// <summary>
        /// 日报模型
        /// </summary>
        public class DailyModel
        {
            public string BillNo { get; set; }
            public string BillDate { get; set; }
            public string PersonId { get; set; }
            public string MakerId { get; set; }
            public string MakeDate { get; set; }
            public string ModifierId { get; set; }
            public string ModifyDate { get; set; }
            public string InternalId { get; set; }
            public string PersonName { get; set; }
            public string DeptName { get; set; }
            public string PositionName { get; set; }
            public string ToUserIds { get; set; }
            public string CcUserIds { get; set; }
            public string MailTitle { get; set; }
        }
        #endregion

    }
}
