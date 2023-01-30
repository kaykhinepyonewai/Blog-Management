<%@ Page Title="Report Detail" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="ReportDetail.aspx.cs" Inherits="MOON.Web.Views.Dashboard.Report.ReportDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentDashboardPlaceHolder" runat="server">
      <div class="row">
        <div class="col-12">
            <div class="card border-0 breadcrumb-main">
                <nav class="d-breadcrumb">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="<%= ResolveUrl("~/Views/Dashboard/Home.aspx") %>" class="text-decoration-none">Dashboard</a></li>
                        <li><i class="fas fa-chevron-right "></i></li>
                        <li class="breadcrumb-item"><a href="<%= ResolveUrl("~/Views/Dashboard/Report/ReportList.aspx") %>" class="text-decoration-none">Report Lists</a></li>
                        <li><i class="fas fa-chevron-right "></i></li>
                        <li class="breadcrumb-item active" aria-current="page">Report Article Detail</li>
                    </ol>
                </nav>
                <!-- /.d-breadcrumb -->
            </div>
            <!-- /.breadcrumb-main -->
        </div>
    </div>
    <div class="row">
        <div class="col-12">
            <div class="card border-0">
                <asp:HiddenField ID="hdArticleId" runat="server" Value="0" />
                <div class="card-body db-detail-blk m-auto">
                    <%foreach (var obj in GATReportDetail())
                        {  %>
                    <div class="article-user d-flex align-items-center">
                        <img src="<%= ResolveUrl(obj.Profile) %>" class="profile-img" alt="Profile image">
                        <div class="article-user-info">
                            <h4 class="article-username"><%=obj.UserName %></h4>
                            <a href="#" class="article-category fw-bold text-decoration-none"><%=obj.CategoryName %></a>
                            <span class="created-at"><%=obj.CreatedAt.ToString("MMM dd , yyyy") %></span>
                        </div>
                    </div>
                    <!-- /.article-user -->
                    <div class="main-detail">
                         <h3 class="article-hdr"><%=obj.Title %></h3>
                        <%if (obj.Thumbnail != string.Empty)
                            {  %>
                        <img src="<%=obj.Thumbnail %>" class="thumbnail-db-detail" alt="thumbnail photo">
                        <%} %>
                        <p class="db-detail-pgh">
                            <%= Server.HtmlDecode(obj.Description) %>
                        </p>
                        <!-- /.db-detail-pgh -->
                        <%} %>
                        <div class="article-slider slider">
                            <%foreach (var objImg in GTImage())
                            {  %>
                            <img src="<%=objImg.PhotoImage %>" class="slide-img" alt="#">
                            <%} %>
                        </div>
                        <!-- /.article-slider -->
                        <div class="d-flex justify-content-center align-items-center">
                            <asp:Button ID="btnSuppress" CssClass="btn btn-approve btn-primary" runat="server" Text="Suppress" OnClick="BtnSuppressClick" />
                            <asp:Button ID="btnRejectHandler" CssClass="btn btn-reject btn-outline-danger d-none" runat="server" Text="Reject" OnClick="BtnDeleteClick" />
                             <asp:Button ID="btnReject" UseSubmitBehavior="false" CssClass="btn btn-reject btn-outline-danger" CommandName="Delete" OnClientClick="return btnReject(this)" runat="server" Text="Remove" />
                        </div>
                    </div>
                    <!-- /.main-detail -->
                </div>
            </div>
        </div>
    </div>
</asp:Content>
