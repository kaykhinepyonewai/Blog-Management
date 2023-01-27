using MOON.Services.User;
using System;
using System.Data;
using System.Web;

namespace MOON.Web
{
    public partial class Dashboard : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Users"] == null && Session.Count == 0)
                {
                    Response.Redirect("~/Login.aspx");
                }else
                {
                    LoadedInfo();
                }
            }
        }
        protected void lnkBtnLogoutClick(object sender, EventArgs e)
        {
            if (Session.Count != 0)
            {
                Session.RemoveAll();
                Session.Clear();
                Session.Abandon();
                Response.Redirect("~/Login.aspx");
            }
        }

        private void LoadedInfo()
        {
            string[] user = (string[])Session["Users"];
            UserService userService = new UserService();
            DataTable dt = userService.GetId(Convert.ToInt32(user[0]));
            imgProfile.ImageUrl = dt.Rows[0]["Profile"].ToString();
            username.InnerText = dt.Rows[0]["Username"].ToString();
            hyprLnkProfile.NavigateUrl = "~/Views/Dashboard/User/UserProfile.aspx?email=" + dt.Rows[0]["Email"].ToString();
            hyprLnkPwdSetting.NavigateUrl = "~/Views/Dashboard/User/PasswordSetting.aspx?email=" + dt.Rows[0]["Email"].ToString();
        }

        protected void lnkBtnHomeClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Dashboard/Home.aspx");
        }

        protected void chgSearchByText(object sender, EventArgs e)
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;

            if ( path == "/Views/Dashboard/User/UserList")
            {
                Response.Redirect("~/Views/Dashboard/User/UserList.aspx?keyword=" + txtSearch.Text);

            } else if( path == "/Views/Dashboard/Article/ArchieveList")
            {
                Response.Redirect("~/Views/Dashboard/Article/ArchieveList.aspx?keyword=" + txtSearch.Text);

            } else if( path == "/Views/Dashboard/Article/WaitingList")
            {
                Response.Redirect("~/Views/Dashboard/Article/WaitingList.aspx?keyword=" + txtSearch.Text);

            } else if( path == "/Views/Dashboard/RequestPost/RequestPost")
            {
                Response.Redirect("~/Views/Dashboard/RequestPost/RequestPost.aspx?keyword=" + txtSearch.Text);

            } else if( path == "/Views/Dashboard/Report/ReportList")
            {
                Response.Redirect("~/Views/Dashboard/Report/ReportList.aspx?keyword=" + txtSearch.Text);

            } else if ( path == "/Views/Dashboard/Comment/CommentList")
            {
                Response.Redirect("~/Views/Dashboard/Comment/CommentList.aspx?keyword=" + txtSearch.Text);

            } else if ( path == "/Views/Dashboard/Category/CategoryList")
            {
                Response.Redirect("~/Views/Dashboard/Category/CategoryList.aspx?keyword=" + txtSearch.Text);
            }
            else
            {
                Response.Redirect("~/Views/Dashboard/Article/ArticleList.aspx?keyword=" + txtSearch.Text);
            }
        }
    }
}