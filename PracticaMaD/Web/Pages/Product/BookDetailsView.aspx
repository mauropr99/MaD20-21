<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="BookDetailsView.aspx.cs" Inherits="Web.Pages.Product.BookDetailsView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

    <form id="form1" runat="server">
        <br />
        <br />
        <asp:Label ID="lblTitle" runat="server" Text="Title: "></asp:Label>
        <asp:Label ID="lblTitleContent" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblAuthor" runat="server" Text="Author: "></asp:Label>
        <asp:Label ID="lblAuthorContent" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblPrice" runat="server" Text="Price: "></asp:Label>
        <asp:Label ID="lblPriceContent" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblStock" runat="server" Text="Available stock: "></asp:Label>
        <asp:Label ID="lblStockContent" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblGenre" runat="server" Text="Genre: "></asp:Label>
        <asp:Label ID="lblGenreContent" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblReleaseDate" runat="server" Text="Release date: "></asp:Label>
        <asp:Label ID="lblReleaseDateContent" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblIsbnCode" runat="server" Text="ISBN: "></asp:Label>
        <asp:Label ID="lblIsbnCodeContent" runat="server"></asp:Label>
        <br />
        <br />
        <br />
        <asp:Label ID="lblQuantity" runat="server" Text="Quantity: "></asp:Label>
        <asp:DropDownList ID="DropDownListQuantity" runat="server">
            <asp:ListItem Enabled="False">1</asp:ListItem>
            <asp:ListItem Enabled="False">2</asp:ListItem>
            <asp:ListItem Enabled="False">3</asp:ListItem>
            <asp:ListItem Enabled="False">4</asp:ListItem>
            <asp:ListItem Enabled="False">5</asp:ListItem>
            <asp:ListItem Enabled="False">6</asp:ListItem>
            <asp:ListItem Enabled="False">7</asp:ListItem>
            <asp:ListItem Enabled="False">8</asp:ListItem>
            <asp:ListItem Enabled="False">9</asp:ListItem>
            <asp:ListItem Enabled="False">10</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <br />
        <asp:Button ID="btnAddToShoppingCart" runat="server" Text="Add to shopping cart" />
        <br />
        <br />
        
    </form>




</asp:Content>
