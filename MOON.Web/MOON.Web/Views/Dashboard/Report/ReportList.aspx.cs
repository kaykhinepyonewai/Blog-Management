using MOON.Entities.Dashboard;
using MOON.Services.Comment;
using MOON.Services.Dashboard;
using MOON.Services.Like;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;

namespace MOON.Web.Views.Dashboard.Report
{
    public partial class ReportList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        /// <summary>
        ///To display all reports as grid table
        /// </summary>
        private void BindGrid()
        {
            ArticleService articleService = new ArticleService();
            if (Request.QueryString["keyword"] != null)
            {
                DataTable dt = articleService.GetReportsBySearch(Request.QueryString["keyword"].ToString());
                gvReports.DataSource = dt;
                gvReports.DataBind();
            }
            else
            {
                DataTable dt = articleService.GetReports();
                gvReports.DataSource = dt;
                gvReports.DataBind();
            }
        }

        /// <summary>
        ///For pagination
        /// </summary>
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReports.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        protected void gvReportPostRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                Response.Redirect("~/Views/Dashboard/Report/ReportDetail.aspx?id=" + e.CommandArgument);
            }
        }
    }
}