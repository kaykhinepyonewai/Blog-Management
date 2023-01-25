using MOON.Services.Dashboard;
using MOON.Services.User;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace MOON.Web.Views.Dashboard.RequestPost
{
    public partial class RequestPost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(Session.Count != 0)
                {
                    UserService userService = new UserService();
                    string[] user = (string[])Session["Users"];
                    DataTable dt = userService.GetId(Convert.ToInt32(user[0]));
                    if (Convert.ToInt32(dt.Rows[0]["RoleId"].ToString()) == 1)
                    {
                        BindGrid();
                    }else
                    {
                        Response.Write("<script>history.go(-1)</script>");
                    }
                }
            }
        }

       private void BindGrid()
        {
            ArticleService articleService = new ArticleService();
            if (Request.QueryString["keyword"] != null)
            {
                DataTable dt = articleService.GetAllPendingBySearch(Request.QueryString["keyword"].ToString());
                gvRequestPost.DataSource = dt;
                gvRequestPost.DataBind();
            }else
            {
                DataTable dt = articleService.GetAllPending();
                gvRequestPost.DataSource = dt;
                gvRequestPost.DataBind();
            }
        }

        protected void gvRequestPostRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                Response.Redirect("~/Views/Dashboard/RequestPost/RequestDetail.aspx?id=" + e.CommandArgument);
            }
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRequestPost.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

    }
}