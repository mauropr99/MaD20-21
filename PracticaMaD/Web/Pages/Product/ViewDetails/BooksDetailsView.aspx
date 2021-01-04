﻿<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="BooksDetailsView.aspx.cs" Inherits="Web.Pages.Product.BooksDetailsView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

    <form id="form1" runat="server">
        <div>
            <br />
            <asp:LinkButton ID="lnkbutton" runat="server" OnClick="BtnBackToPreviousPage_Click" Text="<%$ Resources:Common, back %>" />
            <br />
            <br />
            <asp:Label ID="lblTitle" runat="server" Text="<%$ Resources: , title %>"></asp:Label>
            <asp:Label ID="lblTitleContent" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblAuthor" runat="server" Text="<%$ Resources: , author %>"></asp:Label>
            <asp:Label ID="lblAuthorContent" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label ID="lblPrice" runat="server" Text="<%$ Resources: , price %>"></asp:Label>
            <asp:Label ID="lblPriceContent" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblStock" runat="server" Text="<%$ Resources: , availableStock %>"></asp:Label>
            <asp:Label ID="lblStockContent" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label ID="lblGenre" runat="server" Text="<%$ Resources: , genre %>"></asp:Label>
            <asp:Label ID="lblGenreContent" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label ID="lblReleaseDate" runat="server" Text="<%$ Resources: , releaseDate %>"></asp:Label>
            <asp:Label ID="lblReleaseDateContent" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label ID="lblIsbnCode" runat="server" Text="<%$ Resources: , isbnCode %>"></asp:Label>
            <asp:Label ID="lblIsbnCodeContent" runat="server"></asp:Label>
            <br />
            <br />
            <br />
            <asp:Label ID="lblQuantity" runat="server" Text="<%$ Resources: , quantity %>"></asp:Label>
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
            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="Comment_Click" Text="<%$ Resources: , comments %>" />
            <br />
            <br />
            <asp:Button ID="btnAddToShoppingCart" runat="server" Text="<%$ Resources: , addToShoppingCart %>" OnClick="btnAddToShoppingCart_Click" />
            <br />
            <br />
        </div>
    </form>




</asp:Content>
