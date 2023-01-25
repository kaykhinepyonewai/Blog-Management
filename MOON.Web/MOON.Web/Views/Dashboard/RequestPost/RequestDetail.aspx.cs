using MOON.Entities.Dashboard;
using MOON.Services.Dashboard;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Web;
using Microsoft.VisualBasic.Devices;
using MOON.Services.User;
using System.Security.Policy;

namespace MOON.Web.Views.Dashboard.RequestPost
{
    public partial class RequestDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session.Count != 0)
                {
                    UserService userService = new UserService();
                    string[] user = (string[])Session["Users"];
                    DataTable dt = userService.GetId(Convert.ToInt32(user[0]));
                    if (Convert.ToInt32(dt.Rows[0]["RoleId"].ToString()) == 1)
                    {
                        if (Request.QueryString["id"] != null)
                        {
                            hdArticleId.Value = Request.QueryString["id"].ToString();
                            GATDetail();
                        }
                    }else
                    {
                        Response.Write("<script>history.go(-1)</script>");
                    }
                }
            }
        }

        public List<ArticleEntity> GATDetail()
        {
                hdArticleId.Value = Request.QueryString["id"].ToString();
                ArticleService articleService = new ArticleService();
                List<ArticleEntity> cc = articleService.GATDetail(Convert.ToInt32(hdArticleId.Value));
                return cc;
        }


        public List<PhotoEntity> GTImage()
        {
            PhotoService photoService = new PhotoService();
            List<PhotoEntity> cc = photoService.GetAllImage(Convert.ToInt32(hdArticleId.Value));
            return cc;
        }

        protected void BtnApproveClick(object sender, EventArgs e)
        {
            bool success;
            ArticleService articleService = new ArticleService();
            ArticleEntity articleEntity = CreateData();

            success = articleService.UpdateStatus(articleEntity);

            if (success)
            {
                Response.Redirect("~/Views/Dashboard/RequestPost/RequestPost.aspx");
            }
        }

        protected void BtnRejectClick(object sender, EventArgs e)
        {
           
            bool success;
            try
            {
                ArticleService articleService = new ArticleService();
                PhotoService photoService = new PhotoService();
                ArticleEntity articleEntity = CreateDataReject();
                PhotoEntity photoEntity = CreatePhotoData();

                DataTable dt1 = articleService.GetThumbnail((Convert.ToInt32(hdArticleId.Value)));
                string checkpath1 = Server.MapPath(dt1.Rows[0]["Thumbnail"].ToString());
                string filepath1 = Path.GetFullPath(checkpath1);
                if (File.Exists(filepath1))
                {
                    File.Delete(filepath1);
                }

                DataTable dt = photoService.GetImage(Convert.ToInt32(hdArticleId.Value));

                foreach (DataRow row in dt.Rows)
                {
                    string checkpath = Server.MapPath(row["PhotoName"].ToString());
                    string filepath = Path.GetFullPath(checkpath);

                    if (File.Exists(filepath))
                    {
                        File.Delete(filepath);
                    }
                }

                DataTable dtGmail = articleService.GetEmail(Convert.ToInt32(hdArticleId.Value));

                success = articleService.UpdateStatusReject(articleEntity);

                string body = string.Empty;
                using (StreamReader read = new StreamReader(Server.MapPath("~/Template/reject_post.html")))
                {
                    body = read.ReadToEnd();
                }
                SendMail(dtGmail.Rows[0]["Email"].ToString(), body);

                if (success)
                {
                    Response.Redirect("~/Views/Dashboard/RequestPost/RequestPost.aspx");
                }
            }catch (Exception ex) {
                string msg = ex.Message;
                Response.Redirect("~/Views/Dashboard/RequestPost/RequestPost.aspx");
            }

        }


        private void SendMail(string mail, string messagebody)
        {
            var versionname = new ComputerInfo().OSFullName;
            string username = mail.Substring(0, mail.IndexOf("@"));

            SmtpClient smtp = new SmtpClient("smtp.mailtrap.io", 2525);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("a70f64ecb948dd", "9c05f17a0375b0");
            MailMessage message = new MailMessage();
            message.From = new MailAddress("kaykhinepyonewai@gmail.com");
            message.IsBodyHtml = true;
            message.Subject = "Password reset link";

            string mailbody = messagebody.Replace("#name#", username).Replace("#opertaion_system#", versionname).Replace("#browser_name#", HttpContext.Current.Request.Browser.Browser);
           

            message.Body = mailbody;
            message.To.Add(mail);
            try
            {
                smtp.Send(message);
               
            }
            catch (SmtpException ex)
            {
                string msg = ex.Message;
            }
        }


        public ArticleEntity CreateData()
        {
            ArticleEntity articleEntity = new ArticleEntity();

            articleEntity.ArticleId = Convert.ToInt32(hdArticleId.Value);
            articleEntity.Status = "active";
            
            return articleEntity;
        }


        public PhotoEntity CreatePhotoData()
        {
            PhotoEntity phototEntity = new PhotoEntity();

            phototEntity.ArticleId = Convert.ToInt32(hdArticleId.Value);
           
            return phototEntity;
        }

        public ArticleEntity CreateDataReject()
        {
            ArticleEntity articleEntity = new ArticleEntity();

            articleEntity.ArticleId = Convert.ToInt32(hdArticleId.Value);
            articleEntity.Status = "reject";

            return articleEntity;
        }


    }
}