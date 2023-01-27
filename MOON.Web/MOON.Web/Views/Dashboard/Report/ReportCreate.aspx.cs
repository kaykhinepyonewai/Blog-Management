using MOON.Entities.Dashboard;
using MOON.Entities.Report;
using MOON.Services.Comment;
using MOON.Services.Dashboard;
using MOON.Services.Like;
using MOON.Services.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

        protected void gvRowDeleteing(object sender, EventArgs e)
        {
            ReportService reportService = new ReportService();
            ArticleService articleService = new ArticleService();
            PhotoService photoService = new PhotoService();
            CommentService commentService = new CommentService();
            LikeService likeService = new LikeService();

            int id = Convert.ToInt32(hdnValueId.Value.ToString());

            DataTable articledt = articleService.GetArticleByReport(id);
            foreach (DataRow row in articledt.Rows)
            {
                // Do something with the data in the current row
                string value = row["Thumbnail"].ToString();
                string checkthumbpath = Server.MapPath(value);
                string filethumbpath = Path.GetFullPath(checkthumbpath);
                if (value != null)
                {
                    try
                    {
                        File.Delete(filethumbpath);
                    }
                    catch (Exception ex)
                    {
                        string messsage = ex.Message.ToString();
                    }
                }

            }

            foreach (DataRow row in articledt.Rows)
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


            bool success = reportService.Delete(id);
           if(success)
            {
                BindGrid();
            }
        }
    }
}