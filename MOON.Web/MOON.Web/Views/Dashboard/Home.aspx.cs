using MOON.Entities.Dashboard;
using MOON.Services.Dashboard;
using MOON.Services.User;
using System;
using System.Collections.Generic;
using System.Data;

namespace MOON.Web.Views.Dashboard
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session.Count != 0 && Session["Users"] != null )
                {
                    string[] user = (string[])Session["Users"];

                    if (Convert.ToInt32(user[1]) != 3)
                    {
                        UserService userService = new UserService();
                        DataTable dt = userService.GetId(Convert.ToInt32(user[0]));

                        if (Convert.ToInt32(dt.Rows[0]["RoleId"]) == 2)
                        {
                            Response.Redirect("~/Views/Dashboard/Article/ArticleList.aspx");
                        }
                        else
                        {
                            username.InnerText = "Hello, " + dt.Rows[0]["Username"].ToString();
                            CountBox();
                        }
                    }
                    
                }
               
            }
        }

        private void CountBox()
        {
            UserService userService = new UserService();
            ArticleService articleService = new ArticleService();
            string[] user = (string[])Session["Users"];
            DataTable userdt = userService.GetId(Convert.ToInt32(user[0]));
            DataTable reportdt = articleService.GetReports();

            DataTable dt = userService.CountAll();
            DataTable articledt = articleService.GetAll();
            DataTable archievedt = articleService.GetArchieve(Convert.ToInt32(userdt.Rows[0]["UserId"].ToString()));
            userCount.InnerText = dt.Rows.Count.ToString();
            articleCount.InnerText = articledt.Rows.Count.ToString();
            ArchieveCount.InnerText = archievedt.Rows.Count.ToString();
            reportCount.InnerText = reportdt.Rows.Count.ToString();
        }

        public List<ArticleEntity> GetTrending()
        {

            ArticleService articleService = new ArticleService();
            List<ArticleEntity> cc = articleService.GetTrending();
            return cc;
        }

    }
}