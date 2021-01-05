<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ComputersDetailsView.aspx.cs" Inherits="Web.Pages.Product.ComputersDetailsView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

    <form id="form1" runat="server">
        <br />
        <asp:LinkButton ID="lnkbutton" runat="server" OnClick="BtnBackToPreviousPage_Click" Text="<%$ Resources:Common, back %>" />
        <br />
        <div class="field">
            <asp:Label ID="lblComputerName" CssClass="label"  runat="server" Text="<%$ Resources: , computerName %>"></asp:Label>
            <span class="entry">
                <asp:Label ID="lblComputerNameContent" CssClass="labelResult" text-align="left" Font-Bold="false" runat="server"></asp:Label>
            </span>
        </div>
        <br />
        <br />
        <div class="field">
            <asp:Label ID="lblBrand" CssClass="label"  runat="server" Text="<%$ Resources: , brand %>"></asp:Label>
            <span class="entry">
                <asp:Label ID="lblBrandContent" CssClass="labelResult"  runat="server"></asp:Label>
            </span>
        </div>
        <br />
        <br />
        <div class="field">
            <asp:Label ID="lblPrice" CssClass="label"  runat="server" Text="<%$ Resources: , price %>"></asp:Label>
            <span class="entry">
                <asp:Label ID="lblPriceContent" CssClass="labelResult" Font-Bold="false" runat="server"></asp:Label>
            </span>
        </div>
        <br />
        <br />
        <div class="field">
            <asp:Label ID="lblStock" CssClass="label"  runat="server" Text="<%$ Resources: , availableStock %>"></asp:Label>
            <span class="entry">
                <asp:Label ID="lblStockContent" CssClass="labelResult" Font-Bold="false" runat="server"></asp:Label>
            </span>
        </div>
        <br />
        <br />
        <div class="field">
            <asp:Label ID="lblProcessor" CssClass="label"  runat="server" Text="<%$ Resources: , processor %>"></asp:Label>
            <span class="entry">
                <asp:Label ID="lblProcessorContent" CssClass="labelResult" Font-Bold="false" runat="server"></asp:Label>
            </span>
        </div>
        <br />
        <br />
        <div class="field">
            <asp:Label ID="lblReleaseDate" CssClass="label"  runat="server" Text="<%$ Resources: , releaseDate %>"></asp:Label>
            <span class="entry">
                <asp:Label ID="lblReleaseDateContent" CssClass="labelResult" Font-Bold="false" runat="server"></asp:Label>
            </span>
        </div>
        <br />
        <br />
        <div class="field">
            <asp:Label ID="lblOperatingSystem" CssClass="label"  runat="server" Text="<%$ Resources: , operatingSystem %>"></asp:Label>
            <span class="entry">
                <asp:Label ID="lblOperatingSystemContent" CssClass="labelResult" Font-Bold="false" runat="server"></asp:Label>
            </span>
        </div>
        <br />
        <br />
        <br />
        <div class="field">
            <asp:Label ID="lblQuantity" CssClass="label"  runat="server" Text="<%$ Resources: , quantity %>"></asp:Label>
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
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="Computer_Click" Text="<%$ Resources: , comments %>" />
        <br />
        <br />
        <asp:Button ID="btnAddToShoppingCart" runat="server" Text="<%$ Resources: , addToShoppingCart %>" CommandName="AddToCart" OnClick="btnAddToShoppingCart_Click"/>
        <br />
        <br />

    </form>




</asp:Content>

