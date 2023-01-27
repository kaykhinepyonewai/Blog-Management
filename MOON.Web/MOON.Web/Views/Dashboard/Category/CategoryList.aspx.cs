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

namespace MOON.Web.Views.Dashboard.Category
{
    public partial class CategoryList : System.Web.UI.Page
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
                    try
                    {
                        if (Convert.ToInt32(dt.Rows[0]["RoleId"]) == 1)
                        {
                            BindGrid();
                        }
                        else
                        {
                            Response.Write("<script>history.go(-1)</script>");
                        }
                    }
                    catch (Exception)
                    {
                        Response.Write("<script>history.go(-1)</script>");
                    }
                }
            }
        }

        private void BindGrid()
        {
            CategoryService categoryService = new CategoryService();
            if(Request.QueryString["keyword"] != null)
            {
                DataTable dt = categoryService.GetAllBySearch(Request.QueryString["keyword"].ToString());
                gvCategories.DataSource = dt;
                gvCategories.DataBind();
            }
            else
            {
                DataTable dt = categoryService.GetAll();
                gvCategories.DataSource = dt;
                gvCategories.DataBind();
            }
         }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCategories.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }


        protected void gvCategoryRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                Response.Redirect("~/Views/Dashboard/Category/CategoryCreate.aspx?id=" + e.CommandArgument);
            }
        }

        protected void gvRowDeleteing(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hdnValueId.Value.ToString());
            CategoryService categoryService = new CategoryService();
            ArticleService articleService = new ArticleService();
            PhotoService photoService = new PhotoService();
            CommentService commentService = new CommentService();
            LikeService likeService = new LikeService();

            DataTable dt = articleService.GetArticleByCategory(id);
            foreach (DataRow row in dt.Rows)
            {
                // Do something with the data in the current row
                string value = row["Thumbnail"].ToString();
                string checkpath = Server.MapPath(value);
                string filepath = Path.GetFullPath(checkpath);
                if (value != null)
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

            }

            foreach (DataRow row in dt.Rows)
            {
                // Do something with the data in the current row
                int articleid = Convert.ToInt32(row["ArticleId"].ToString());
                List<PhotoEntity> photos = photoService.GetAllImages(articleid);
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
                bool commentsuccess = commentService.DeleteSpecificArticle(articleid);
                bool likesuccess = likeService.DeleteSpecificArticle(articleid);
                bool photosuccess = photoService.ReportAllPhotosRemove(articleid);
                bool articlesuccess = articleService.RemoveAllArticles(articleid);
            }

            bool success = categoryService.Delete(id);
            if (success)
            {
                BindGrid();
            }
        }
    }
}