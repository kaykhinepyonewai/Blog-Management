using MOON.Entities.User;
using MOON.Services.User;
using System;
using System.Data;

namespace MOON.Web
{
    public partial class ResetPassword : System.Web.UI.Page

    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ResetPassword"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        protected void BtnResetPassword(object sender, EventArgs e)
        {
            string password = txtPassword.Text.ToString();
            string confirmpassword = txtConfirmPwd.Text.ToString();
            Register register = new Register();
            UserService userService = new UserService();
            UserEntity userEntity = CreateData();

            if (!(register.IsValidPassword(password)) || !(register.IsValidPassword(confirmpassword)))
            {
                lblCheckPassword.Text = "Password must be included at least 1 numeric, 1 special character, 1 alphabet and 8 in length.";
            }
            else
            {
                if (password == confirmpassword)
                {
                    userService.Update(userEntity);
                    Session.RemoveAll();
                    Session.Clear();
                    Session.Abandon();
                    Response.Redirect("~/Login.aspx");
                }
                else
                {
                    lblErrorMsg.Text = "Enter password and confirm password should be the same. ";
                }
            }
        }

        public UserEntity CreateData()
        {
            UserService userService = new UserService();
            Register register = new Register();
            DataTable dt = userService.GetSpecific(Session["ResetPassword"].ToString(), "Email");

            var currentTime = DateTime.Now;
            UserEntity userEntity = new UserEntity();
            userEntity.UserId = Convert.ToInt32(dt.Rows[0]["UserId"]);
            userEntity.Username = Convert.ToString(dt.Rows[0]["Username"]);
            userEntity.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
            userEntity.SecondName = Convert.ToString(dt.Rows[0]["SecondName"]);
            userEntity.Email = Convert.ToString(dt.Rows[0]["Email"]);
            userEntity.Address = Convert.ToString(dt.Rows[0]["Address"]);
            userEntity.Mobile = Convert.ToString(dt.Rows[0]["Mobile"]);
            userEntity.Gender = Convert.ToString(dt.Rows[0]["Gender"]);
            userEntity.Profile = Convert.ToString(dt.Rows[0]["Profile"]);
            userEntity.Password = register.HashPassword($"{Convert.ToString(txtPassword.Text.ToString())}{currentTime.ToString()}");
            userEntity.ConfirmPassword = register.HashPassword($"{Convert.ToString(txtConfirmPwd.Text.ToString())}{currentTime.ToString()}");
            userEntity.RoleId = Convert.ToInt32(dt.Rows[0]["RoleId"]);
            userEntity.CreatedAt = currentTime;
            userEntity.UpdatedAt = currentTime;
            return userEntity;
        }
    }

}