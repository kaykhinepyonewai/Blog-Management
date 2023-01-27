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
                if (Session.Count != 0 && Session["Users"] != null)
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

            string[] user = (string[])Session["Users"]; // get specific user info in the session array
            DataTable dt = userService.GetId(Convert.ToInt32(user[0]));
            int role = Convert.ToInt32(dt.Rows[0]["RoleId"].ToString());
            DataTable userdt = userService.GetId(Convert.ToInt32(Session["UserId"].ToString()));
           if(role == 1)
            {
                if (Request.QueryString["keyword"] != null)
                {
                    DataTable dt1 = articleService.GetAllArchivesBySearch(Request.QueryString["keyword"].ToString());
                    gvArchieveArticle.DataSource = dt1;
                    gvArchieveArticle.DataBind();
                }
                else
                {
                    DataTable dt2 = articleService.GetAllArchives();
                    gvArchieveArticle.DataSource = dt2;
                    gvArchieveArticle.DataBind();
                }
            }
            else
            {
                if (Request.QueryString["keyword"] != null)
                {
                    DataTable dt3 = articleService.GetArchieveBySearch(Convert.ToInt32(userdt.Rows[0]["UserId"].ToString()), Request.QueryString["keyword"].ToString());
                    gvArchieveArticle.DataSource = dt3;
                    gvArchieveArticle.DataBind();
                }
                else
                {
                    DataTable dt4 = articleService.GetArchieve(Convert.ToInt32(userdt.Rows[0]["UserId"].ToString()));
                    gvArchieveArticle.DataSource = dt4;
                    gvArchieveArticle.DataBind();
                }
            }
            
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvArchieveArticle.PageIndex = e.NewPageIndex;
            this.BindData();
        }

        protected void gvRowDeleteing(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hdnValueId.Value.ToString());
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

            List<PhotoEntity> photos = photoService.GetArchieveImages(id);
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
            bool photosuccess = photoService.Remove(id);
            bool commentsuccess = commentService.DeleteSpecificArticle(id);
            bool likesuccess = likeService.DeleteSpecificArticle(id);
            bool success = articleService.Remove(id);
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