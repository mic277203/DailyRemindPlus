﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Linq;
using System.IO;
using Newtonsoft.Json;


namespace DailyRemindPlus
{
    public class WeekRemindService : RemindServiceBase
    {
        public override void Check()
        {
            if (SetCookies())
            {
                var listModels = GetList();
                if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                {
                    var str = DateTime.Now.ToString("yyyyMMdd");
                    var result = listModels.Any(p => p.BillDate == str);

                    if (!result)
                    {
                        string title = $"{DateTime.Now.ToString("yyyy-MM-dd")} 周报填写提醒";
                        string content = $"Hi,< br />< b >{ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}< b /> 检测到你没有填写周报,请安排时间填写! < br /> < a href =\"http://192.168.1.7/EIPDevManager/Login\">前往</a> ";
                        EmailHelper.SendEmail(title, content);
                    }
                }
            }
        }

        /// <summary>
        /// 设置Cookies
        /// </summary>
        /// <returns></returns>
        private bool SetCookies()
        {
            var result = Login(new LoginModel()
            {
                UserName = "lph",
                Password = "lph"
            });

            return result;
        }

        /// <summary>
        /// 获取周报列表
        /// </summary>
        /// <returns></returns>
        private List<WeekModel> GetList()
        {
            var request = (HttpWebRequest)WebRequest.Create(ConstTag.WeekUrl);
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36";
            request.ContentType = "application/json; charset=utf-8";
            request.CookieContainer = cookieContainer;

            HttpWebResponse myResponse;

            try
            {
                myResponse = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException we)
            {
                myResponse = (HttpWebResponse)we.Response;
            }

            var result = string.Empty;

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

            return GetModelList(result);
        }

        /// <summary>
        /// 获取周报
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private List<WeekModel> GetModelList(string result)
        {
            if (string.IsNullOrEmpty(result))
                return null;

            var strDaily = result.Substring(1, result.IndexOf("]", StringComparison.Ordinal));

            var listModels = JsonConvert.DeserializeObject<List<WeekModel>>(strDaily);

            return listModels;
        }
    }
}
