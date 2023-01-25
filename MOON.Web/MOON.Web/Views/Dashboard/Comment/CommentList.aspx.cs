using MOON.Services.Comment;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace MOON.Web.Views.Dashboard.Comment
{
    public partial class CommentList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Users"] != null)
                {
                    BindGrid();
                }
            }
        }

        private void BindGrid()
        {
            CommentService commentService = new CommentService();
            if (Request.QueryString["keyword"] != null)
            {
                DataTable dt = commentService.GetCommentsBySearch(Convert.ToInt32(Session["UserId"].ToString()), Request.QueryString["keyword"].ToString());
                gvComments.DataSource = dt;
                gvComments.DataBind();
            }
            else
            {
                DataTable dt = commentService.GetComments(Convert.ToInt32(Session["UserId"].ToString()));
                gvComments.DataSource = dt;
                gvComments.DataBind();
            }
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvComments.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        protected void gvCommentRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label articleId = (Label)gvComments.Rows[e.RowIndex].FindControl("lblArticleID");
            Label commentId = (Label)gvComments.Rows[e.RowIndex].FindControl("lblCommentID");
            CommentService commentService = new CommentService();
            bool success = commentService.DeleteComment(Convert.ToInt32(articleId.Text), Convert.ToInt32(commentId.Text));
            if (success)
            {
                this.BindGrid();
            }

        }
    }
}