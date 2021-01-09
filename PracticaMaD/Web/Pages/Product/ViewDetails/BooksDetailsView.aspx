<%@ Page meta:resourcekey="Page" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="BooksDetailsView.aspx.cs" Inherits="Web.Pages.Product.BooksDetailsView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

        <div>
            <br />
            <asp:LinkButton ID="lnkbutton" runat="server" OnClick="BtnBackToPreviousPage_Click" Text="<%$ Resources:Common, back %>" />
            <br />
            <br />
            <div class="field">
                <asp:Label ID="lblTitle" CssClass="label" runat="server" Text="<%$ Resources:, title %>" Font-Bold="True"></asp:Label>
                <span class="entry">
                    <asp:Label ID="lblTitleContent" CssClass="labelResult" runat="server"></asp:Label>
                </span>
            </div>
            <br />
            <br />
            <div class="field">
                <asp:Label ID="lblAuthor" CssClass="label"  Font-Bold="True" runat="server" Text="<%$ Resources: , author %>"></asp:Label>
                <span class="entry">
                    <asp:Label ID="lblAuthorContent"  CssClass="labelResult" runat="server"></asp:Label>
                </span>
            </div>
            <br />
            <br />
            <div class="field">
                <asp:Label ID="lblPrice" CssClass="label"  Font-Bold="True" runat="server" Text="<%$ Resources: , price %>"></asp:Label>
                <span class="entry">
                    <asp:Label ID="lblPriceContent"  CssClass="labelResult" runat="server"></asp:Label>
                </span>
            </div>
            <br />
            <br />
            <div class="field">
                <asp:Label ID="lblStock" CssClass="label"  Font-Bold="True" runat="server" Text="<%$ Resources: , availableStock %>"></asp:Label>
                <span class="entry">
                    <asp:Label ID="lblStockContent"  CssClass="labelResult" runat="server"></asp:Label>
                </span>
            </div>
            <br />
            <br />
            <div class="field">
                <asp:Label ID="lblGenre" CssClass="label" Font-Bold="True" runat="server" Text="<%$ Resources: , genre %>"></asp:Label>
                <span class="entry">
                    <asp:Label ID="lblGenreContent"  CssClass="labelResult" runat="server"></asp:Label>
                </span>
            </div>
            <br />
            <br />
            <div class="field">
                <asp:Label ID="lblReleaseDate" CssClass="label" Font-Bold="True" runat="server" Text="<%$ Resources: , releaseDate %>"></asp:Label>
                <span class="entry">
                    <asp:Label ID="lblReleaseDateContent"  CssClass="labelResult" runat="server"></asp:Label>
                </span>
            </div>
            <br />
            <br />
            <div class="field">
                <asp:Label ID="lblIsbnCode" CssClass="label" Font-Bold="True" runat="server" Text="<%$ Resources: , isbnCode %>"></asp:Label>
                <span class="entry">
                    <asp:Label ID="lblIsbnCodeContent"  CssClass="labelResult" runat="server"></asp:Label>
                </span>
            </div>
            <br />
            <br />
            <br />
            <div class="field">
                <asp:Label ID="lblQuantity" CssClass="label" Font-Bold="True" runat="server" Text="<%$ Resources: , quantity %>"></asp:Label>
                <span class="entry">
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
                </span>
            </div>
            <br />
            <br />
            <asp:LinkButton ID="linkViewComment" runat="server" OnClick="Book_Click" Text="<%$ Resources: , comments %>" />&nbsp &nbsp &nbsp
            <asp:LinkButton ID="btnNewComment" runat="server" Text="<%$ Resources:, newComment%>" OnClick="BtnNewComment_Click" />
            <br />
            <br />
            <asp:Button ID="btnAddToShoppingCart" runat="server" Text="<%$ Resources: , addToShoppingCart %>" OnClick="btnAddToShoppingCart_Click" />
            <br />
            <br />
        </div>

</asp:Content>
