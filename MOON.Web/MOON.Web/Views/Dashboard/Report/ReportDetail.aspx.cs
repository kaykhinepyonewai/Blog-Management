using MOON.Entities.Dashboard;
using MOON.Services.Comment;
using MOON.Services.Dashboard;
using MOON.Services.Like;
using MOON.Services.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace MOON.Web.Views.Dashboard.Report
{
    public partial class ReportDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session.Count != 0 && Session["Users"] != null)
                {
                    UserService userService = new UserService();
                    string[] user = (string[])Session["Users"];
                    DataTable dt = userService.GetId(Convert.ToInt32(user[0]));
                    if (Convert.ToInt32(dt.Rows[0]["RoleId"].ToString()) == 1)
                    {
                        if (Request.QueryString["id"] != null)
                        {
                            hdArticleId.Value = Request.QueryString["id"].ToString();
                            GATReportDetail();
                        }
                    }
                    else
                    {
                        Response.Write("<script>history.go(-1)</script>");
                    }
                }
            }
        }

        public List<ArticleEntity> GATReportDetail()
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

        protected void BtnSuppressClick(object sender, EventArgs e)
        {
            bool success;
            ArticleService articleService = new ArticleService();
            ArticleEntity articleEntity = CreateData();

            success = articleService.UpdateReportStatus(articleEntity);

            if (success)
            {
                Response.Redirect("~/Views/Dashboard/Report/ReportList.aspx");
            }
        }

        /// <summary>
        ///Once you delete a report, it will also delete the associated article at the same time.
        /// </summary>
        protected void BtnDeleteClick(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hdArticleId.Value.ToString());
            ArticleService articleService = new ArticleService();
            PhotoService photoService = new PhotoService();
            CommentService commentService = new CommentService();
            LikeService likeService = new LikeService();

            DataTable dt = articleService.GetSpecificArchieve(id);
            string thumbnail = dt.Rows[0]["Thumbnail"].ToString();
            string checkpath = Server.MapPath(thumbnail);
            string filepath = Path.GetFullPath(checkpath);
            if (thumbnail != null)
            {
                try
                {
                    File.Delete(filepath);
                }
                catch (Exception ex)
                {
                    string messsage = ex.Message.ToString();
                }
            }

            List<PhotoEntity> photos = photoService.GetAllImage(id);
            if (photos.Count > 0)
            {
                foreach (var photo in photos)
                {
                    string img = photo.PhotoImage.ToString();
                    string checkpathimg = Server.MapPath(img);
                    string filepathimg = Path.GetFullPath(checkpathimg);
                    if (img != null)
                    {
                        try
                        {
                            File.Delete(filepathimg);
                        }
                        catch (Exception ex)
                        {
                            string messsage = ex.Message.ToString();
                        }
                    }
                }
            }
            bool commentsuccess = commentService.DeleteSpecificArticle(id);
            bool likesuccess = likeService.DeleteSpecificArticle(id);
            bool photosuccess = photoService.ReportPhotosRemove(id);
            bool success = articleService.ReportRemove(id);
            if (commentsuccess || likesuccess || photosuccess || success)
            {
                Response.Redirect("~/Views/Dashboard/Report/ReportList.aspx");
            }

        }

        public ArticleEntity CreateData()
        {
            ArticleEntity articleEntity = new ArticleEntity();

            articleEntity.ArticleId = Convert.ToInt32(hdArticleId.Value);
            articleEntity.ReportStatus = 0;
            articleEntity.ReportId = 12;
            articleEntity.ReportAt = DateTime.Now;

            return articleEntity;
        }


        
    }
}