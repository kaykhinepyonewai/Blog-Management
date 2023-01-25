using MOON.Entities.User;
using MOON.Services.User;
using System;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;

namespace MOON.Web
{
    public partial class Register : System.Web.UI.Page
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

        protected void LnkBtnSignInClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }

        protected void btnSignUpClick(object sender, EventArgs e)
        {
            string email = txtEmail.Text.ToString();
            string password = txtPassword.Text.ToString();
            string confirmpassword = txtConfirmPassword.Text.ToString();
            UserService userService = new UserService();
            UserEntity userEntity = CreateData();
            int exist = userService.Exist(userEntity);
            DataTable checkemail = userService.GetSpecific(email, "Email");
            var formatemail = ValidateEmail(email);

            if (exist > 0)
            {
                lblCheckUserName.Text = "Username is already taken.";
            }
            else
            {
                if (checkemail.Rows.Count > 0)
                {
                    lblCheckEmail.Text = "Email is already taken.";
                }
                else
                {
                    if (!(formatemail.Success))
                    {
                        lblCheckEmail.Text = "Email format is not a valid email address.";
                    }
                    else
                    {
                        if (!(IsValidPassword(password)))
                        {
                            lblCheckPassword.Text = "Password must inlcude at least first character should be uppercase, 1 numeric, 1 special character, 1 alphabet and 8 in length.";
                        }
                        else
                        {
                            if (password == confirmpassword)
                            {
                                userService.Insert(userEntity);
                                Response.Redirect("~/Login.aspx");
                            }
                            else
                            {
                                lblErrorMsg.Text = "Confirm password and password should be the same.";
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// To Check an email is a valid email address or not.
        /// </summary>
        private Match ValidateEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var match = regex.Match(email);
            return match;
        }

        /// <summary>
        /// Giving role automatically. If there is no rows in database, it will give the first user to be admin.
        /// </summary>
        private int CheckRole()
        {
            UserService userService = new UserService();
            DataTable dt = userService.CountAll();
            int role = (dt.Rows.Count > 0) ? 2 : 1; 
            return role;
        }

        /// <summary>
        /// Turning user password into unreadable character and number
        /// </summary>
        public string HashPassword(string password)
        {
            SHA256 hash = SHA256.Create();
            var passwordbyte = Encoding.Default.GetBytes(password);
            var hashpassword = hash.ComputeHash(passwordbyte);
            var hexString = BitConverter.ToString(hashpassword);
            hexString = hexString.Replace("-", "");
            return hexString;
        }

        /// <summary>
        /// Create data to store into Database
        /// </summary>
        public UserEntity CreateData()
        {
            var currentTime = DateTime.Now;
            UserEntity userEntity = new UserEntity();
            userEntity.UserId = Convert.ToInt32(hdnLoginUserId.Value);
            userEntity.Username = Convert.ToString(txtUsername.Text.ToString());
            userEntity.Email = Convert.ToString(txtEmail.Text.ToString());
            userEntity.Password = HashPassword($"{Convert.ToString(txtPassword.Text.ToString())}{currentTime.ToString()}");
            userEntity.ConfirmPassword = HashPassword($"{Convert.ToString(txtConfirmPassword.Text.ToString())}{currentTime.ToString()}");
            userEntity.RoleId = CheckRole();
            userEntity.CreatedAt = currentTime;
            userEntity.UpdatedAt = currentTime;
            return userEntity;
        }

        static bool IsLetter(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }

        static bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        static bool IsSymbol(char c)
        {
            return c > 32 && c < 127 && !IsDigit(c) && !IsLetter(c);
        }

        static bool IsLength(string password)
        {
            return password.Length >= 8;
        }

        static bool IsUpperCase(string password)
        {
            return Char.IsUpper(password[0]);
        }

        /// <summary>
        /// To check the password,
        /// if the password length is greater than 8 or if the password includes at least 1 numeric, 1 special character and 1 letter 
        /// </summary>
        public bool IsValidPassword(string password)
        {
            return
               IsLength(password) &&
               IsUpperCase(password) &&
               password.Any(c => IsLetter(c)) &&
               password.Any(c => IsDigit(c)) &&
               password.Any(c => IsSymbol(c));
               
        }
    }
}