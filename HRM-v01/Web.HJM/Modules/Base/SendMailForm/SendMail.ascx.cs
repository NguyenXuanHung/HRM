using System;
using System.Net;
using Ext.Net;
using System.Net.Mail;
using System.Text;
using SoftCore.Utilities;
using Web.Core.Framework;

namespace Web.HJM.Modules.UserControl
{
    public partial class Modules_Base_SendMailForm_SendMail : BaseUserControl, IBaseControl, IBaseWindow
    {
        public string MailTemplateFolder { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                txtMailGo.Text = CurrentUser.User.Email;

            }
        }

        /// <summary>
        /// Thiết lập địa chỉ mail gửi đi
        /// </summary>
        /// <param name="Email"></param>
        public void SetEmailTo(string MailFrom, string Pasword, string MailTo)
        {
            txtMailGo.Text = MailFrom;
            txtPassword.Text = Pasword;
            txtMailTo.Text = MailTo;
        }

        protected void btnSendMail_Click(object sender, DirectEventArgs e)
        {
            if (Email.SendEmail(txtMailGo.Text, txtPassword.Text, CurrentUser.User.DisplayName, txtMailTo.Text,
                txtTieuDe.Text, htmlMail.Text))
            {
                wdSendEmail.Hide();
            }

            SendMail("smtp.gmail.com", 587, txtMailGo.Text, txtPassword.Text, CurrentUser.User.DisplayName,
                txtMailTo.Text, txtTieuDe.Text, htmlMail.Text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host">smtp.gmail.com</param>
        /// <param name="port">587</param>
        /// <param name="mailsend">Địa chỉ mail gửi</param>
        /// <param name="password">Mật khẩu địa chỉ gửi</param>
        /// <param name="MailName">Tên người gửi</param>
        /// <param name="mailto">Mail đến</param>
        /// <param name="titlemail">Tiêu đề mail</param>
        /// <param name="bodymail">Nội dung mail</param>
        public void SendMail(string host, int port, string mailsend, string password, string MailName, string mailto,
            string titlemail, string bodymail)
        {
            #region[Sendmail]

            var mailMessage = new MailMessage {From = (new MailAddress(mailsend, MailName, Encoding.UTF8))};
            mailMessage.To.Add(mailto);
            mailMessage.Bcc.Add(mailto);
            mailMessage.Subject = titlemail;
            mailMessage.SubjectEncoding = Encoding.UTF8;
            mailMessage.Body = bodymail;
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = Encoding.UTF8;
            var mailAuthentication = new NetworkCredential
            {
                UserName = mailsend,
                Password = password
            };
            var mailClient = new SmtpClient(host, port)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = mailAuthentication
            };
            try
            {
                mailClient.Send(mailMessage);
                Dialog.ShowNotification("Hệ thống đã gửi mail thành công");
            }
            catch (Exception ex)
            {
                Dialog.ShowNotification("Hệ thống đã  có lỗi xẩy ra khi gửi mail!");
            }

            #endregion
        }


        public string GetID()
        {
            throw new NotImplementedException();
        }

        public object GetValue()
        {
            throw new NotImplementedException();
        }

        public void SetValue(object value)
        {
            throw new NotImplementedException();
        }

        public void Show()
        {
            wdSendEmail.Show();
        }
    }
}