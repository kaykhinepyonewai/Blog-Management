<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="MOON.Web.Register" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>MOON | Register</title>
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>

    <webopt:BundleReference runat="server" Path="~/Content/css" />
    <link href="~/img/Common/favicon.png" rel="shortcut icon" type="image/x-icon" />
    <link href="~/Resources/css/reset.css" rel="stylesheet" />
    <link href="~/Content/bootstrap.css" rel="stylesheet" />
    <link href="~/Content/all.min.css" rel="stylesheet" />
    <link href="~/Resources/css/style.css" rel="stylesheet" />
    <link href="~/Resources/css/darticles.css" rel="stylesheet" />

</head>
<body>
    <section class="signup-form">
        <div class="inner-container ">
            <div class="main card border-0 rounded clearfix flex-row overflow-hidden">
                <div class="img-left">
                    <img src="../img/Register/img_gradient.jpg" class="gradient" alt="Gradient Background">
                    <div class="logo-blk clearfix">
                        <img src="../img/Common/favicon.png" class="logo-img" alt="Moon Logo">
                        <h1 class="logo-ttl"><a href="<%= ResolveUrl("~/Default.aspx") %>">Moon</a></h1>
                    </div>
                    <!-- /.logo-blk -->
                    <div class="wb-blk">
                        <h3 class="wb-ttl mb-4">Welcome Back.</h3>
                        <p>Enter your personal details and start journey with us.</p>
                    </div>
                    <!-- /.wb-blk -->
                </div>
                <!-- /.img-left -->
                <form class="register-form" id="form1" runat="server">
                    <asp:HiddenField ID="hdnLoginUserId" Value="0" runat="server" />
                    <div class="form-group mb-3">
                        <asp:Label ID="lblUsername" CssClass="form-label" runat="server" >Username <span class="text-danger">*</span></asp:Label>
                        <br />
                        <asp:Label ID="lblCheckUserName" CssClass="form-label" runat="server" ForeColor="Red" Text=""></asp:Label>
                        <asp:TextBox ValidationGroup="signup" CssClass="form-control" ID="txtUsername" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="signup" ID="RequiredFieldValidatorUsername" ControlToValidate="txtUsername" runat="server" ForeColor="Red" ErrorMessage="Username field required."></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group mb-3">
                        <asp:Label ID="lblEmail" CssClass="form-label" runat="server" >Email <span class="text-danger">*</span></asp:Label>
                        <br />
                        <asp:Label ID="lblCheckEmail" CssClass="form-label" runat="server" ForeColor="Red" Text=""></asp:Label>
                        <asp:TextBox ValidationGroup="signup" CssClass="form-control" ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="signup" ID="RequiredFieldValidatorForEmail" ControlToValidate="txtEmail" runat="server" ForeColor="Red" ErrorMessage="Email field required."></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group mb-3">
                        <asp:Label ID="lblPassword" CssClass="form-label" runat="server" >Password <span class="text-danger">*</span></asp:Label>
                        <br />
                        <asp:Label ID="lblCheckPassword" CssClass="form-label" runat="server" ForeColor="Red" Text=""></asp:Label>
                        <asp:TextBox ValidationGroup="signup" CssClass="form-control" ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="signup" ID="RequiredFieldValidatorForPassword" ControlToValidate="txtPassword" runat="server" ForeColor="Red" ErrorMessage="Password field required."></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group mb-3">
                        <asp:Label ID="lblConfirmPassword" CssClass="form-label" runat="server" >Confirm Password <span class="text-danger">*</span></asp:Label>
                        <asp:TextBox ValidationGroup="signup" CssClass="form-control" ID="txtConfirmPassword" TextMode="Password" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="signup" ID="RequiredFieldValidatorConfirmPassword" ControlToValidate="txtConfirmPassword" runat="server" ForeColor="Red" ErrorMessage="Confirm Password field required."></asp:RequiredFieldValidator>
                    </div>
                    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
                    <asp:Button ID="btnSignUp" ValidationGroup="signup" CssClass="btn btn-dark mt-3 w-100 signup-btn" runat="server" OnClick="btnSignUpClick" Text="Sign Up" />
                    <div class="signin-location text-center mt-4">
                        <p>
                            Already have an account? 
                        <span>
                            <asp:LinkButton ID="lnkBtnSignIn" CssClass="text-decoration-none fw-bold signin-btn" OnClick="LnkBtnSignInClick" runat="server">Sign in</asp:LinkButton>
                        </span>
                        </p>
                    </div>
                    <!-- /.signin-location -->
                </form>
                <!-- /.register-form -->
            </div>
        </div>
    </section>
    <!-- /.signup-form -->
        <script src='<%= ResolveUrl("~/Scripts/Library/jquery.min.js") %>'></script>
        <script src='<%= ResolveUrl("~/Resources/js/common.js") %>'></script>
</body>
</html>

