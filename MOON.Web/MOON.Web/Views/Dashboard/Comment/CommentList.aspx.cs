using MOON.Services.Comment;
using MOON.Services.Dashboard;
using MOON.Services.User;
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
            UserService userService = new UserService();

            string[] user = (string[])Session["Users"]; // get specific user info in the session array
            DataTable dt = userService.GetId(Convert.ToInt32(user[0]));
            int role = Convert.ToInt32(dt.Rows[0]["RoleId"].ToString());

            if (role == 1)
            {
                if (Request.QueryString["keyword"] != null)
                {
                    DataTable dt1 = commentService.GetAllCommentsBySearch(Request.QueryString["keyword"].ToString());
                    gvComments.DataSource = dt1;
                    gvComments.DataBind();
                }
                else
                {
                    DataTable dt2 = commentService.GetAllComments();
                    gvComments.DataSource = dt2;
                    gvComments.DataBind();
                }
            }
            else
            {
                if (Request.QueryString["keyword"] != null)
                {
                    DataTable dt3 = commentService.GetCommentsBySearch(Convert.ToInt32(Session["UserId"].ToString()), Request.QueryString["keyword"].ToString());
                    gvComments.DataSource = dt3;
                    gvComments.DataBind();
                }
                else
                {
                    DataTable dt4 = commentService.GetComments(Convert.ToInt32(Session["UserId"].ToString()));
                    gvComments.DataSource = dt4;
                    gvComments.DataBind();
                }
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

        protected void gvRowDeleteing(object sender, EventArgs e)
        {
            int commentId = Convert.ToInt32(hdnValueId.Value.ToString());
            int articleId = Convert.ToInt32(hdnArticleId.Value.ToString());
            CommentService commentService = new CommentService();
            bool success = commentService.DeleteComment(articleId,commentId);
            if (success)
            {
                this.BindGrid();
            }
        }
    }
}