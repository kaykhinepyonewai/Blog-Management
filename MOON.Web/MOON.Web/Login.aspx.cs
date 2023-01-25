using MOON.Services.User;
using System;
using System.Data;
using System.Linq;


namespace MOON.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Users"] != null)
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }

        protected void BtnSignInClick(object sender, EventArgs e)
        {
            UserService userService = new UserService();
            Register register = new Register();

            string email = txtEmail.Text.ToString();
            DataTable dt = userService.GetSpecific(email, "Email");
            
            try
            {
                int id = Convert.ToInt32(dt.Rows[0]["UserId"]);
                int roleId = Convert.ToInt32(dt.Rows[0]["RoleId"]);
                Session["RoleId"] = roleId;
                Session["UserId"] = id;
                var salt = dt.Rows[0]["CreatedAt"].ToString();
                string password = dt.Rows[0]["Password"].ToString();
                string currentpassowrd = register.HashPassword($"{txtPassword.Text.ToString()}{salt}"); //Verify hash password

                if (currentpassowrd == password)
                {
                    var userinfo = dt.Rows[0].ItemArray.Select(x => x.ToString()).ToArray(); //turn dataTable into array list
                    Session["Users"] = userinfo; // To save all user info in the session as array
                    
                    string[] user = (string[])Session["Users"]; // get specific user info in the session array
                    if (Convert.ToInt32(user[1]) == 3) // To check if a user roleId is banned or not
                    {
                        lblErrorMsg.Text = "Your account is prohibited at this time!";
                        Session.RemoveAll();
                        Session.Clear();
                        Session.Abandon();
                    }
                    else
                    {
                        Response.Redirect("~/Views/Dashboard/Home.aspx");
                    }
                }
                else
                {
                    lblErrorMsg.Text = "Email or password is incorrect!.";
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "Email or password is incorrect!.";
            }
        }

        protected void lnkBtnRegisterClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Register.aspx");
        }
    }
}