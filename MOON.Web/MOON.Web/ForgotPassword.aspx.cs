using MOON.Services.User;
using System;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.VisualBasic.Devices;

namespace MOON.Web
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnSendResetMail(object sender, EventArgs e)
        {
            string email = txtEmail.Text.ToString();
            var formatemail = ValidateEmail(email);
            UserService userService = new UserService();
            DataTable dt = userService.GetSpecific(email, "Email");
            if (!(formatemail.Success))
            {
                lblErrorMsg.Text = "Email format is incorrect!.";
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    string body = string.Empty;
                    using (StreamReader read = new StreamReader(Server.MapPath("~/Template/reset_mail.html")))
                    {
                        body = read.ReadToEnd();
                    }
                    SendMail(email, body);
                }
                else
                {
                    lblErrorMsg.Text = "Email address does not exist, try another one!.";

                }
            }

        }

        private Match ValidateEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var match = regex.Match(email);
            return match;
        }

        private void SendMail(string mail, string messagebody)
        {
            var versionname = new ComputerInfo().OSFullName;
            string username = mail.Substring(0, mail.IndexOf("@"));
            MailAddress from = new MailAddress(mail);
            MailAddress to = new MailAddress("Moon@mailtrap.io");
            MailMessage message = new MailMessage(from, to);
            message.Body = messagebody;
            message.IsBodyHtml = true;
            string mailbody = messagebody.Replace("#name#", username).Replace("#opertaion_system#", versionname).Replace("#browser_name#", HttpContext.Current.Request.Browser.Browser);
            message.Body = mailbody;
            message.Subject = "Hello ," + username;
            Session["Status"] = "change on";

            SmtpClient client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("b175e99d9a1a8e", "f40868ff58681c"),
                EnableSsl = true
            };
            try
            {
                client.Send(message);
                Session["ResetPassword"] = txtEmail.Text.ToString();
                lblSuccessMsg.Text = "We send you a rest password page link in your mail box.";
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}