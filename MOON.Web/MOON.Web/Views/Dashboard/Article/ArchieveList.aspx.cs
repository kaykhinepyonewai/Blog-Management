using MOON.Entities.Dashboard;
using MOON.Services.Comment;
using MOON.Services.Dashboard;
using MOON.Services.Like;
using MOON.Services.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;

namespace MOON.Web.Views.Dashboard.Article
{
    public partial class ArchieveList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session.Count != 0)
                {
                    string[] user = (string[])Session["Users"];

                    if (Convert.ToInt32(user[1]) != 3)
                    {
                        BindData();
                    }

                }
            }
        }

        void BindData()
        {
            ArticleService articleService = new ArticleService();
            UserService userService = new UserService();
            DataTable user = userService.GetId(Convert.ToInt32(Session["UserId"].ToString()));
            if (Request.QueryString["keyword"] != null)
            {
                DataTable dt = articleService.GetArchieveBySearch(Convert.ToInt32(user.Rows[0]["UserId"].ToString()), Request.QueryString["keyword"].ToString());
                gvArchieveArticle.DataSource = dt;
                gvArchieveArticle.DataBind();
            }
            else
            {
                DataTable dt = articleService.GetArchieve(Convert.ToInt32(user.Rows[0]["UserId"].ToString()));
                gvArchieveArticle.DataSource = dt;
                gvArchieveArticle.DataBind();
            }
            
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvArchieveArticle.PageIndex = e.NewPageIndex;
            this.BindData();
        }

        protected void gvArchieveArticleRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label lblArticleId = (Label)gvArchieveArticle.Rows[e.RowIndex].FindControl("lblArticleId");
            ArticleService articleService = new ArticleService();
            PhotoService photoService = new PhotoService();
            CommentService commentService = new CommentService();
            LikeService likeService = new LikeService();

            DataTable dt = articleService.GetSpecificArchieve(Convert.ToInt32(lblArticleId.Text));
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

            List<PhotoEntity> photos = photoService.GetArchieveImages(Convert.ToInt32(lblArticleId.Text));
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
            bool photosuccess = photoService.Remove(Convert.ToInt32(lblArticleId.Text));
            bool commentsuccess = commentService.DeleteSpecificArticle(Convert.ToInt32(lblArticleId.Text));
            bool likesuccess = likeService.DeleteSpecificArticle(Convert.ToInt32(lblArticleId.Text));
            bool success = articleService.Remove(Convert.ToInt32(lblArticleId.Text));
            if (success || photosuccess || commentsuccess || likesuccess)
            {
                Response.Redirect("~/Views/Dashboard/Article/ArchieveList.aspx");
                BindData();
            }

        }

        protected void gvArchieveArticleRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Unarchieve")
            {
                ArticleService articleService = new ArticleService();
                PhotoService photoService = new PhotoService();
                photoService.UnArchieve(Convert.ToInt32(e.CommandArgument));
              bool success =  articleService.UnArchieve(Convert.ToInt32(e.CommandArgument));
                
                if (success)
                {
                    Response.Redirect("~/Views/Dashboard/Article/ArchieveList.aspx");
                }
            }
        }

    }
}