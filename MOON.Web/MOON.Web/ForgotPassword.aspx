<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="MOON.Web.ForgotPassword" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>MOON | Forgot Password</title>
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>

    <webopt:BundleReference runat="server" Path="~/Content/css" />
    <link href="~/img/Common/favicon.png" rel="shortcut icon" type="image/x-icon" />
    <link href="~/Resources/css/reset.css" rel="stylesheet" />
    <link href="~/Content/bootstrap.css" rel="stylesheet" />
    <link href="~/Content/all.min.css" rel="stylesheet" />
    <link href="~/Resources/css/common.css" rel="stylesheet" />
    <link href="~/Resources/css/style.css" rel="stylesheet" />
    <link href="~/Resources/css/darticles.css" rel="stylesheet" />
</head>
<body>
    <section class="signup-form">
        <div class="inner-container">
            <div class="main card border-0 rounded clearfix flex-row overflow-hidden">
                <div class="img-left">
                    <img src="./img/register/img_gradient.jpg" class="gradient" alt="Gradient Background">
                    <div class="logo-blk clearfix">
                        <img src="./img/common/favicon.png" class="logo-img" alt="Moon Logo">
                        <h1 class="logo-ttl"><a href="#">Moon</a></h1>
                    </div>
                    <!-- /.logo-blk -->
                    <div class="wb-blk">
                        <h3 class="wb-ttl mb-4">Hello Again.</h3>
                        <p>Enter the email address associated with your account, we'll send you a link to reset your password.</p>
                    </div>
                    <!-- /.wb-blk -->
                </div>
                <!-- /.img-left -->
                <form class="forgot-form" runat="server">
                    <asp:Label ID="lblSuccessMsg" runat="server" CssClass="text-success" Text=""></asp:Label>
                    <div class="form-group mb-3">
                        <label for='<%= txtEmail.ClientID %>'>Email</label>
                        <asp:TextBox ID="txtEmail" ValidationGroup="control" CssClass="form-control" runat="server" TextMode="Email"></asp:TextBox>
                        <asp:Label ID="lblErrorMsg" ValidationGroup="control" runat="server" ForeColor="Red" Text=""></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorForEmail" ControlToValidate="txtEmail" runat="server" ForeColor="Red" ErrorMessage="Email field is required"></asp:RequiredFieldValidator>
                    </div>
                    <asp:Button ID="btnSendMail" runat="server" ValidationGroup="control" OnClick="BtnSendResetMail" CssClass="btn btn-dark w-100 btn-signin" Text="Continue" />
                    <div class="signin-location text-center mt-4">
                        <a href='<%= ResolveUrl("~/Login.aspx") %>' class="text-decoration-none signin-btn">Back to login</a>
                    </div>
                </form>
                <!-- /.login-form -->
            </div>
        </div>
    </section>
    <!-- /.signup-form -->
</body>
</html>
