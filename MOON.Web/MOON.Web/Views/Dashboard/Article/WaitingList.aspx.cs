using MOON.Services.Dashboard;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace MOON.Web.Views.Dashboard.Article
{
    public partial class WaitingList : System.Web.UI.Page
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

            int user = Convert.ToInt32(Session["UserId"]);
            ArticleService articleService = new ArticleService();
            if (Request.QueryString["keyword"] != null)
            {
                DataTable dt = articleService.GetWaitingBySearch(user, Request.QueryString["keyword"].ToString());
                gvArticle.DataSource = dt;
                gvArticle.DataBind();
            }
            else
            {
                DataTable dt = articleService.GetWaiting(user);
                gvArticle.DataSource = dt;
                gvArticle.DataBind();
            }
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