using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace DailyRemindPlus
{
    public abstract class RemindServiceBase
    {
        protected static CookieContainer cookieContainer = new CookieContainer();
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        protected bool Login(LoginModel model)
        {
            var data = Encoding.UTF8.GetBytes(model.GetQueryString());
            var myRequest = (HttpWebRequest)WebRequest.Create(ConstTag.LoginUrl);
            myRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36";
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            myRequest.ContentLength = data.Length;
            myRequest.CookieContainer = cookieContainer;
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

        public abstract void Check();
    }
}
