<%@ Page Title="Password Setting" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="PasswordSetting.aspx.cs" Inherits="MOON.Web.Views.Dashboard.User.PasswordSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentDashboardPlaceHolder" runat="server">
    <% string[] user = (string[])Session["Users"];%>
    <asp:HiddenField ID="hdnUsername" runat="server" Value="" />
    <div class="row">
        <div class="col-12">
            <div class="card border-0 breadcrumb-main">
                <nav class="d-breadcrumb">
                    <ol class="breadcrumb m-0">
                        <% if (Convert.ToInt32(user[1]) == 1)
                            {  %>
                        <li class="breadcrumb-item"><a href='<%= ResolveUrl("~/Views/Dashboard/Home.aspx") %>' class="text-decoration-none">Dashboard</a></li>
                        <li><i class="fas fa-chevron-right "></i></li>
                        <li class="breadcrumb-item active" aria-current="page">Password Setting</li>
                        <%  }
                            else if (Convert.ToInt32(user[1]) == 2)
                            {  %>
                        <li class="breadcrumb-item active" aria-current="page">Password Setting</li>
                        <% } %>
                    </ol>
                </nav>
            </div>
        </div>
        <div class="col-6">
            <div class="card create-form border-0">
                <div class="row">
                    <div class="form-group col-12">
                        <label class="form-label" for="<%= txtEmail.ClientID %>">Email <span class="text-danger">*</span></label>
                        <asp:Label ID="lblCheckEmail" CssClass="form-label" runat="server" ForeColor="Red" Text=""></asp:Label>
                        <asp:TextBox ID="txtEmail" ValidationGroup="control" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="control" ID="RequiredFieldValidatorEmail" ControlToValidate="txtEmail" runat="server" ForeColor="Red" ErrorMessage="Email field is required."></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group col-12">
                        <label class="form-label" for="<%= txtPassword.ClientID %>">New Password <span class="text-danger">*</span></label>
                        <asp:Label ID="lblCheckPassword" CssClass="form-label" runat="server" ForeColor="Red" Text=""></asp:Label>
                        <asp:TextBox ID="txtPassword" ValidationGroup="control" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="control" ID="RequiredFieldValidatorForPassword" ControlToValidate="txtPassword" runat="server" ForeColor="Red" ErrorMessage="Password field required."></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group col-12">
                        <label class="form-label" for="<%= txtConfirmPwd.ClientID %>">Confirm Password <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtConfirmPwd" ValidationGroup="control" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="control" ID="RequiredFieldValidator1" ControlToValidate="txtConfirmPwd" runat="server" ForeColor="Red" ErrorMessage="Confirm Password field required."></asp:RequiredFieldValidator>
                    </div>
                    <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
                    <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" ValidationGroup="control" CssClass="btn btn-primary mt-3" OnClick="btnChangePasswordClick" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
