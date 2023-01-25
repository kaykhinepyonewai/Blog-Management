<%@ Page Title="Create Category" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="CategoryCreate.aspx.cs" Inherits="MOON.Web.Views.Dashboard.Category.CategoryCreate" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentDashboardPlaceHolder" runat="server">
    <div class="row">
        <div class="col-12">
            <div class="card border-0 breadcrumb-main">
                <nav class="d-breadcrumb">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href='<%= ResolveUrl("~/Views/Dashboard/Home.aspx") %>' class="text-decoration-none">Dashboard</a></li>
                        <li><i class="fas fa-chevron-right "></i></li>
                        <li class="breadcrumb-item"><a href='<%= ResolveUrl("~/Views/Dashboard/Category/CategoryList.aspx") %>' class="text-decoration-none">Category</a></li>
                        <li><i class="fas fa-chevron-right "></i></li>
                        <li class="breadcrumb-item active" aria-current="page">Create Category</li>
                    </ol>
                </nav>
            </div>
        </div>
        <div class="col-12">
            <div class="card create-form border-0 w-100">
                <h3 class="create-ttl">Create Category</h3>
                <div class="form-group" runat="server">
                    <asp:HiddenField ID="hdCategoryId" runat="server" Value="0" />
                    <label class="form-label" for="<%= txtTitle.ClientID %>">Category Name</label>
                    <asp:TextBox ID="txtTitle" class="form-control" runat="server"></asp:TextBox>
                    <asp:Label ID="lblTitle" runat="server" Text="" Visible="false" Value="" ForeColor="Red"></asp:Label>
                    <asp:RequiredFieldValidator ID="revTitle" runat="server" ValidationGroup="validGp" ControlToValidate="txtTitle" ErrorMessage="Category name field is required." ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </div>

                <div class="form-group">
                    <label class="form-label" for="<%= txtSlug.ClientID %>">Slug</label>
                    <asp:TextBox ID="txtSlug" class="form-control" runat="server"></asp:TextBox>
                    <asp:Label ID="lblSlug" runat="server" Text="" Visible="false" Value="" ForeColor="Red"></asp:Label>
                    <asp:RequiredFieldValidator ID="revSlugName" runat="server" ValidationGroup="validGp" ControlToValidate="txtSlug" ErrorMessage="Category slug field is required" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </div>
                <div class="d-flex align-items-center">
                    <asp:Button ID="btnSave" CssClass="btn btn-primary text-white" runat="server" Text="Save" OnClick="btnSaveClick" ValidationGroup="validGp" />
                    <asp:Button ID="btnCancel" CssClass="btn btn-outline-secondary ms-1" runat="server" Text="Cancel" OnClick="btnCancelClick" />
                </div>

            </div>
        </div>
    </div>
</asp:Content>
