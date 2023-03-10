using MOON.Services.Dashboard;
using MOON.Services.User;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace MOON.Web.Views.Dashboard.Article
{
    public partial class TrendArticle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Session.Count != 0 && Session["Users"] != null)
                {
                    UserService userService = new UserService();
                    string[] user = (string[])Session["Users"];
                    DataTable dt = userService.GetId(Convert.ToInt32(user[0]));
                    if (Convert.ToInt32(dt.Rows[0]["RoleId"].ToString()) == 1)
                    {
                        BindGrid();
                    }
                    else
                    {
                        Response.Write("<script>history.go(-1)</script>");
                    }
                }
            }
        }

        private void BindGrid()
        {
            ArticleService articleService = new ArticleService();
            DataTable dt = articleService.GetTrends();
            gvTrendArticle.DataSource = dt;
            gvTrendArticle.DataBind();
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTrendArticle.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        protected void gvTrendArticleRowCommand(object sender, GridViewCommandEventArgs e)
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
                BindGrid();
            }
        }
    }
}