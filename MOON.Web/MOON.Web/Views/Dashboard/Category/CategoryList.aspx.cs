using MOON.Services.Dashboard;
using MOON.Services.User;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace MOON.Web.Views.Dashboard.Category
{
    public partial class CategoryList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session.Count != 0)
                {
                    UserService userService = new UserService();
                    string[] user = (string[])Session["Users"];
                    DataTable dt = userService.GetId(Convert.ToInt32(user[0]));
                    try
                    {
                        if (Convert.ToInt32(dt.Rows[0]["RoleId"]) == 1)
                        {
                            BindGrid();
                        }
                        else
                        {
                            Response.Write("<script>history.go(-1)</script>");
                        }
                    }
                    catch (Exception)
                    {
                        Response.Write("<script>history.go(-1)</script>");
                    }
                }
            }
        }

        private void BindGrid()
        {
            CategoryService categoryService = new CategoryService();
            if(Request.QueryString["keyword"] != null)
            {
                DataTable dt = categoryService.GetAllBySearch(Request.QueryString["keyword"].ToString());
                gvCategories.DataSource = dt;
                gvCategories.DataBind();
            }
            else
            {
                DataTable dt = categoryService.GetAll();
                gvCategories.DataSource = dt;
                gvCategories.DataBind();
            }
         }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCategories.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }


        protected void gvCategoryRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                Response.Redirect("~/Views/Dashboard/Category/CategoryCreate.aspx?id=" + e.CommandArgument);
            }
        }

        protected void gvCategoryRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label getId = (Label)gvCategories.Rows[e.RowIndex].FindControl("lblCategoryId");
            CategoryService categoryService = new CategoryService();
            bool success = categoryService.Delete(Convert.ToInt32(getId.Text));
            if (success)
            {
                BindGrid();
            }
        }

    }
}