<%@ Page Title="Report Create" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="ReportCreate.aspx.cs" Inherits="MOON.Web.Views.Dashboard.Report.ReportCreate" %>

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
                        <li class="breadcrumb-item"><a href='<%= ResolveUrl("~/Views/Dashboard/Report/ReportList.aspx") %>' class="text-decoration-none">Report Lists</a></li>
                        <li><i class="fas fa-chevron-right "></i></li>
                        <li class="breadcrumb-item active" aria-current="page">Create Report</li>
                    </ol>
                </nav>
            </div>
        </div>
        <div class="col-12">
            <div class="card create-form border-0">
                <h3 class="create-ttl">Create Report</h3>
                <div class="form-group">
                    <asp:HiddenField ID="hdReportId" runat="server" Value="0" />
                    <div class="m-2">
                        <label for="<%= txtRpMessage.ClientID %>" class="form-label">Report Title <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtRpMessage" runat="server" Rows="30" CssClass="form-control"></asp:TextBox>
                        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
                        <asp:RequiredFieldValidator ID="validatorRpMessage" runat="server" ErrorMessage="Report input field is required!" ControlToValidate="txtRpMessage" ForeColor="Red" ValidationGroup="control"></asp:RequiredFieldValidator>
                    </div>
                    <div class="mt-2">
                        <asp:Button ID="btnRpCreate" CssClass="btn btn-primary" runat="server" OnClick="RpCreateClick" Text="Create" ValidationGroup="control" />
                        <asp:Button ID="btnRpCancel" CssClass="btn btn-outline-secondary ms-1" runat="server" Text="Cancel" OnClick="RpCancelClick" />
                    </div>
                </div>
            </div>
        </div>
    </div>
     <div class="row">
         <div class="col-12">
            <div class="card w-100 border-0">
                <div class="card-body">
                    <div class="table-wrapper overflow-scroll">
                        <asp:HiddenField ID="hdnValueId" Value="" runat="server" />
                         <asp:GridView ID="gvReports" runat="server" CssClass="table table-bordered mt-3" AutoGenerateColumns="false"  OnRowCommand="gvReportsRowEditing" AllowPaging="true"
                            OnPageIndexChanging="OnPageIndexChanging" PageSize="5">
                            <Columns>
                                <asp:TemplateField HeaderText="No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                        <asp:Label Visible="false" ID="lblReportID" runat="server" Text='<%# Eval("ReportId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Report Title">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReportMessage" runat="server" Text='<%# Eval("Message") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <div class="text-nowrap">
                                            <asp:LinkButton ID="lnkBtnEdit" runat="server" CssClass="btn btn-primary btn-sm" CommandName="Edit" CommandArgument='<%# Eval("ReportId") %>'>
                                                 Edit
                                            </asp:LinkButton>
                                           <asp:Button ID="btnDeleteHandler" CssClass="btn  btn-outline-danger d-none" runat="server" OnClick="gvRowDeleteing" Text="Delete" />
                                        <asp:Button ID="btnDelete" UseSubmitBehavior="false" CssClass="btn btn-danger btn-sm" CommandName="Delete" OnClientClick='<%# "return delHandle("+ Eval("ReportId") + ");" %>'  runat="server" Text="Delete" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="center" CssClass="pagination-bar" />
                            <EmptyDataTemplate>
                                <div align="center">No creative reports are avaliable.</div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
              </div>
            </div>
</div>
</asp:Content>
