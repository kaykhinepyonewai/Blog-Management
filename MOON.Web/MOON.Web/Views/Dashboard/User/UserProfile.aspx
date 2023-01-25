<%@ Page Title="Profile" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="MOON.Web.Views.Dashboard.User.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentDashboardPlaceHolder" runat="server">
    <asp:HiddenField ID="hdnUsername" runat="server" Value="" />
    <% string[] user = (string[])Session["Users"]; %>
    <div class="row">
        <div class="col-12">
            <div class="card border-0 breadcrumb-main">
                <nav class="d-breadcrumb">
                    <ol class="breadcrumb m-0">
                        <%if (Convert.ToInt32(user[1]) == 1)
                            {  %>
                        <li class="breadcrumb-item"><a href='<%= ResolveUrl("~/Views/Dashboard/Home.aspx") %>' class="text-decoration-none">Dashboard</a></li>
                        <li><i class="fas fa-chevron-right "></i></li>
                        <li class="breadcrumb-item active" aria-current="page">Profile</li>
                        <% }
                            else if (Convert.ToInt32(user[1]) == 2)
                            {  %>
                        <li class="breadcrumb-item active" aria-current="page">Profile</li>
                        <% } %>
                    </ol>
                </nav>
            </div>
        </div>
        <div class="col-12">
            <div class="card create-form border-0">
                <div class="row">
                    <div class="form-group col-12 col-md-6 col-lg-6">
                        <label class="form-label" for="<%= txtUsername.ClientID %>">Username</label>
                        <asp:Label ID="lblCheckUserName" CssClass="form-label" runat="server" ForeColor="Red" Text=""></asp:Label>
                        <asp:TextBox ID="txtUsername" ValidationGroup="create" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="create" ID="RequiredFieldValidatorUsername" ControlToValidate="txtUsername" runat="server" ForeColor="Red" ErrorMessage="Username field is required."></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group col-12 col-md-6 col-lg-6">
                        <label class="form-label" for="<%= txtEmail.ClientID %>">Email</label>
                        <asp:Label ID="lblCheckEmail" CssClass="form-label" runat="server" ForeColor="Red" Text=""></asp:Label>
                        <asp:TextBox ValidationGroup="control" CssClass="form-control" ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="create" ID="RequiredFieldValidatorForEmail" ControlToValidate="txtEmail" runat="server" ForeColor="Red" ErrorMessage="Email field is required."></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group col-12 col-md-6 col-lg-6">
                        <label class="form-label" for="<%= txtFirstName.ClientID %>">First Name</label>
                        <asp:Label ID="lblCheckFirstName" CssClass="form-label" runat="server" ForeColor="Red" Text=""></asp:Label>
                        <asp:TextBox ID="txtFirstName" ValidationGroup="create" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="create" ID="RequiredFieldValidatorFirstName" ControlToValidate="txtFirstName" runat="server" ForeColor="Red" ErrorMessage="First Name field is required."></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group col-12 col-md-6 col-lg-6">
                        <label class="form-label" for="<%= txtLastName.ClientID %>">Last Name</label>
                        <asp:Label ID="lblCheckLastName" CssClass="form-label" runat="server" ForeColor="Red" Text=""></asp:Label>
                        <asp:TextBox ID="txtLastName" ValidationGroup="create" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="create" ID="RequiredFieldValidatorLastName" ControlToValidate="txtLastName" runat="server" ForeColor="Red" ErrorMessage="Last Name field is required."></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group col-12 col-md-6 col-lg-6">
                        <label class="form-label" for="<%= txtMobile.ClientID %>">Mobile</label>
                        <asp:Label ID="lblCheckMobile" CssClass="form-label" runat="server" ForeColor="Red" Text=""></asp:Label>
                        <asp:TextBox ValidationGroup="create" CssClass="form-control" ID="txtMobile" runat="server" TextMode="Phone"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="create" ID="RequiredFieldValidatorMobile" ControlToValidate="txtMobile" runat="server" ForeColor="Red" ErrorMessage="Mobile field is required."></asp:RequiredFieldValidator>
                        <br />
                        <asp:RegularExpressionValidator ID="RegexForMobile" runat="server" ForeColor="Red" ErrorMessage="The PhoneNumber field is not a valid phone number." ValidationGroup="control" ControlToValidate="txtMobile" ValidationExpression="^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$"></asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group col-12 col-md-6 col-lg-6">
                        <label class="form-label" for="<%= txtAddress.ClientID %>">Address</label>
                        <asp:Label ID="lblCheckAddress" CssClass="form-label" runat="server" ForeColor="Red" Text=""></asp:Label>
                        <asp:TextBox ValidationGroup="create" CssClass="form-control" ID="txtAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="create" ID="RequiredFieldValidatorAddress" ControlToValidate="txtAddress" runat="server" ForeColor="Red" ErrorMessage="Address field is required."></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group col-12 col-md-6 col-lg-6">
                        <label class="form-label" for="<%= ddlGender.ClientID %>">Gender</label>
                        <asp:DropDownList ID="ddlGender" ValidationGroup="create" CssClass="form-select" runat="server">
                            <asp:ListItem Value="None">Select your gender.</asp:ListItem>
                            <asp:ListItem Value="Male">Male</asp:ListItem>
                            <asp:ListItem Value="Female">Female</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorForGender" ValidationGroup="create" InitialValue="None" ControlToValidate="ddlGender" runat="server" ForeColor="Red" ErrorMessage="Select your gender."></asp:RequiredFieldValidator>
                    </div>
                    <%if (Convert.ToInt32(user[1]) == 1)
                        {  %>
                    <div class="form-group col-12 col-md-6 col-lg-6">
                        <label class="form-label" for="<%= ddlRole.ClientID %>">Role</label>
                        <span class="role-describe" id="roleText" runat="server">(Once you are an administrator, you may not switch your own role again.)</span>
                        <asp:DropDownList ID="ddlRole" ValidationGroup="create" CssClass="form-select" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorForRole" ValidationGroup="create" InitialValue="0" ControlToValidate="ddlRole" runat="server" ForeColor="Red" ErrorMessage="Select your role."></asp:RequiredFieldValidator>
                    </div>
                    <% } %>
                    <div class="form-group col-12 col-md-6 col-lg-6">
                        <asp:Image ID="imgBox" runat="server" CssClass="profile-img" />
                        <br />
                        <label class="form-label" for="<%= fuldProfile.ClientID %>">Profile</label>
                        <asp:FileUpload ID="fuldProfile" ValidationGroup="create" runat="server" CssClass="form-control" />
                        <asp:Label ID="lblImgError" CssClass="form-label" runat="server" ForeColor="Red" Text=""></asp:Label>
                    </div>
                    <div class="d-flex align-items-center">
                        <asp:Button ID="btnUpdate" ValidationGroup="create" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="btnUpdateProfile" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-outline-secondary ms-1" OnClick="btnCancelProfile" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
