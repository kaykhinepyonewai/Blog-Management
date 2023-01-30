<%@ Page Title="MOON" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MOON.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-blk">
        <div class="container-fluid container-lg">
            <div class="d-flex flex-column flex-md-row flex-lg-row">
                <div class="col-12 col-md-8 col-lg-9 overflow-scroll vh-100 main-blk">
                    <div class="container">
                        <div class="row justify-content-evenly algin article-list">
                            <asp:Repeater ID="reptrArticles" runat="server" OnItemCommand="ReptrCardDataItemCommand" OnItemDataBound="ReptrCardItemDataBound">
                                <ItemTemplate>
                                    <asp:Label ID="lblArticleSlug" Visible="false" runat="server" Text='<%# Eval("Slug") %>'></asp:Label>
                                    <div class="col-12 col-md-12 col-lg-5 card article-item p-0">
                                        <asp:LinkButton ID="lnkBtnCard" runat="server" CommandName="Slug" CommandArgument='<%# Eval("Slug") %>'>
                                        <img src="<%# Eval("Thumbnail")%>"  alt="Thumbnail photo">
                                        <div class="card-body">
                                            <h4 class="article-ttl"><a href='<%# ResolveUrl("~/Views/Frontend/Detail.aspx?slug="+Eval("Slug")) %>'><%# Eval("Title") %></a></h4>
                                            <a href='<%# ResolveUrl("~/Default.aspx?category="+Eval("Name")) %>' class="text-decoration-none fw-bolder"><%# Eval("Name") %></a>
                                            <p class="article-describe text-secondary ">
                                            <%# Server.HtmlDecode(Eval("Excerpt").ToString()) %>
                                            </p>
                                            <div class="article-footer d-flex justify-content-between align-items-center">
                                                <div>
                                                     <p class="mb-0 fw-bold article-username"><%# Eval("Username") %></p> <!-- /.username-->
                                                        <span class="article-date d-block"><%# Eval("CreatedAt","{0: hh:mm tt}") %></span><!-- /.article-date -->
                                                        <span class="article-date"><%# Eval("CreatedAt","{0: MMM dd , yyyy}") %></span><!-- /.article-date -->
                                                </div>
                                                <a href='<%# ResolveUrl("~/Views/Frontend/Detail.aspx?slug="+Eval("Slug")) %>' class="text-decoration-none btn btn-secondary rounded">Read more</a>
                                            </div>
                                        </div>
                                        </asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div id="NoRecords" class="text-center" runat="server" visible="false">
                                        No articles are available at the moment.
                                    </div>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-md-4 col-lg-3 overflow-scroll category-side-blk border-3">
                    <div class="category-blk">
                        <h3 class="comm-side-hdr">Categories</h3>
                        <span class="divider"></span>
                        <ul class="list-group category-list-blk">
                            <li class="list-item <%= (Request.QueryString["category"] == null) ? "active" : ""%>"><a href="<%= ResolveUrl("~/Default.aspx") %>" class="">All</a></li>
                            <%foreach (var obj in GetCategories())
                                {  %>

                            <li class="list-item <%= (Request.QueryString["category"] == obj.Slug) ? "active" : ""%>  %>">
                                <a href='<%= ResolveUrl("~/Default.aspx?category="+obj.Slug) %>'><%= obj.Name %></a>
                            </li>

                            <%} %>
                        </ul>
                        <!-- /.category-list-blk -->
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
