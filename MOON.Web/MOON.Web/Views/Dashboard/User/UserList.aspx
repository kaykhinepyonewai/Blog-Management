<%@ Page Title="Profile" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="MOON.Web.Views.Dashboard.User.UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentDashboardPlaceHolder" runat="server">
    <div class="row">
        <div class="col-12">
            <div class="card border-0 breadcrumb-main">
                <nav class="d-breadcrumb">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href='<%= ResolveUrl("~/Views/Dashboard/Home.aspx") %>' class="text-decoration-none">Dashboard</a></li>
                        <li><i class="fas fa-chevron-right "></i></li>
                        <li class="breadcrumb-item active" aria-current="page">Users</li>
                    </ol>
                </nav>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-12">
            <div class="card w-100 vh-100 border-0">
                <div class="card-body">
                    <div class="clearfix table-create">
                        <p class="float-start mb-0 table-title">All Users</p>
                        <div class="dropdown float-end">
                            <button class="btn btn-secondary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                                Options
                            </button>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton ID="lnkBtnExport" CssClass="dropdown-item" OnClick="LnkBtnExport" runat="server">Export as xlsx</asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton ID="lnkImportBtn" CssClass="dropdown-item" data-bs-toggle="modal" data-bs-target="#importModal" runat="server">Import as xlsx</asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="table-wrapper overflow-scroll">
                        <asp:GridView ID="gvUsers" runat="server" CssClass="table table-bordered mt-3" AutoGenerateColumns="false" OnRowCommand="gvUserRowCommand" OnRowDeleting="gvUserRowDeleting" AllowPaging="true"
                            OnPageIndexChanging="OnPageIndexChanging" PageSize="8">
                            <Columns>
                                <asp:TemplateField HeaderText="No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                        <asp:Label Visible="false" ID="lblUserID" runat="server" Text='<%# Eval("UserId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Username">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("Username") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMobile" runat="server" Text='<%# Eval("Mobile") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gender">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGender" runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Role">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRole" runat="server" Text='<%# Eval("Role") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <div class="text-nowrap">
                                            <asp:LinkButton ID="lnkBtnEdit" runat="server" CssClass="btn btn-primary btn-sm" CommandName="Edit" CommandArgument='<%# Eval("Username") %>'>
                                                 Edit
                                            </asp:LinkButton>
                                            <asp:Button ID="btnDelete" UseSubmitBehavior="false" CssClass="btn btn-danger btn-sm" CommandName="Delete" OnClientClick="return delHandle(this)" runat="server" Text="Delete" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="center" CssClass="pagination-bar" />
                             <EmptyDataTemplate>
                                <div align="center">No user records avaliable.</div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal import box -->
        <div class="modal fade" id="importModal" tabindex="-1" aria-labelledby="importModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="importModalLabel">Import excel (only support xlsx)</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body m-auto">
                        <asp:RequiredFieldValidator ValidationGroup="importValidate" ID="requiredFileValidation" ControlToValidate="fuldImport" runat="server" ForeColor="Red" ErrorMessage="File input field required!"></asp:RequiredFieldValidator>
                        <asp:FileUpload ID="fuldImport" ValidationGroup="importValidate" CssClass="form-control" runat="server" />
                        <asp:RegularExpressionValidator
                            ID="regexFileValidation" ValidationGroup="importValidate" ForeColor="Red" runat="server" ControlToValidate="fuldImport"
                            ErrorMessage="File type does not support."
                            ValidationExpression="^([0-9a-zA-Z_\-~ :\\()])+(.xlsx|.xls)$"></asp:RegularExpressionValidator>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <asp:Button ID="btnImport" ValidationGroup="importValidate" CssClass="btn btn-primary" runat="server" Text="Import" OnClick="LnkImportBtn" />
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
