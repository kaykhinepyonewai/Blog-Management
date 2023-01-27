<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MOON.Web.Login" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>MOON | Login</title>
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
        <div class="inner-container">
            <div class="main card border-0 rounded clearfix flex-row overflow-hidden">
                <div class="img-left">
                    <img src="../img/Register/img_gradient.jpg" class="gradient" alt="Gradient Background">
                    <div class="logo-blk clearfix">
                        <img src="../img/Common/favicon.png" class="logo-img" alt="Moon Logo">
                        <h1 class="logo-ttl"><a href="<%= ResolveUrl("~/Default.aspx") %>">Moon</a></h1>
                    </div>
                    <!-- /.logo-blk -->
                    <div class="wb-blk">
                        <h3 class="wb-ttl mb-4">Hello Again.</h3>
                        <p>To keep connected with us please login with your personal info.</p>
                    </div>
                    <!-- /.wb-blk -->
                </div>
                <!-- /.img-left -->
                <form class="login-form" id="form1" runat="server">
                    <div class="form-group mb-3">
                        <asp:Label ID="lblEmail" CssClass="form-label" runat="server" >Email <span class="text-danger">*</span></asp:Label>
                        <asp:TextBox ValidationGroup="signin" CssClass="form-control" ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="signin" ID="RequiredFieldValidatorForEmail" ControlToValidate="txtEmail" runat="server" ForeColor="Red" ErrorMessage="Email field required."></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group mb-3">
                        <asp:Label ID="lblPassword" CssClass="form-label" runat="server" >Password <span class="text-danger">*</span></asp:Label>
                        <asp:TextBox ValidationGroup="signin" CssClass="form-control" ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="signin" ID="RequiredFieldValidatorForPassword" ControlToValidate="txtPassword" runat="server" ForeColor="Red" ErrorMessage="Password field required."></asp:RequiredFieldValidator>
                    </div>
                    <div class="mb-3 clearfix">
                        <div class="check-wrapper">
                            <div class="checkbox-wrapper-12">
                                <div class="cbx">
                                    <input id="cbx-12" type="checkbox" />
                                    <label for="cbx-12"></label>
                                    <svg viewbox="0 0 15 14" fill="none">
                                        <path d="M2 8.36364L6.23077 12L13 2"></path>
                                    </svg>
                                </div>
                                <!-- /.cbx -->
                                <svg xmlns="http://www.w3.org/2000/svg" version="1.1">
                                    <defs>
                                        <filter id="goo-12">
                                            <fegaussianblur in="SourceGraphic" stddeviation="4" result="blur"></fegaussianblur>
                                            <fecolormatrix in="blur" mode="matrix" values="1 0 0 0 0  0 1 0 0 0  0 0 1 0 0  0 0 0 22 -7" result="goo-12"></fecolormatrix>
                                            <feblend in="SourceGraphic" in2="goo-12"></feblend>
                                        </filter>
                                    </defs>
                                </svg>
                            </div>
                            <!-- /.checkbox-wrapper-12 -->
                            <span>Remember Me</span>
                        </div>
                        <!-- /.check-wrapper -->
                        <a href='<%= ResolveUrl("~/ForgotPassword.aspx") %>' class="recover-pwd">Forgot your password?</a>
                        <!-- /.recover-pwd -->
                    </div>
                    <asp:Label ID="lblErrorMsg" runat="server" CssClass="text-center mb-3 d-block" ForeColor="Red" Text=""></asp:Label>
                    <asp:Button ID="btnSignIn" ValidationGroup="signin" CssClass="btn btn-dark btn-signin w-100" runat="server" OnClick="BtnSignInClick" Text="Sign in" />
                    <div class="signin-location text-center mt-4">
                        <p>
                            Don't have an account?
           <span>
               <asp:LinkButton ID="lnkBtnRegister" class="text-decoration-none fw-bold signin-btn" OnClick="lnkBtnRegisterClick" runat="server">Sign up</asp:LinkButton>
           </span>
                        </p>
                    </div>
                    <!-- /.signin-location -->
                </form>
                <!-- /.login-form -->
            </div>
        </div>
    </section>
    <!-- /.signup-form -->
      <script src='<%= ResolveUrl("~/Scripts/Library/jquery.min.js") %>'></script>
        <script src='<%= ResolveUrl("~/Resources/js/common.js") %>'></script>
</body>
</html>

