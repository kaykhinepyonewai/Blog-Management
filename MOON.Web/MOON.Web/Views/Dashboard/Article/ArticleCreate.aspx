<%@ Page Title="Create Article" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="ArticleCreate.aspx.cs" Inherits="MOON.Web.Views.Dashboard.ArticleCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentDashboardPlaceHolder" runat="server">
    <% string[] user = (string[])Session["Users"];%>

    <div class="row">
        <div class="col-12">
            <div class="card border-0 breadcrumb-main">
                <nav class="d-breadcrumb">
                    <ol class="breadcrumb m-0">
                        <%if (Convert.ToInt32(user[1]) == 1)
                            {  %>
                        <li class="breadcrumb-item"><a href='<%= ResolveUrl("~/Views/Dashboard/Home.aspx") %>' class="text-decoration-none">Dashboard</a></li>
                        <li><i class="fas fa-chevron-right "></i></li>
                        <li class="breadcrumb-item"><a href='<%= ResolveUrl("~/Views/Dashboard/Article/ArticleList.aspx") %>' class="text-decoration-none">Articles</a></li>
                        <li><i class="fas fa-chevron-right "></i></li>
                        <li class="breadcrumb-item active" aria-current="page">Create Article</li>
                        <% }
                            else if (Convert.ToInt32(user[1]) == 2)
                            {  %>
                        <li class="breadcrumb-item"><a href='<%= ResolveUrl("~/Views/Dashboard/Article/ArticleList.aspx") %>' class="text-decoration-none">Articles</a></li>
                        <li><i class="fas fa-chevron-right "></i></li>
                        <li class="breadcrumb-item active" aria-current="page">Create Article</li>
                        <% } %>
                    </ol>
                </nav>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-12">
            <div class="card create-form border-0">
                <h3 class="create-ttl">Share your stories</h3>
                <div class="form-group">
                    <asp:HiddenField ID="hdArticleId" runat="server" Value="0" />
                    <label class="form-label" for="<%= txtTitle.ClientID %>">Title</label>
                    <asp:Label ID="lblTitle" CssClass="form-label" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                    <asp:TextBox ID="txtTitle" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="revTitle" runat="server" ValidationGroup="validGp" ControlToValidate="txtTitle" ErrorMessage="Input title field is required." ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label class="form-label" for="<%= ddlCategoryName.ClientID %>">Category Name</label>
                    <asp:DropDownList ID="ddlCategoryName" runat="server" CssClass="form-control"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="revCategoryName" runat="server" InitialValue="0" ValidationGroup="validGp" ControlToValidate="ddlCategoryName" ErrorMessage="Select your category." ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label class="form-label" for="<%= txtSlug.ClientID %>">Slug</label>
                     <asp:Label ID="lblSlug" CssClass="form-label" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                    <asp:TextBox ID="txtSlug" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="revSlugName" runat="server" ValidationGroup="validGp" ControlToValidate="txtSlug" ErrorMessage="Input slug field is required." ForeColor="#FF3300"></asp:RequiredFieldValidator>   
                </div>
                <div class="form-group">
                    <label class="form-label" for="<%= txtDescription.ClientID %>">Description</label>
                    <asp:TextBox ID="txtDescription" class="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="revDescription" runat="server" ValidationGroup="validGp" ControlToValidate="txtDescription" ErrorMessage="Description field is required." ForeColor="#FF3300"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label class="form-label" for="<%= FileUploadThum.ClientID %>">Thumbnail</label>
                    <asp:FileUpload ID="FileUploadThum" class="form-control" runat="server" />
                    <asp:Image ID="imgBox" CssClass="article-thumbnail mt-2" runat="server" />
                    <asp:Label ID="lblThubmail" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                </div>
                <div class="form-group">
                    <label class="form-label" for="<%= FileUploadPhoto.ClientID %>">Photos</label>
                    <asp:Label ID="lblPhoto" CssClass="form-label" runat="server" Visible="false" Text="Label" ForeColor="#CC0000"></asp:Label>
                    <asp:FileUpload ID="FileUploadPhoto" AllowMultiple="true" class="form-control" runat="server" />
                    <asp:RequiredFieldValidator ID="revPhoto" runat="server" ValidationGroup="validGp1" ControlToValidate="FileUploadPhoto" ErrorMessage="Multiple photos required." ForeColor="#FF3300"></asp:RequiredFieldValidator>
                    <div class="table-wrapper">
                        <asp:HiddenField ID="txtHidden" runat="server" />
                        <table class="table table-bordered align-middle">
                            <tbody>
                                <% int i = 0;%>
                                <%foreach (var obj in GTImage())
                                    {  %>
                                <% i++; %>
                                <tr>
                                    <td class="visually-hidden"><%=obj.PhotoId %></td>
                                    <td><%= i %></td>
                                    <td>
                                        <img src="<%=obj.PhotoImage %>" class="imgThum" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnDelete" CssClass="btn btn-danger" runat="server" Text="Delete" OnClientClick="btnDelete(this)" OnClick="btnDeleteClick" />
                                    </td>
                                </tr>
                                <%} %>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="d-flex align-items-center ms-1">
                    <asp:Button ID="btnSave" CssClass="btn btn-primary text-white" runat="server" Text="Save" OnClick="btnSaveClick" ValidationGroup="validGp" />
                    <asp:Button ID="btnCancel" CssClass="btn btn-outline-secondary ms-1" runat="server" Text="Cancel" OnClick="btnCancelClick" />
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function btnDelete(element) {

            var currentRow = $(element).closest("tr");
            var col1 = currentRow.find("td:eq(0)").text();
            document.getElementById('<%= txtHidden.ClientID %>').value = col1;
        }
    </script>



</asp:Content>
