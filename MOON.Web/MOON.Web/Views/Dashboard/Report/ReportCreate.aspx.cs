using MOON.Entities.Report;
using MOON.Services.Report;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace MOON.Web.Views.Dashboard.Report
{
    public partial class ReportCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    btnRpCreate.Text = "Update";
                    hdReportId.Value = Request.QueryString["id"].ToString();
                    BindData();
                }
                BindGrid();
            }
        }
        private void BindData()
        {
            ReportService reportService = new ReportService();

            DataTable dt = reportService.Get(Convert.ToInt32(hdReportId.Value));

            if (dt.Rows.Count > 0)
            {
                txtRpMessage.Text = dt.Rows[0]["Message"].ToString();
            }
        }

        private void BindGrid()
        {
            ReportService reportService = new ReportService();
            DataTable dt = reportService.GetAll();
            gvReports.DataSource = dt;
            gvReports.DataBind();
        }
        protected void RpCreateClick(object sender, EventArgs e)
        {
            CreateReport();
        }
        protected void RpCancelClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Dashboard/Report/ReportList.aspx");
        }
        private void CreateReport()
        {
            ReportService reportService = new ReportService();
            ReportEntity reportEntity = CreateData();
            int exist = reportService.Exist(reportEntity);
            bool success = false;

            if (hdReportId.Value == "0")
            {
                if (exist > 0)
                {
                    lblErrorMsg.Text = "Report message is already existed.";
                }
                else
                {
                    success = reportService.Insert(reportEntity);
                }
            }
            else
            {
                if (exist > 0)
                {
                    lblErrorMsg.Text = "Report message is already existed.";
                }
                else
                {
                    success = reportService.Update(reportEntity);

                }
            }

            if (success)
            {
                Response.Redirect("~/Views/Dashboard/Report/ReportCreate.aspx");
            }

        }

        private ReportEntity CreateData()
        {
            ReportEntity reportEntity = new ReportEntity(); ;
            reportEntity.ReportId = Convert.ToInt32(hdReportId.Value);
            reportEntity.Message = txtRpMessage.Text;
            reportEntity.CreatedAt = DateTime.Now;
            return reportEntity;
        }

        /// <summary>
        ///For pagination
        /// </summary>
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReports.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        protected void gvReportsRowEditing(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Edit") {
                Response.Redirect("~/Views/Dashboard/Report/ReportCreate.aspx?id=" + e.CommandArgument);
            }
        }

        protected void gvReportRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ReportService reportService = new ReportService();
            Label getid = (Label)gvReports.Rows[e.RowIndex].FindControl("lblReportID");
            bool success = reportService.Delete(Convert.ToInt32(getid.Text.ToString()));
           if(success)
            {
                BindGrid();
            }
        }
    }
}