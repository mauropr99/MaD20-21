<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="UpdateComputersDetailsView.aspx.cs" Inherits="Web.Pages.Product.UpdateComputersDetailsView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

       <form id="form1" runat="server">
        <br />
        <br />
        <asp:Label ID="lblComputerName" runat="server" Text="<%$ Resources: , computerName %>"></asp:Label>
         <asp:TextBox ID="lblComputerNameContent" runat="server"></asp:TextBox>
           <br />
        <br />
        <asp:Label ID="lblBrand" runat="server" Text="<%$ Resources: , brand %>"></asp:Label>
         <asp:TextBox ID="lblBrandContent" runat="server"></asp:TextBox>
           <br />
           <br />
        <asp:Label ID="lblPrice" runat="server" Text="<%$ Resources: , price %>"></asp:Label>
         <asp:TextBox ID="lblPriceContent" runat="server"></asp:TextBox>
           <br />
        <br />
        <asp:Label ID="lblStock" runat="server" Text="<%$ Resources: , availableStock %>"></asp:Label>
         <asp:TextBox ID="lblStockContent" runat="server"></asp:TextBox>
           <br />
        <br />
        <asp:Label ID="lblProcessor" runat="server" Text="<%$ Resources: , processor %>"></asp:Label>
         <asp:TextBox ID="lblProcessorContent" runat="server"></asp:TextBox>
           <br />
        <br />
        <asp:Label ID="lblOperatingSystem" runat="server" Text="<%$ Resources: , operatingSystem %>"></asp:Label>
         <asp:TextBox ID="lblOperatingSystemContent" runat="server"></asp:TextBox>
        <br />
        <br />

       <asp:Button ID="Submit" runat="server" Text="<%$ Resources: , submit %>" OnClick="Submit_Click"/>

    </form>


</asp:Content>

