<%@ Page meta:resourcekey="Page" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="UpdateBooksDetailsView.aspx.cs" Inherits="Web.Pages.Product.UpdateBooksDetailsView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

    <br />
    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="BtnBackToPreviousPage_Click" Text="<%$ Resources:Common, back %>" />
    <br />
    <br />
    <div class="field">
        <asp:Label ID="Label1" CssClass="label" runat="server" Text="<%$ Resources:, title %>" Font-Bold="True"></asp:Label>
        <span class="entry">
            <asp:TextBox ID="txtTitleContent" runat="server"></asp:TextBox>
        </span>
    </div>
    <br />
    <br />
    <div class="field">
        <asp:Label ID="Label2" CssClass="label" Font-Bold="True" runat="server" Text="<%$ Resources: , author %>"></asp:Label>
        <span class="entry">
            <asp:TextBox ID="txtAuthorContent" runat="server"></asp:TextBox>
        </span>
    </div>
    <br />
    <br />
    <div class="field">
        <asp:Label ID="Label3" CssClass="label" Font-Bold="True" runat="server" Text="<%$ Resources: , price %>"></asp:Label>
        <span class="entry">
            <asp:TextBox ID="txtPriceContent" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator runat="server" ForeColor="Red" ControlToValidate="txtPriceContent"
                ValidationExpression="-?\d{1,3}(,\d{3})*(\.\d+)?"
                ErrorMessage="<%$ Resources: , ControlStock %>" />
        </span>
    </div>
    <br />
    <br />
    <div class="field">
        <asp:Label ID="Label4" CssClass="label" Font-Bold="True" runat="server" Text="<%$ Resources: , availableStock %>"></asp:Label>
        <span class="entry">
            <asp:TextBox ID="txtStockContent" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator runat="server" ForeColor="Red" ControlToValidate="txtStockContent"
                ValidationExpression="^(?!-1+$)[0-9]+$"
                ErrorMessage="<%$ Resources: , ControlStock %>" />
        </span>
    </div>
    <br />
    <br />
    <div class="field">
        <asp:Label ID="Label5" CssClass="label" Font-Bold="True" runat="server" Text="<%$ Resources: , genre %>"></asp:Label>
        <span class="entry">
            <asp:TextBox ID="txtGenreContent" runat="server"></asp:TextBox>
        </span>
    </div>
    <br />
    <br />
    <asp:LinkButton ID="linkViewComment" runat="server" OnClick="Book_Click" Text="<%$ Resources: , comments %>" />
    &nbsp &nbsp &nbsp
        <asp:LinkButton ID="btnNewComment" runat="server" Text="<%$ Resources:, newComment%>" OnClick="BtnNewComment_Click" />
    <br />
    <br />
    <asp:Button ID="Submit" runat="server" Text="<%$ Resources: , submit %>" OnClick="Submit_Click" />

</asp:Content>

