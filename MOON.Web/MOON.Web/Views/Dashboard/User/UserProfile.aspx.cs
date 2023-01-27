using MOON.Entities.User;
using MOON.Services.User;
using MOON.Services.Role;
using System;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;

namespace MOON.Web.Views.Dashboard.User
{
    public partial class UserProfile : System.Web.UI.Page
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
                    // Get current user 
                    DataTable checkuser = userService.GetSpecific(Request.QueryString["email"].ToString(), "Email"); 
                    // check user with querystring
                    if (Request.QueryString["email"] != null && Request.QueryString["email"].ToString() == dt.Rows[0]["Email"].ToString()
                        && Convert.ToInt32(dt.Rows[0]["RoleId"]) != 3 || Convert.ToInt32(dt.Rows[0]["RoleId"]) == 1) 
                    {  //To check if an admin can modify every user's information, if not, the user can only modify their own information
                        if (checkuser.Rows.Count > 0) 
                       // To check if a username at URI that neither exists in the database nor does not exist in the database
                        {
                            hdnUsername.Value = checkuser.Rows[0]["Username"].ToString();
                            BindData();
                            BindRoleList();
                            if (Convert.ToInt32(checkuser.Rows[0]["RoleId"]) == 1 && checkuser.Rows[0]["Email"].ToString() == "admin@gmail.com") 
                            { 
                                roleText.Style.Add("display", "inline-block"); 
                            } 
                            else { 
                                roleText.Style.Add("display", "none"); 
                            }
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

                }
            }
        }

        /// <summary>
        ///"To retrieve a list of all roles from the database and display them as a dropdown list.
        /// </summary>
        private void BindRoleList()
        {
            RoleService roleService = new RoleService();
            DataTable dt = roleService.GetAll();

            ddlRole.DataSource = dt;
            ddlRole.DataTextField = "Role";
            ddlRole.DataValueField = "RoleId";
            ddlRole.DataBind();
            ddlRole.Items.Insert(0, new ListItem("Role Options", "0"));
            if (ddlRole.SelectedIndex == 0)
            {
                ddlRole.Items[0].Attributes["disabled"] = "disabled";
            }
        }

        /// <summary>
        ///To display an individual user's information from the database.
        /// </summary>
        private void BindData()
        {
            UserService userService = new UserService();
            DataTable dt = userService.GetSpecific(Request.QueryString["email"].ToString(),"Email");
            if (dt.Rows.Count > 0)
            {
                txtUsername.Text = dt.Rows[0]["Username"].ToString();
                txtFirstName.Text = dt.Rows[0]["FirstName"].ToString();
                txtLastName.Text = dt.Rows[0]["SecondName"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
                ddlGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
                ddlRole.SelectedValue = dt.Rows[0]["RoleId"].ToString();
                imgBox.ImageUrl = dt.Rows[0]["profile"].ToString();
            }
            try
            {
                if (Convert.ToInt32(dt.Rows[0]["RoleId"].ToString()) == 1 && dt.Rows[0]["Email"].ToString() == "admin@gmail.com")
                {
                    ddlRole.Enabled = false;
                }
            }catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }

        /// <summary>
        /// btnUpdateProfile
        ///To Update user information and check validation.
        /// </summary>
        protected void btnUpdateProfile(object sender, EventArgs e)
        {
            UserService userService = new UserService();
            DataTable dt = userService.GetSpecific(Request.QueryString["email"].ToString(),"Email");

            //To send current user id, role, password and date to CreateData
            int id = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
            int role = Convert.ToInt32(dt.Rows[0]["RoleId"].ToString());
            string password = dt.Rows[0]["Password"].ToString();
            DateTime date = DateTime.Parse(dt.Rows[0]["CreatedAt"].ToString());

            string email = txtEmail.Text.ToString();
            string username = txtUsername.Text.ToString();
            string[] user = (string[])Session["Users"];
            int currentRole = Convert.ToInt32(user[1]);

            DataTable checkemail = userService.GetSpecific(email, "Email");
            DataTable checkusername = userService.GetSpecific(username,"Username");
            string extension = Path.GetExtension(fuldProfile.FileName);

            UserEntity userEntity = CreateData(id, password, date, currentRole);

            if (email == dt.Rows[0]["Email"].ToString() || !(checkemail.Rows.Count > 0))
            {/*During updating a user's information, to ensure that the email in the input box is
                * the same as the current user's email, no error will be generated.
                * But if you use another user's email that is already in use, 
                * an error will be generated.
                */
                if (username == dt.Rows[0]["Username"].ToString() || !(checkusername.Rows.Count > 0))
                {
                    //This validation is the same as the email validation on the top
                    if (fuldProfile.HasFiles)
                    {
                        if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
                        {
                            if (fuldProfile.PostedFile.ContentLength <= 102400)
                            {
                                bool success = userService.Update(userEntity);
                                if (success)
                                {
                                    if (currentRole == 1)
                                    {
                                        Response.Redirect("~/Views/Dashboard/Home.aspx");

                                    }
                                    else
                                    {
                                        Response.Redirect("~/Views/Dashboard/Article/ArticleList.aspx");
                                    }

                                }
                            }
                            else
                            {
                                lblImgError.Text = "(Imgae sizes has to be less than 100kb.)";
                            }

                        }
                        else
                        {
                            lblImgError.Text = "(Only .jpg, .jpeg, .png files are supported.)";
                        }
                    }
                    else
                    {
                        bool success = userService.Update(userEntity);
                        if (success)
                        {
                            if (currentRole == 1)
                            {
                                Response.Redirect("~/Views/Dashboard/Home.aspx");
                            }
                            else
                            {
                                Response.Redirect("~/Views/Dashboard/Article/ArticleList.aspx");
                            }
                        }
                    }
                }
                else
                {
                    lblCheckUserName.Text = "(Username is already existed.)";
                }
            }
            else
            {
                lblCheckEmail.Text = "(Email is already taken.)";
            }
        }  
       
        protected void btnCancelProfile (object sender, EventArgs e)
        {
            string[] user = (string[])Session["Users"];
            int currentRole = Convert.ToInt32(user[1]);
            if (currentRole == 1)
            {
                Response.Redirect("~/Views/Dashboard/User/UserList.aspx");
            }
            else
            {
                Response.Redirect("~/Views/Dashboard/Article/ArticleList.aspx");
            }
        }

        /// <summary>
        ///"To update an individual user's information.
        /// </summary>
        private UserEntity CreateData(int id, string password, DateTime date, int role)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.UserId = id;
            userEntity.RoleId = (role == 1) ? Convert.ToInt32(ddlRole.SelectedItem.Value) : 2;
            userEntity.Username = txtUsername.Text.ToString().ToLower();
            userEntity.FirstName = txtFirstName.Text.ToString();
            userEntity.SecondName = txtLastName.Text.ToString();
            userEntity.Email = txtEmail.Text.ToString();
            userEntity.Password = password;
            userEntity.ConfirmPassword = password;
            userEntity.Gender = Convert.ToString(ddlGender.SelectedItem.Value);
            userEntity.Address = txtAddress.Text.ToString();
            userEntity.Mobile = txtMobile.Text.ToString();
            userEntity.Profile = UpdatePhoto(fuldProfile);
            userEntity.CreatedAt = date;
            userEntity.UpdatedAt = DateTime.Now;
            return userEntity;
        }

        /// <summary>
        ///To update a profile photo, only update the photo if the user 
        ///uses the file upload feature to change the profile picture.
        ///If not, the current photo will be stored in the database.
        /// </summary>
        private String UpdatePhoto(FileUpload file)
        {
            UserService userService = new UserService();
            DataTable dt = userService.GetSpecific(Request.QueryString["email"].ToString(),"Email");
            if (file.HasFiles)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
                {

                    if (file.PostedFile.ContentLength <= 102400)
                    {
                        string checkpath = Server.MapPath(dt.Rows[0]["Profile"].ToString());
                        string filepath = Path.GetFullPath(checkpath);
                        if (File.Exists(filepath) && dt.Rows[0]["Profile"].ToString() != "~/img/Dashboard/Profile/Guest.png")
                        {
                         //only delete a photo file, if a profile file is not a Guest.png
                            File.Delete(filepath);
                        }
                        Random random = new Random();
                        int randomNumber = random.Next();
                        string newFileName = "img_" + Path.GetFileNameWithoutExtension(file.FileName.Replace("-", "").ToLower().Trim()) + "_" + randomNumber.ToString() + Path.GetExtension(file.FileName);
                        string filename = Path.GetFileName(newFileName);
                        //Rename a unique photo file name and save it into the database
                        string path = Server.MapPath("~/img/Dashboard/Profile/" + filename);
                        file.PostedFile.SaveAs(path);
                        string img = "~/img/Dashboard/Profile/" + filename;
                        return img;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                string img = dt.Rows[0]["Profile"].ToString();
                return img;
            }
        }
    }
}
