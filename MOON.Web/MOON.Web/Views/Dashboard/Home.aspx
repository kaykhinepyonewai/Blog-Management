<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="MOON.Web.Views.Dashboard.Home" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentDashboardPlaceHolder" runat="server">
    <div class="row">
        <div class="col-12 col-md-12 col-xl-7">
            <div class="card quote-wrapper border-0">
                <div class="row">
                    <div class="col-sm-7">
                        <div class="card-body">
                            <h2 class="quote-name" id="username" runat="server"></h2>
                            <p class="quote-describe">
                                Lorem ipsum dolor sit amet, consectetur adipisicing elit. Pariatur eos aperiam,
                      accusantium modi harum error temporibus ab iure doloremque provident maxime non magnam,
                      laborum aut fugiat quasi sunt ex unde!
                            </p>
                        </div>
                    </div>
                    <div class="col-sm-5">
                        <div class="card-body character-main p-0">
                            <img src="../../../img/Dashboard/img_character.svg" class="character-item" alt="Illustrator character">
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.quote-wrapper -->
            <div class="treand-blog">
                <div class="clearfix breadcrumb-menu">
                    <p class="float-start">Trending Blogs</p>
                    <p class=" float-end"><a href="<%= ResolveUrl("~/Views/Dashboard/Article/TrendArticle.aspx") %>">View All</a></p>
                </div>
                <% if (GetTrending().Count > 0)
                    {  %>
                <div class="trend-slide slide clearfix">
                    <%foreach (var obj in GetTrending())
                        {  %>
                    <div class="slider-item">
                        <a href='<%= ResolveUrl("~/Views/Frontend/Detail.aspx?slug=" + obj.Slug) %>' class="text-dark text-decoration-none">
                            <h3><%= obj.Title %></h3>
                            <p><%= obj.Excerpt %></p>
                        </a>
                    </div>

                    <%} %>
                </div>
                <!-- /.trend-slide -->
                <% }
                else
                {  %>
                <p class="text-center notrend mb-0">No trending articles at the moment.</p>
                <% } %>
            </div>
            <!-- /.trend-blog -->
        </div>
        <div class="col-12 col-md-12 col-xl-5">
            <div class="row box-wrapper">
                <div class="col-12">
                    <div class="card box-item border-0 flex-row justify-content-between align-items-center">
                        <div class="d-flex align-items-center">
                            <div class="box-icon bg-b-orange">
                                <i class="fas fa-book"></i>
                            </div>
                            <div class="box-describe">
                                <a href="<%= ResolveUrl("~/Views/Dashboard/Article/ArticleList.aspx") %>">Total Articles</a>
                                <span class="counter-up" id="articleCount" runat="server"></span>
                            </div>
                        </div>
                        <a href="<%= ResolveUrl("~/Views/Dashboard/Article/ArticleList.aspx") %>" class="btn btn-link direct-btn">
                            <i class="fas fa-chevron-right"></i>
                        </a>
                    </div>
                </div>
                <div class="col-12">
                    <div class="card box-item border-0 flex-row justify-content-between align-items-center">
                        <div class="d-flex align-items-center">
                            <div class="box-icon bg-b-purple">
                                <i class="fas fa-box-archive"></i>
                            </div>
                            <div class="box-describe">
                                <a href="<%= ResolveUrl("~/Views/Dashboard/Article/ArchieveList.aspx") %>">Archieve Articles</a>
                                <span class="counter-up" id="ArchieveCount" runat="server"></span>
                            </div>
                        </div>
                        <a href="<%= ResolveUrl("~/Views/Dashboard/Article/ArchieveList.aspx") %>" class="btn btn-link direct-btn">
                            <i class="fas fa-chevron-right"></i>
                        </a>
                    </div>
                </div>
                <div class="col-12">
                    <div class="card box-item border-0 flex-row justify-content-between align-items-center">
                        <div class="d-flex align-items-center">
                            <div class="box-icon bg-b-blue">
                                <i class="fas fa-user-group"></i>
                            </div>
                            <div class="box-describe">
                                <a href="<%= ResolveUrl("~/Views/Dashboard/User/UserList.aspx") %>">Total Members</a>
                                <span class="counter-up" id="userCount" runat="server"></span>
                            </div>
                        </div>
                        <a href="<%= ResolveUrl("~/Views/Dashboard/User/UserList.aspx") %>" class="btn btn-link direct-btn">
                            <i class="fas fa-chevron-right"></i>
                        </a>
                    </div>
                </div>
                <div class="col-12">
                    <div class="card box-item box-last-item border-0 flex-row justify-content-between align-items-center">
                        <div class="d-flex align-items-center">
                            <div class="box-icon bg-b-red">
                                <i class="fas fa-bell"></i>
                            </div>
                            <div class="box-describe">
                                <a href="<%= ResolveUrl("~/Views/Dashboard/Report/ReportList.aspx") %>">Total Reports</a>
                                <span class="counter-up" id="reportCount" runat="server"></span>
                            </div>
                        </div>
                        <a href="<%= ResolveUrl("~/Views/Dashboard/Report/ReportList.aspx") %>" class="btn btn-link direct-btn">
                            <i class="fas fa-chevron-right"></i>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
