<%@ Page Title="Report List" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="ReportList.aspx.cs" Inherits="MOON.Web.Views.Dashboard.Report.ReportList" %>
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
                        <li class="breadcrumb-item active" aria-current="page">Report Lists</li>
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
                        <p class="float-start mb-0 table-title">Report Lists</p>
                        <a href='<%= ResolveUrl("~/Views/Dashboard/Report/ReportCreate.aspx") %>' class="btn btn-secondary float-end">Report Create</a>
                    </div>
                    <div class="table-wrapper overflow-scroll">
                        <asp:HiddenField ID="hdnValueId" Value="" runat="server" />
                         <asp:GridView ID="gvReports" runat="server" CssClass="table table-bordered mt-3" AutoGenerateColumns="false" OnRowCommand="gvReportPostRowCommand"   AllowPaging="true"
                            OnPageIndexChanging="OnPageIndexChanging" PageSize="8">
                            <Columns>
                                <asp:TemplateField HeaderText="No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
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
                                <asp:TemplateField HeaderText="Report Message">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMessage" runat="server" Text='<%# Eval("Message") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Report Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreatedAt" runat="server" Text='<%#Eval("ReportAt","{0: MM/dd/yyyy} ") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <div class="text-nowrap">
                                        <asp:LinkButton ID="lnkbtnDetail" runat="server" CssClass="btn btn-warning btn-sm" CommandName="Detail" CommandArgument='<%# Eval("ArticleId") %>'>
                                            Detail
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="center" CssClass="pagination-bar" />
                            <EmptyDataTemplate>
                                <div align="center">No report records avaliable.</div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
              </div>
            </div>
</div>
</asp:Content>
