<%@ Page Title="MOON | Detail" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="MOON.Web.Views.Frontend.Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-blk">
        <div class="container-fluid container-lg">
            <div class="d-flex flex-column-reverse flex-md-row flex-lg-row">
                <div class="col-12 col-md-4 col-lg-4 vh-100 overflow-scroll side-wrapper border-3">
                    <div class="category-blk">
                        <h3 class="comm-side-hdr ">Categories</h3>
                        <span class="divider"></span>
                        <ul class="list-group category-list-blk">
                            <li class="list-item <%= (Request.QueryString["category"] == null) ? "active" : ""%>"><a href="<%= ResolveUrl("~/Default.aspx") %>">All</a></li>
                            <%foreach (var obj in GetCategories())
                                {  %>

                            <li class="list-item <%= (Request.QueryString["category"] == obj.Slug) ? "active" : ""%>  %>">
                                <a href='<%= ResolveUrl("~/Default.aspx?category="+obj.Slug) %>'><%= obj.Name %></a>
                            </li>

                            <%} %>
                        </ul>
                        <!-- /.category-list-blk -->
                    </div>
                    <!-- /.category-blk -->
                    <div class="related-blk">
                        <h3 class="comm-side-hdr ">Related Articles</h3>
                        <span class="divider"></span>
                        <ul class="list-group related-list-blk">
                            <asp:Repeater ID="reptrRelatedLists" runat="server" OnItemCommand="ReptrRelatedItemCommand" OnItemDataBound="ReptrRelatedItemDataBound">
                                <ItemTemplate>
                                    <li class="related-item">
                                        <asp:LinkButton ID="lnkBtnRelated" CssClass="text-decoration-none d-flex justify-content-between align-items-center" CommandName="RelatedSlug" CommandArgument='<%# Eval("slug") %>' runat="server">
                                <img src="<%# Eval("Thumbnail")  %>" alt="#" class="related-img">
                                <div class="col-8 col-sm-8 artl-related-ttl">
                                    <p class="related-title mb-0"><%# Eval("Title") %></p>
                                    <span class="related-category"><%# Eval("Name") %></span>
                                </div>
                                        </asp:LinkButton>
                                    </li>
                                    <!-- /.related-item -->
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div id="NoRecords" class="text-center" runat="server" visible="false">
                                        No related posts at the moment.
                                    </div>
                                </FooterTemplate>
                            </asp:Repeater>
                        </ul>
                        <!-- /.related-list-blk -->
                    </div>
                    <!-- /.related-blk -->
                </div>
                <asp:HiddenField ID="hdSlug" runat="server" />
                <div class="col-12 col-md-8 col-lg-8 vh-100 overflow-scroll main-blk">
                    <div class="alert alert-success" visible="false" id="reportAlert" runat="server" role="alert">
                       You have successfully reported this atitcle!.
                    </div>
                    <div class="article-detail">
                        <div class="article-slider slider">
                            <%foreach (var objImg in GTImage())
                                {  %>
                            <img src="<%=objImg.PhotoImage %>" class="slide-img" alt="#">

                            <%} %>
                        </div>
                        <!-- /.article-slider -->
                        <div class="article-wrapper">
                            <div class="d-flex justify-content-between align-items-center">
                                <div class="article-user d-flex justify-content-between align-items-center">
                                    <%foreach (var obj in GATDetailBySlug())
                                        {  %>
                                    <img src="<%= ResolveUrl(obj.Profile) %>" class="profile-img" alt="Profile image">
                                    <div class="article-user-info">
                                        <h4 class="article-username"><%=obj.UserName %></h4>
                                        <a href="#" class="article-category text-decoration-none"><%=obj.CategoryName %></a>
                                        <span class="created-at"><%=obj.CreatedAt.ToString("MMM dd , yyyy") %></span>
                                    </div>
                                </div>
                                <%if (Convert.ToInt32(Session["UserId"]) != 0)
                                    {  %>

                                <div class="dropdown option-list">
                                    <button class="border-0 bg-transparent" type="button" data-bs-toggle="dropdown" data-bs-auto-close="outside" aria-expanded="false">
                                        <i class="fas fa-ellipsis-vertical"></i>
                                    </button>
                                    <ul class="dropdown-menu">

                                        <%if (GATPermit() > 0 || GetRoleId() == 1)
                                            {  %>
                                        <li><a class="dropdown-item" href='<%= ResolveUrl("~/Views/Dashboard/User/UserProfile.aspx?username="+obj.UserName) %>'>Profile</a></li>
                                        <li><a class="dropdown-item" href="<%= ResolveUrl("~/Views/Dashboard/Article/ArticleCreate.aspx?id="+ obj.ArticleId) %>">Edit</a></li>
                                        <li>
                                            <asp:LinkButton ID="lnkDelete" CssClass="dropdown-item" runat="server" OnClick="btnlnkDeleteClick">Delete</asp:LinkButton>
                                        </li>
                                        <%} %>
                                        <li class="dropdown-item">
                                            <div class="btn-group">
                                                <button class="border-0 bg-transparent report-btn p-0" type="button" data-bs-toggle="dropdown" data-bs-auto-close="outside" aria-expanded="false">
                                                    Report
                                                </button>
                                                <ul class="dropdown-menu">
                                                    <asp:Repeater ID="reptrReportLists" runat="server" OnItemCommand="ReptrReportDataItemCommand">
                                                        <ItemTemplate>
                                                            <li>
                                                                <a class="dropdown-item" href="#"></a>
                                                                <asp:LinkButton ID="lnkBtnReport" CssClass="dropdown-item" runat="server" CommandName="ReportId" CommandArgument='<%# Eval("ReportId") %>'><%# Eval("Message") %></asp:LinkButton>
                                                            </li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ul>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                                <%} %>
                            </div>
                            <div class="article-info">
                                <h3 class="article-hdr"><%=obj.Title %></h3>
                                <p class="article-describe">
                                    <%= Server.HtmlDecode(obj.Description) %>
                                </p>
                            </div>
                            <div class="reaction-controller">
                                <div class="reaction">

                                    <table>
                                        <tbody>
                                            <asp:HiddenField ID="txtLikedHidden" runat="server" />
                                            <asp:HiddenField ID="txtLikedHidden1" runat="server" />
                                            <tr>
                                                <td style="display: none"><%=obj.ArticleId %></td>
                                                <td style="display: none"><%=obj.UserId %></td>
                                                <td>
                                                    <%if (CountButton(obj.ArticleId) > 0)
                                                        {  %>
                                                    <asp:LinkButton ID="btnUnlike" CssClass="heart-btn1 heart-btn" OnClientClick="btnUnLike(this)" OnClick="UnLikeClick" runat="server">
                                                    <i class="fa-solid fa-heart"></i>
                                                    </asp:LinkButton>
                                                    <%} %>
                                                    <%else
                                                        {  %>
                                                    <asp:LinkButton ID="btnLike" CssClass="heart-btn2 heart-btn" OnClientClick="btnLike(this)" OnClick="LikeClick" runat="server">
                                                    <i class="fa-solid fa-heart"></i>
                                                    </asp:LinkButton>
                                                    <%} %>
                                                </td>
                                                <td><span class="reaction-count viewerBtn"><%=Count(obj.ArticleId) %></span></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <%} %>
                                </div>
                                <asp:LinkButton ID="lnkBtnComment" CssClass="text-decoration-none comment border-0 commentBtn bg-transparent" ValidationGroup="comment" runat="server">
                                    <i class="fa-regular fa-comment"></i>
                                    <span class="message-count" id="msgCount" runat="server"></span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <!-- /.article-wrapper -->
                    </div>
                    <!-- /.article-detail -->
                </div>
                <!-- /.main-blk -->
                <div class="reaction-viewers viewers">
                    <div class="reaction-viewers-close closeReaction viewerClose"></div>
                    <div class="reaction-viewers-wrapper">
                        <div class="reaction-viewer-title clearfix">
                            <button class="border-0 bg-transparent close-btn closeReaction float-end">
                                <i class="fa-solid fa-xmark"></i>
                            </button>
                            <p>You got <span><%=CountLike() %></span> loves for <span>"<%= GetTitle() %>"</span> </p>
                        </div>
                        <div class="reaction-viewers-lists">
                            <asp:Repeater ID="reptrReactionViewers" runat="server">
                                <ItemTemplate>
                                    <div class="reaction-viewers-item">
                                        <asp:Image ID="imgProfile" ImageUrl='<%# Eval("Profile") %>' AlternateText="User Profile" runat="server" />
                                        <span><%# Eval("Username") %></span>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
                <!-- /.reaction-viewers-->
                <div class="comment-asidenav">
                    <div class="comment-aside-close"></div>
                    <div class="comment-blk">
                        <div class="comment-header">
                            <h3>Comments(<span id="msgCounts" runat="server"></span>)</h3>
                            <button class="close-com-btn">
                                <i class="fa-solid fa-xmark"></i>
                            </button>
                        </div>
                        <div class="comment-card">
                            <div class="comment-owner">
                                <% if (Session["Users"] == null)
                                    {  %>
                                <img src="~/img/Dashboard/Profile/Guest.png" runat="server" alt="User Profile">
                                <span>Guest</span>
                                <% }
                                else
                                { %>
                                <asp:Image ID="imgProfile" runat="server" AlternateText="User Profile" />
                                <span runat="server" id="username"></span>
                                <% } %>
                            </div>
                            <div action="#" class="comment-box">
                                <asp:HiddenField ID="hdnCommentId" runat="server" Value="0" />
                                <asp:TextBox ID="txtComment" CssClass="form-control comment-input shadow-none" ValidationGroup="txtComment" TextMode="MultiLine" runat="server" PlaceHolder="What are your thoughts?" Rows="5" Columns="30"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorForComment" runat="server" ControlToValidate="txtComment" ValidationGroup="txtComment" ForeColor="Red" ErrorMessage="Write some comments before you published it."></asp:RequiredFieldValidator>
                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
                                <asp:Button ID="btnCommentSend" OnClick="BtnCommentSendClick"  runat="server" Text="Response" ValidationGroup="txtComment" />
                            </div>
                        </div>
                        <ul class="comment-lists">
                            <asp:Repeater ID="reptrCommentLists" runat="server" OnItemDataBound="ReptrCommentItemDataBound">
                                <ItemTemplate>
                                    <li class="comment-item">
                                        <div class="user-item-group">
                                            <asp:Image ID="imgProfile" ImageUrl='<%# Eval("Profile") %>' AlternateText="User Profile" runat="server" />
                                            <div class="user-name">
                                                <p><%# Eval("Username") %></p>
                                                <span><%# Eval("CreatedAt","{0: hh:mm tt}") %></span>
                                            </div>
                                        </div>
                                        <p class="comment-paragraph"><%# Eval("Message") %></p>
                                        <div class="divider"></div>
                                    </li>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div id="NoRecords" class="text-center" runat="server" visible="false">
                                        There are currently no responses for this stroy.<br />
                                        Be the first to response
                                    </div>
                                </FooterTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
                <!-- /.comment-asidenav -->
            </div>
        </div>
    </section>
    <!-- /.content-blk -->

    <script language="javascript" type="text/javascript">

        function btnLike(element) {
            var currentRow = $(element).closest("tr");
            var col = currentRow.find("td:eq(0)").text();
            document.getElementById('<%= txtLikedHidden.ClientID %>').value = col;
            var col1 = currentRow.find("td:eq(1)").text();
            document.getElementById('<%= txtLikedHidden1.ClientID %>').value = col1;
        }

        function btnUnLike(element) {
            var currentRow = $(element).closest("tr");
            var col = currentRow.find("td:eq(0)").text();
            document.getElementById('<%= txtLikedHidden.ClientID %>').value = col;
            var col1 = currentRow.find("td:eq(1)").text();
            document.getElementById('<%= txtLikedHidden1.ClientID %>').value = col1;
        }
    </script>
</asp:Content>
