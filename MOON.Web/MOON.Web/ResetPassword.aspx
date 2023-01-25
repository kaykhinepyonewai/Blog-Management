<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="MOON.Web.ResetPassword" %>

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
                        <h3 class="wb-ttl mb-4">Create New Password.</h3>
                        <p>Your new password must be different from previous used passwords.</p>
                    </div>
                    <!-- /.wb-blk -->
                </div>
                <!-- /.img-left -->
                <form class="forgot-form" runat="server">
                    <div class="form-group mb-3">
                        <label for="<%= txtPassword.ClientID %>">Password</label>
                        <br>
                        <asp:Label ID="lblCheckPassword" CssClass="form-label" runat="server" ForeColor="Red" Text=""></asp:Label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="signup" ID="RequiredFieldValidatorForPassword" ControlToValidate="txtPassword" runat="server" ForeColor="Red" ErrorMessage="Password field required."></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group mb-3">
                        <label for="<%= txtConfirmPwd.ClientID %>">Confirm Password</label>
                        <asp:TextBox ID="txtConfirmPwd" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="signup" ID="RequiredFieldValidatorForConfirmPassword" ControlToValidate="txtConfirmPwd" runat="server" ForeColor="Red" ErrorMessage="Password field required."></asp:RequiredFieldValidator>
                    </div>
                    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
                    <asp:Button ID="btnResetPassword" runat="server" OnClick="BtnResetPassword" CssClass="btn btn-dark w-100 btn-signin" Text="Reset Password" />
                </form>
                <!-- /.login-form -->
            </div>
        </div>
    </section>
    <!-- /.signup-form -->
</body>
</html>
