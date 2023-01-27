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

        /// <summary>
        ///Once you delete a report, it will also delete the associated article at the same time.
        /// </summary>
        protected void gvRowDeleteing(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hdnValueId.Value.ToString());
            ArticleService articleService = new ArticleService();
            PhotoService photoService = new PhotoService();
            CommentService commentService = new CommentService();
            LikeService likeService = new LikeService();

            DataTable dt = articleService.GetSpecificArchieve(id);
            string thumbnail = dt.Rows[0]["Thumbnail"].ToString();
            string checkpath = Server.MapPath(thumbnail);
            string filepath = Path.GetFullPath(checkpath);
            if (thumbnail != null)
            {
                try
                {
                    File.Delete(filepath);
                }
                catch (Exception ex)
                {
                    string messsage = ex.Message.ToString();
                }
            }

            List<PhotoEntity> photos = photoService.GetAllImage(id);
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
            bool commentsuccess = commentService.DeleteSpecificArticle(id);
            bool likesuccess = likeService.DeleteSpecificArticle(id);
            bool photosuccess = photoService.ReportPhotosRemove(id);
            bool success = articleService.ReportRemove(id);
            if (commentsuccess || likesuccess || photosuccess || success)
            {
                BindGrid();
            }
           
        }
    }
}