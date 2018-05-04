using System;
using MimeKit;
using MailKit.Net.Smtp;
using MimeKit.Text;

namespace DailyRemindPlus
{
    public class EmailHelper
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        public static void SendEmail(string title,string content)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("LPH", "345271593@qq.com"));
            message.To.Add(new MailboxAddress("Petter", "785626232@qq.com"));

            message.Subject = title;

            message.Body = new TextPart(TextFormat.Html)
            {
                Text = content
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
    }
}
