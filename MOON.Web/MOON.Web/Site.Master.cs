using MOON.Services.User;
using System;
using System.Data;
using System.Web.UI;

namespace MOON.Web
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(Session["Users"] != null)
                {
                    LoadedInfo();
                }
            }
        }

        private void LoadedInfo()
        {
            if (Session["Users"] != null)
            {
                    string[] user = (string[])Session["Users"];
                    UserService userService = new UserService();
                try
                {
                    DataTable dt = userService.GetId(Convert.ToInt32(user[0]));
                    imgProfile.ImageUrl = dt.Rows[0]["Profile"].ToString();
                    username.InnerText = dt.Rows[0]["Username"].ToString();
                    hyprLnkProfile.NavigateUrl = "~/Views/Dashboard/User/UserProfile.aspx?email=" + dt.Rows[0]["Email"].ToString();
                }catch(Exception ex)
                {
                    string message = ex.Message;
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
        protected void chgSearch(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx?keyword=" + txtMainSearch.Text);
        }
    }
}