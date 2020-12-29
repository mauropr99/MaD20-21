<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ComputerDetailsView.aspx.cs" Inherits="Web.Pages.Product.ComputerDetailsView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

    <form id="form1" runat="server">
        <br />
        <br />
        <asp:Label ID="lblComputerName" runat="server" Text="<%$ Resources: , computerName %>"></asp:Label>
        <asp:Label ID="lblComputerNameContent" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblBrand" runat="server" Text="<%$ Resources: , brand %>"></asp:Label>
        <asp:Label ID="lblBrandContent" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblPrice" runat="server" Text="<%$ Resources: , price %>"></asp:Label>
        <asp:Label ID="lblPriceContent" runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblStock" runat="server" Text="<%$ Resources: , availableStock %>"></asp:Label>
        <asp:Label ID="lblStockContent" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblProcessor" runat="server" Text="<%$ Resources: , processor %>"></asp:Label>
        <asp:Label ID="lblProcessorContent" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblReleaseDate" runat="server" Text="<%$ Resources: , releaseDate %>"></asp:Label>
        <asp:Label ID="lblReleaseDateContent" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblOperatingSystem" runat="server" Text="<%$ Resources: , operatingSystem %>"></asp:Label>
        <asp:Label ID="lblOperatingSystemContent" runat="server"></asp:Label>
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
        <br />
        <asp:Button ID="btnAddToShoppingCart" runat="server" Text="<%$ Resources: , addToShoppingCart %>" />
        <br />
        <br />
        
    </form>




</asp:Content>

