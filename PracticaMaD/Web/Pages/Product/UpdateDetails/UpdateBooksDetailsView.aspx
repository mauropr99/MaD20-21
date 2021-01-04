<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="UpdateBooksDetailsView.aspx.cs" Inherits="Web.Pages.Product.UpdateBooksDetailsView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

    <form id="form1" runat="server">
        <br />
        <asp:LinkButton ID="lnkbutton" runat="server" OnClick="BtnBackToPreviousPage_Click" Text="<%$ Resources:Common, back %>" />
        <br />
        <br />
        <asp:Label ID="lblTitle" runat="server" Text="<%$ Resources: , title %>"></asp:Label>
        <asp:TextBox ID="txtTitleContent" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="lblAuthor" runat="server" Text="<%$ Resources: , author %>"></asp:Label>
        <asp:TextBox ID="txtAuthorContent" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="lblPrice" runat="server" Text="<%$ Resources: , price %>"></asp:Label>
        <asp:TextBox ID="txtPriceContent" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="lblStock" runat="server" Text="<%$ Resources: , availableStock %>"></asp:Label>
        <asp:TextBox ID="txtStockContent" runat="server"></asp:TextBox>
        <asp:RegularExpressionValidator runat="server" ForeColor="Red" ControlToValidate="txtStockContent"
            ValidationExpression="^(?!-1+$)[0-9]+$"
            ErrorMessage="<%$ Resources: , ControlStock %>" />
        <br />
        <br />
        <asp:Label ID="lblGenre" runat="server" Text="<%$ Resources: , genre %>"></asp:Label>
        <asp:TextBox ID="txtGenreContent" runat="server"></asp:TextBox>
        <br />
        <br />


        <asp:Button ID="Submit" runat="server" Text="<%$ Resources: , submit %>" OnClick="Submit_Click" />

    </form>


</asp:Content>

