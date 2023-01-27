﻿<%@ Page Title="Trend Articles" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="TrendArticle.aspx.cs" Inherits="MOON.Web.Views.Dashboard.Article.TrendArticle" %>

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
                        <li class="breadcrumb-item active" aria-current="page">Trend Articles</li>
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
                        <p class="float-start mb-0 table-title">All Trends</p>
                    </div>
                    <div class="table-wrapper overflow-scroll">
                        <asp:HiddenField ID="hdnValueId" Value="" runat="server" />
                        <asp:GridView ID="gvTrendArticle" runat="server" CssClass="table table-bordered mt-3" AutoGenerateColumns="False" OnRowCommand="gvTrendArticleRowCommand" AllowPaging="true"
                            OnPageIndexChanging="OnPageIndexChanging" PageSize="8">
                            <Columns>
                                <asp:TemplateField HeaderText="No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblArticleId" Visible="false" runat="server" Text='<%# Eval("ArticleId") %>'></asp:Label>
                                        <asp:Label ID="lblArticleNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Thumbnail">
                                    <ItemTemplate>
                                        <img src="<%# Eval("Thumbnail") %>" class="imgThum" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Title">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTeacherName" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Excerpt">
                                    <ItemTemplate>
                                        <asp:Label ID="lblExcerptName" runat="server" Text='<%# Server.HtmlDecode(Eval("Excerpt").ToString()) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <div class="text-nowrap">
                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CssClass="btn btn-warning btn-sm" CommandName="Edit" CommandArgument='<%# Eval("ArticleId") %>'>
                                            Edit
                                        </asp:LinkButton>
                                            <asp:Button ID="btnDeleteHandler" CssClass="btn  btn-outline-danger d-none" runat="server" OnClick="gvRowDeleteing" Text="Delete" />
                                            <asp:Button ID="btnDelete" UseSubmitBehavior="false" CssClass="btn btn-danger btn-sm" CommandName="Delete" OnClientClick='<%# "return delHandle("+ Eval("ArticleId") + ");" %>'  runat="server" Text="Delete" />                                        
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="center" CssClass="pagination-bar" />
                            <EmptyDataTemplate>
                                <div align="center">No trend records avaliable.</div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
