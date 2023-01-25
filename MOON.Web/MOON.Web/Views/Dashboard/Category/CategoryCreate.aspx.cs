using MOON.Entities.Dashboard;
using MOON.Services.Dashboard;
using System;
using System.Data;
using System.Text.RegularExpressions;


namespace MOON.Web.Views.Dashboard.Category
{
    public partial class CategoryCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["id"] != null)
                {
                    hdCategoryId.Value = Request.QueryString["id"].ToString();
                    BindData();
                    btnSave.Text = "Update";

                }
                btnSave.Enabled = true;
            }
        }

        void BindData()
        {
            CategoryService categoryService = new CategoryService();
            DataTable dt = categoryService.Get(Convert.ToInt32(hdCategoryId.Value));
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = dt.Rows[0]["Name"].ToString();
                txtSlug.Text = dt.Rows[0]["Slug"].ToString();
            }
        }
        protected void btnSaveClick(object sender, EventArgs e)
        {
            AddorUpdate();
        }


        void AddorUpdate()
        {
            CategoryService categoryService = new CategoryService();
            CategoryEntity categoryEntity = CreateData();
            bool success = false;
            if (hdCategoryId.Value == "0")
            {
                int countName = 0;
                countName = categoryService.CountName(categoryEntity);
                int countSlug = 0;
                countSlug = categoryService.CountSlug(categoryEntity);
                if (countName > 0)
                {/*During inserting a category, to ensure that the name in the input box is
                     * the same as another category's name that is already in use, 
                     * an error will be generated.
                      */
                    btnSave.Enabled = false;
                    lblTitle.Text = "Input field name is already existed.";
                    lblTitle.Visible = true;
                }
                else if (countSlug > 0)
                {/*During inserting a category, to ensure that the slug in the input box is
                     * the same as another category's slug that is already in use, 
                     * an error will be generated.
                      */
                    btnSave.Enabled = false;
                    lblSlug.Text = "Input field slug is already existed.";
                    lblSlug.Visible = true;
                }
                else
                {
                    success = categoryService.Insert(categoryEntity);
                    btnSave.Enabled = true;

                }

                btnSave.Enabled = true;
                txtTitle.Text = "";
                txtSlug.Text = "";
            }

            else
            {
                int countTitle = 0;
                int countSlug = 0;
                countTitle = categoryService.CountTitleUpdate(categoryEntity);
                countSlug = categoryService.CountSlugUpdate(categoryEntity);
                if (countTitle > 0)
                {/*During updating a category, to ensure that the title in the input box is
                     * the same as the current category's title, no error will be generated.
                     * But if you use another category's title that is already in use, 
                     * an error will be generated.
                      */
                    btnSave.Enabled = false;
                    lblTitle.Text = "Please Update Name, input field name is already existed.";
                    lblTitle.Visible = true;
                }
                else if (countSlug > 0)
                {/*During updating a category, to ensure that the slug in the input box is
                     * the same as the current category's slug, no error will be generated.
                     * But if you use another category's slug that is already in use, 
                     * an error will be generated.
                      */
                    btnSave.Enabled = false;
                    lblSlug.Text = "Please Update Slug, input field slug is already existed.";
                    lblSlug.Visible = true;
                }
                else
                {
                    success = categoryService.Update(categoryEntity);
                }

                btnSave.Enabled = true;
            }

            if (success)
            {
                Response.Redirect("~/Views/Dashboard/Category/CategoryList.aspx");

            }
        }

        public string GetSlug(string text)
        {
            string slug = text.ToLower();
            slug = Regex.Replace(slug, @"[^A-Za-z0-9\s-]", "");
            slug = Regex.Replace(slug, @"\s+", " ").Trim();
            slug = Regex.Replace(slug, @"\s", "-");
            return slug;
        }

        public CategoryEntity CreateData()
        {
            CategoryEntity categoryEntity = new CategoryEntity();
            categoryEntity.CategoryId = Convert.ToInt32(hdCategoryId.Value);
            categoryEntity.Name = txtTitle.Text;
            categoryEntity.Slug = GetSlug(txtSlug.Text);
            return categoryEntity;
        }


        protected void btnCancelClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Dashboard/Category/CategoryList.aspx");
        }
    }
}