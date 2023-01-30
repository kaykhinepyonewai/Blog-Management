using MOON.Entities.Dashboard;
using MOON.Services.Dashboard;
using MOON.Services.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace MOON.Web.Views.Dashboard
{
    public partial class ArticleCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(Session.Count != 0 && Session["Users"] != null)
                {
                    if (Request.QueryString["id"] != null)
                    {
                        
                        UserService userService = new UserService();
                        ArticleService articleService = new ArticleService();
                        string[] user = (string[])Session["Users"];
                        DataTable dt = userService.GetId(Convert.ToInt32(user[0]));
                        // Get current user id
                        if (Request.QueryString["id"].ToString() != string.Empty)
                        {
                            DataTable checkarticle = articleService.CheckArticleWithUser(Convert.ToInt32(Request.QueryString["id"].ToString()), Convert.ToInt32(dt.Rows[0]["UserId"].ToString()));
                            // check articleId from URI and current userId is existed in the article database.
                            DataTable exist = articleService.CheckArticle(Convert.ToInt32(Request.QueryString["id"].ToString()), "ArticleId");
                            // get article id from query string and check it is existed in the article database.
                            if (Request.QueryString["id"] != null && checkarticle.Rows.Count > 0 && Request.QueryString["id"].ToString() == checkarticle.Rows[0]["ArticleId"].ToString()
                                && Convert.ToInt32(dt.Rows[0]["RoleId"]) != 3 || Convert.ToInt32(dt.Rows[0]["RoleId"]) == 1)
                            {  //To check if an admin can modify every user's information, if not, the user can only modify their own information
                                if (exist.Rows.Count > 0)
                                // To check if a article id at URI that neither exists in the database nor does not exist in the database
                                {
                                    hdArticleId.Value = Request.QueryString["id"].ToString();
                                    BindData();
                                    GTImage();
                                    imgBox.Visible = true;
                                    btnSave.Text = "Update";
                                    btnSave.Enabled = true;
                                }
                                else
                                {
                                    Response.Write("<script>history.go(-1)</script>");
                                }
                            }
                            else
                            {
                                Response.Write("<script>history.go(-1)</script>");
                            }
                        }else
                        {
                            Response.Write("<script>history.go(-1)</script>");
                        }
                    }
                    BindCategory();
                }
            }
        }

        void BindData()
        {
            ArticleService articleService = new ArticleService();
            DataTable dt = articleService.Get(Convert.ToInt32(hdArticleId.Value));
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = dt.Rows[0]["Title"].ToString();
                txtSlug.Text = dt.Rows[0]["Slug"].ToString();
                ddlCategoryName.SelectedValue = dt.Rows[0]["CategoryId"].ToString();
                txtDescription.Text = WebUtility.HtmlDecode(dt.Rows[0]["Description"].ToString());
                txtExcerpt.Text = WebUtility.HtmlDecode(dt.Rows[0]["Excerpt"].ToString());
                imgBox.ImageUrl = dt.Rows[0]["Thumbnail"].ToString();
            }
        }

        void BindCategory()
        {
            ArticleService articleService = new ArticleService();
            DataTable dt = articleService.GetCategory();
            ddlCategoryName.DataSource = dt;
            ddlCategoryName.DataValueField = "CategoryId";
            ddlCategoryName.DataTextField = "Name";
            ddlCategoryName.DataBind();
            ddlCategoryName.Items.Insert(0, new ListItem("Select your category.", "0"));
            if (ddlCategoryName.SelectedIndex == 0)
            {
                ddlCategoryName.Items[0].Attributes["disabled"] = "disabled";
            }
        }

        private string GetExcerpt(string text)
        {
            int maxlenght = 150;
            if (text.Length < maxlenght)
            {
                return text;
            }
            else
            {
                var characterLength = 0;
                var words = text.Split(' ');
                var box = new List<string>();
                foreach (var word in words)
                {
                    box.Add(word);
                    characterLength += word.Length + 1;
                    if (characterLength > maxlenght)
                    {
                        break;
                    }

                }
                return string.Join(" ", box) + "...";
            }
        }

        protected void btnSaveClick(object sender, EventArgs e)
        {
            AddorUpdate();
        }

        protected void btnCancelClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Dashboard/Article/ArticleList.aspx");
        }

        protected void btnDeleteClick(object sender, EventArgs e)
        {
            string val = txtHidden.Value;
            PhotoService photoService = new PhotoService();
            bool success = photoService.Delete(Convert.ToInt32(val));

            if (success)
            {
                GTImage();
            }
        }

        public List<PhotoEntity> GTImage()
        {
            PhotoService photoService = new PhotoService();
            List<PhotoEntity> cc = photoService.GetAllImage(Convert.ToInt32(hdArticleId.Value));
            return cc;
        }

        void AddorUpdate()
        {
            ArticleService articleService = new ArticleService();
            ArticleEntity articleEntity = CreateData();
            bool success = false;
            if (hdArticleId.Value == "0")
            {
                int countSlug = 0;
                int countTitle = 0;
                int countThumbnail = 0;
                countSlug = articleService.CountSlug(articleEntity);
                countTitle = articleService.CountTitle(articleEntity);
                countThumbnail = articleService.CountThumbnail(articleEntity);
                if (countTitle > 0)
                {/*During inserting an article, to ensure that the title in the input box is
                     * the same as another article's title that is already in use, 
                     * an error will be generated.
                      */
                    btnSave.Enabled = false;

                    lblTitle.Text = "(Title is already existed.)";
                    lblTitle.Visible = true;
                }
                else if (countSlug > 0)
                {/*During inserting an article, to ensure that the slug in the input box is
                     * the same as another article's slug that is already in use, 
                     * an error will be generated.
                      */
                    btnSave.Enabled = false;
                    lblSlug.Text = "(Slug is already existed.)";
                    lblSlug.Visible = true;
                }
                else if (countThumbnail > 0)
                {
                    btnSave.Enabled = false;
                    lblThubmail.Text = "(Thumbnail is already existed.)";
                    lblThubmail.Visible = true;
                }
                else if (FileUploadPhoto.PostedFile.ContentLength >= 1024000000)
                {
                    try
                    {
                        lblPhoto.Visible = true;
                        btnSave.Enabled = false;
                        lblPhoto.Text = "(Imgae sizes has to be less than 128mb)";
                    }
                    catch (Exception ex)
                    {
                        lblPhoto.Text = ex.Message.ToString();
                    }
                }

                else
                {
                    btnSave.Enabled = true;
                    if (imgBox.ImageUrl != string.Empty)
                    {
                        success = articleService.Insert(articleEntity);
                    }
                    else
                    {
                        lblThubmail.Text = "(Thumbnail is Required.)";
                        lblThubmail.Visible = true;
                    }
                    string str1 = FileUploadPhoto.FileName;
                    PhotoEntity photoEntity = CreateImage();
                }
                btnSave.Enabled = true;
            }
            else
            {
                int countUpdateSlug = 0;
                int countUpdateTitle = 0;
                int countUpdateThumbnail = 0;
                countUpdateSlug = articleService.CountUpdateSlug(articleEntity);
                countUpdateTitle = articleService.CountUpdateTitle(articleEntity);
                countUpdateThumbnail = articleService.CountUpdateThumbnail(articleEntity);
                if (countUpdateTitle > 0)
                {
                    /*During updating an article, to ensure that the title in the input box is
                     * the same as the current article's title, no error will be generated.
                     * But if you use another article's title that is already in use, 
                     * an error will be generated.
                      */
                    btnSave.Enabled = false;
                    lblTitle.Text = "(Title is already existed.)";
                    lblTitle.Visible = true;
                }
                else if (countUpdateSlug > 0)
                {/*During updating an article, to ensure that the slug in the input box is
                     * the same as the current article's slug, no error will be generated.
                     * But if you use another article's title that is already in use, 
                     * an error will be generated.
                      */
                    btnSave.Enabled = false;
                    lblSlug.Text = "(Slug is already existed.)";
                    lblSlug.Visible = true;
                }
                else if (countUpdateThumbnail > 0)
                {
                    btnSave.Enabled = false;
                    lblThubmail.Text = "(Thumbnail is already existed.)";
                    lblThubmail.Visible = true;
                }
                else if (FileUploadPhoto.PostedFile.ContentLength >= 1024000000)
                {
                    try
                    {
                        lblPhoto.Visible = true;
                        btnSave.Enabled = false;
                        lblPhoto.Text = "(Imgae sizes has to be less than 128mb.)";
                    }catch (Exception ex)
                    {
                        lblPhoto.Text = ex.Message.ToString();
                    }
                }
                else
                {
                    btnSave.Enabled = true;
                    success = articleService.Update(articleEntity);
                    PhotoEntity photoEntity = CreateImage();
                }
                btnSave.Enabled = true;
            }

            if (success)
            {
                int roleId = Convert.ToInt32(Session["RoleId"]);
                if (roleId == 1)
                {
                    Response.Redirect("~/Views/Dashboard/Article/ArticleList.aspx");
                }
                else
                {
                    Response.Redirect("~/Views/Dashboard/Article/WaitingList.aspx");
                   
                }
            }
        }

        public PhotoEntity CreateImage()
        {
            PhotoService photoService = new PhotoService();
            ArticleService articleService = new ArticleService();
            PhotoEntity photoEntity = new PhotoEntity();
            ArticleEntity articleEntity1 = CreateSlug();
            bool success = false;

            if (FileUploadPhoto.HasFile)
            {
                string extension = Path.GetExtension(FileUploadPhoto.FileName);
                if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")

                {
                        foreach (HttpPostedFile fu in FileUploadPhoto.PostedFiles)
                        {
                            Random random = new Random();
                            int randomNumber = random.Next();
                            string newFileName = "img_" + Path.GetFileNameWithoutExtension(fu.FileName.Replace("-", "").ToLower().Trim()) + "_" + randomNumber.ToString() + Path.GetExtension(fu.FileName);
                            string filename = Path.GetFileName(newFileName);
                            string path = "../../../Photos/" + filename;
                            fu.SaveAs((Server.MapPath("../../../Photos/" + filename)));
                            int ArticleId = articleService.GetId(articleEntity1);
                            photoEntity.PhotoImage = path;
                            photoEntity.ArticleId = ArticleId;
                            photoEntity.DeleteStatus = 0;
                            success = photoService.InsertImage(photoEntity);
                        }
                }

                else
                {
                    lblPhoto.Visible = true;
                    lblPhoto.Text = "(Only .jpg, .jpeg, .png files are supported.)";
                }
            }
            else
            {

            }
            return photoEntity;
        }

        public string GetSlug(string text)
        {
            string slug = text.ToLower();
            slug = Regex.Replace(slug, @"[^A-Za-z0-9\s-]", "");
            slug = Regex.Replace(slug, @"\s+", " ").Trim();
            slug = Regex.Replace(slug, @"\s", "-");
            return slug;
        }

        public ArticleEntity CreateSlug()
        {
            ArticleEntity articleEntity = new ArticleEntity();
            articleEntity.Slug = GetSlug(txtSlug.Text);

            return articleEntity;
        }

        public ArticleEntity CreateData()
        {
            ArticleEntity articleEntity = new ArticleEntity();
            ArticleService articleservice = new ArticleService();
            if (FileUploadThum.HasFile)
            {
                string extension = Path.GetExtension(FileUploadThum.FileName);
                if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
                {

                    Random random = new Random();
                    int randomNumber = random.Next();
                    string newFileName = "img_" + Path.GetFileNameWithoutExtension(FileUploadThum.FileName.Replace("-", "").ToLower().Trim()) + "_" + randomNumber.ToString() + Path.GetExtension(FileUploadThum.FileName);
                    string filename = Path.GetFileName(newFileName);

                    try
                    {
                        if (Request.QueryString["id"] != null)
                        {
                            DataTable dt = articleservice.CheckArticle(Convert.ToInt32(Request.QueryString["id"].ToString()),"ArticleId");
                            string checkpath = Server.MapPath(dt.Rows[0]["Thumbnail"].ToString());
                            string filepath = Path.GetFullPath(checkpath);
                            if (File.Exists(filepath))
                            {
                                //only delete a photo file, if a profile file is not a Guest.png
                                File.Delete(filepath);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message;
                    }
                    

                    FileUploadThum.PostedFile.SaveAs(Server.MapPath("../../../Upload/" + filename));
                    string Image = "../../../Upload/" + filename.ToString();
                    imgBox.ImageUrl = Image;
                    string str1 = FileUploadPhoto.FileName;
                    int uu = Convert.ToInt32(Session["UserId"]);
                    int roleId = Convert.ToInt32(Session["RoleId"]);
                    articleEntity.ArticleId = Convert.ToInt32(hdArticleId.Value);
                    articleEntity.Title = txtTitle.Text;
                    articleEntity.UserId = uu;
                    articleEntity.CategroyId = Convert.ToInt32(ddlCategoryName.SelectedItem.Value);
                    articleEntity.Slug = GetSlug(txtSlug.Text);
                    articleEntity.Description = WebUtility.HtmlEncode(txtDescription.Text.ToString());
                    articleEntity.Excerpt = GetExcerpt(WebUtility.HtmlEncode(txtExcerpt.Text.ToString()));
                    articleEntity.Thumbnail = Image;

                    if (roleId == 1)
                    {
                        articleEntity.Status = "active";
                    }
                    else
                    {
                        articleEntity.Status = "pending";
                    }

                    articleEntity.DeleteStatus = 0;

                }

                else
                {
                    lblThubmail.Visible = true;
                    lblThubmail.Text = "(Only .jpg, .jpeg, .png files are supported.)";
                }
            }

            else
            {
                string str1 = FileUploadPhoto.FileName;
                int uu = Convert.ToInt32(Session["UserId"]);
                int roleId = Convert.ToInt32(Session["RoleId"]);
                articleEntity.ArticleId = Convert.ToInt32(hdArticleId.Value);
                articleEntity.Title = txtTitle.Text;
                articleEntity.UserId = uu;
                articleEntity.CategroyId = Convert.ToInt32(ddlCategoryName.SelectedItem.Value);
                articleEntity.Slug = GetSlug(txtSlug.Text);
                articleEntity.Description = WebUtility.HtmlEncode(txtDescription.Text.ToString());
                articleEntity.Excerpt = GetExcerpt(WebUtility.HtmlEncode(txtExcerpt.Text.ToString()));
                articleEntity.Thumbnail = imgBox.ImageUrl;
                if (roleId == 1)
                {
                    articleEntity.Status = "active";
                }
                else
                {
                    articleEntity.Status = "pending";
                }
                articleEntity.DeleteStatus = 0;
            }

            return articleEntity;
        }
    }
}