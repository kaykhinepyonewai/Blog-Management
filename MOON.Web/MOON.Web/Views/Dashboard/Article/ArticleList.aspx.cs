using MOON.Entities.Dashboard;
using MOON.Services.Dashboard;
using MOON.Services.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace MOON.Web.Views.Dashboard.Article
{
    public partial class ArticleList : System.Web.UI.Page
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
            string[] user = (string[])Session["Users"];
            UserService userService = new UserService();
            ArticleService articleService = new ArticleService();
            DataTable userdt = userService.GetId(Convert.ToInt32(user[0].ToString()));

            if (Convert.ToInt32(userdt.Rows[0]["RoleId"]) == 1 && Convert.ToInt32(userdt.Rows[0]["RoleId"]) != 3)
            {
                if (Request.QueryString["keyword"] != null)
                {
                    DataTable dt = articleService.GetAllBySearch(Request.QueryString["keyword"].ToString());
                    gvArticle.DataSource = dt;
                    gvArticle.DataBind();
                }
                else
                {
                    DataTable dt = articleService.GetAll();
                    gvArticle.DataSource = dt;
                    gvArticle.DataBind();
                }
            }
            else if(Convert.ToInt32(userdt.Rows[0]["RoleId"]) == 2 && Convert.ToInt32(userdt.Rows[0]["RoleId"]) != 3)
            {
                if (Request.QueryString["keyword"] != null)
                {
                    int id = Convert.ToInt32(userdt.Rows[0]["UserId"].ToString());
                    DataTable dt = articleService.UserGetAllBySearch(id, Request.QueryString["keyword"].ToString());
                    gvArticle.DataSource = dt;
                    gvArticle.DataBind();
                }
                else
                {
                    int id = Convert.ToInt32(userdt.Rows[0]["UserId"].ToString());
                    DataTable dt = articleService.UserGetAll(id);
                    gvArticle.DataSource = dt;
                    gvArticle.DataBind();
                }
            } 
        }

        public List<ArticleEntity> GAT()
        {

            ArticleService articleService = new ArticleService();
            List<ArticleEntity> cc = articleService.GAT();
            return cc;
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvArticle.PageIndex = e.NewPageIndex;
            this.BindData();
        }

        protected void gvArticleRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                Response.Redirect("~/Views/Dashboard/Article/ArticleCreate.aspx?id=" + e.CommandArgument);
            }
        }

        protected void gvRowDeleteing(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hdnValueId.Value.ToString());
            ArticleService articleService = new ArticleService();
            PhotoService photoService = new PhotoService();
            bool success = articleService.Delete(id);
            bool success1 = photoService.DeleteAritcle(id);

            if (success)
            {
                BindData();
            }
        }
    }
}