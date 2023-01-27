using MOON.Entities.Dashboard;
using MOON.Entities.User;
using MOON.Services.Comment;
using MOON.Services.Dashboard;
using MOON.Services.Like;
using MOON.Services.Role;
using MOON.Services.User;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;

namespace MOON.Web.Views.Dashboard.User
{
    public partial class UserList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session.Count != 0 && Session["Users"] != null)
                {
                    UserService userService = new UserService();
                    string[] user = (string[])Session["Users"];
                    DataTable dt = userService.GetId(Convert.ToInt32(user[0]));
                    try
                    {
                        if (Convert.ToInt32(dt.Rows[0]["RoleId"]) == 1)
                        {//To check if the current user is not an admin, it will directly send you to the previous page.
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

        protected void LnkBtnExport(object sender, EventArgs e)
        {
            UserService userService = new UserService();
            DataTable dt = userService.GetExports();
            var filepath = Server.MapPath("~/Files/") + "users.xlsx";
            ExportExcel(filepath, dt);
            Response.ContentType = "Application/x-msexcel";
            Response.AppendHeader("Content-Disposition", "attachment; filename=users.xlsx");
            Response.TransmitFile(filepath);
            Response.Flush();
            Response.End();
        }

        private void ExportExcel(string filepath, DataTable dt)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                excelPackage.Workbook.Worksheets.Add("User").Cells[1, 1].LoadFromDataTable(dt, true);
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                    excelPackage.SaveAs(new FileInfo(filepath));
                }
                excelPackage.SaveAs(new FileInfo(filepath));
            }
        }

        protected void LnkImportBtn(object sender, EventArgs e)
        {
            string FilePath = ConfigurationManager.AppSettings["FilePath"].ToString();
            string filename = string.Empty;

            filename = Path.GetFileName(Server.MapPath(fuldImport.FileName));
            fuldImport.SaveAs(Server.MapPath(FilePath) + filename);
            string filepath = Server.MapPath(FilePath) + filename;
            ImportExcel(filepath);
        }

        private void ImportExcel(string filepath)
        {
            FileInfo fileInfo = new FileInfo(filepath);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets[0];
                int rowno = excelWorksheet.Dimension.End.Row;
                UserEntity userEntity = new UserEntity();
                UserService userService = new UserService();
                RoleService roleService = new RoleService();
                Register register = new Register();
                Random random = new Random();
                var currenttime = DateTime.Now;
                DataTable dt;
                DataTable role;

                for (int i = 2; i <= rowno; i++)
                {
                    dt = userService.GetSpecific(Convert.ToString(excelWorksheet.Cells[i, 1].Value), "Username");
                    if (dt.Rows.Count > 0)
                    {
                        userEntity.Username = Convert.ToString(excelWorksheet.Cells[i, 1].Value) + "_" + random.Next();
                    }
                    else
                    {
                        userEntity.Username = Convert.ToString(excelWorksheet.Cells[i, 1].Value);
                    }
                    userEntity.Email = Convert.ToString(excelWorksheet.Cells[i, 2].Value);
                    userEntity.Address = (Convert.ToString(excelWorksheet.Cells[i, 3].Value) == string.Empty) ? null : Convert.ToString(excelWorksheet.Cells[i, 3].Value);
                    userEntity.Mobile = (Convert.ToString(excelWorksheet.Cells[i, 4].Value) == string.Empty) ? null : Convert.ToString(excelWorksheet.Cells[i, 4].Value);
                    userEntity.Gender = (Convert.ToString(excelWorksheet.Cells[i, 5].Value) == string.Empty) ? null : Convert.ToString(excelWorksheet.Cells[i, 5].Value);
                    role = roleService.GetSpecific("Role", Convert.ToString(excelWorksheet.Cells[i, 6].Value));
                    try
                    {
                        userEntity.RoleId = Convert.ToInt32(role.Rows[0]["RoleId"].ToString());
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message.ToString();
                    }
                    userEntity.Password = register.HashPassword($"{Convert.ToString(excelWorksheet.Cells[i, 7].Value)}{currenttime.ToString()}");
                    userEntity.ConfirmPassword = register.HashPassword($"{Convert.ToString(excelWorksheet.Cells[i, 7].Value)}{currenttime.ToString()}");
                    userEntity.CreatedAt = DateTime.Now;
                    userEntity.UpdatedAt = DateTime.Now;
                    userService.InsertImport(userEntity);
                }
                BindGrid();
            }
        }

        private void BindGrid()
        {
            UserService userService = new UserService();
            if (Request.QueryString["keyword"] != null)
            {
                DataTable dt = userService.GetSearchUsers(Request.QueryString["keyword"].ToString());
                gvUsers.DataSource = dt;
                gvUsers.DataBind();
            }
            else
            {
                DataTable dt = userService.GetUsers();
                gvUsers.DataSource = dt;
                gvUsers.DataBind();
            }
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsers.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        protected void gvUserRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                Response.Redirect("~/Views/Dashboard/User/UserProfile.aspx?email=" + e.CommandArgument);
            }
        }

        protected void gvRowDeleteing(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hdnValueId.Value.ToString());
            UserService userService = new UserService();
            ArticleService articleService = new ArticleService();
            PhotoService photoService = new PhotoService();
            CommentService commentService = new CommentService();
            LikeService likeService = new LikeService();

            DataTable articledt = articleService.GetArticleByUser(id);
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

            DataTable dt = userService.GetId(id);
            string userimg = dt.Rows[0]["Profile"].ToString();
            string checkpath = Server.MapPath(userimg);
            string filepath = Path.GetFullPath(checkpath);
            if (userimg != null && userimg != "~/img/Dashboard/Profile/Guest.png")
            /*Once you deleted a user from the user lists, it will delete current user profile photo in the project img folder. */
            {
                try
                {
                    File.Delete(filepath);
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                }
            }
            bool success = userService.Delete(id);
            if (success)
            {
                BindGrid();
            }
        }
    }
}