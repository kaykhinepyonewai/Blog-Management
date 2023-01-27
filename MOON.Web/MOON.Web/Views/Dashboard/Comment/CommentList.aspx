<%@ Page Title="Comment Lists" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="CommentList.aspx.cs" Inherits="MOON.Web.Views.Dashboard.Comment.CommentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentDashboardPlaceHolder" runat="server">
    <% string[] user = (string[])Session["Users"];%>
    <div class="row">
        <div class="col-12">
            <div class="card border-0 breadcrumb-main">
                <nav class="d-breadcrumb">
                    <ol class="breadcrumb m-0">
                        <% if (Convert.ToInt32(user[1]) == 1)
                            {  %>
                        <li class="breadcrumb-item"><a href='<%= ResolveUrl("~/Views/Dashboard/Home.aspx") %>' class="text-decoration-none">Dashboard</a></li>
                        <li><i class="fas fa-chevron-right "></i></li>
                        <li class="breadcrumb-item active" aria-current="page">Comment Lists</li>
                        <% }
                        else
                        {  %>
                        <li class="breadcrumb-item active" aria-current="page">Comment Lists</li>
                        <% } %>
                    </ol>
                </nav>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-12">
            <div class="card w-100 border-0">
                <div class="card-body">
                    <div class="clearfix table-create">
                        <p class="float-start mb-0 table-title">Your Article's Comment</p>
                    </div>
                    <div class="table-wrapper overflow-scroll">
                        <asp:HiddenField ID="hdnValueId" Value="" runat="server" />
                        <asp:HiddenField ID="hdnArticleId" Value="" runat="server" />
                        <asp:GridView ID="gvComments" runat="server" CssClass="table table-bordered mt-3" AutoGenerateColumns="false" AllowPaging="true"
                            OnPageIndexChanging="OnPageIndexChanging" PageSize="8">
                            <Columns>
                                <asp:TemplateField HeaderText="No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                        <asp:Label Visible="false" ID="lblCommentID" runat="server" Text='<%# Eval("CommentId") %>'></asp:Label>
                                        <asp:Label Visible="false" ID="lblArticleID" runat="server" Text='<%# Eval("ArticleId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Username">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("Username") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Article Title">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Content">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContent" runat="server" Text='<%# Eval("Message") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <div class="text-nowrap">
                                            <asp:Button ID="btnDeleteHandler" CssClass="btn  btn-outline-danger d-none" runat="server" OnClick="gvRowDeleteing" Text="Delete" />
                                        <asp:Button ID="btnDelete" UseSubmitBehavior="false" CssClass="btn btn-danger btn-sm" CommandName="Delete" OnClientClick='<%# "return delHandle("+ Eval("CommentId") + " , "+ Eval("ArticleId") +");" %>'  runat="server" Text="Delete" />  
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="center" CssClass="pagination-bar" />
                            <EmptyDataTemplate>
                                <div align="center">No comment records avaliable.</div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
