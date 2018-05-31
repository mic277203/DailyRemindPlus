using System;
using MimeKit;
using MailKit.Net.Smtp;
using MimeKit.Text;
using log4net;

namespace DailyRemindPlus
{
    public class EmailHelper
    {
        private static ILog _log = LogManager.GetLogger(typeof(EmailHelper));
        /// <summary>
        /// 发送邮件
        /// </summary>
        public static void SendEmail(string title, string content, string name, string email)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("林培华", "785626232@qq.com"));
            message.To.Add(new MailboxAddress(name, email));
            message.Subject = title;

            message.Body = new TextPart(TextFormat.Text)
            {
                Text = content
            };

            try
            {
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect("smtp.qq.com", 465, true);
                    client.Authenticate("785626232", "gnqkakccmwpxbfii");
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }
    }
}
