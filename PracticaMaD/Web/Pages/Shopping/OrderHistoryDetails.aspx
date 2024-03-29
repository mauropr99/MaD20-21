﻿<%@ Page meta:resourcekey="Page" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="OrderHistoryDetails.aspx.cs" Inherits="Web.Pages.Shopping.OrderHistoryDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

    <br />
    <br />
    <br />
    <asp:GridView CssClass="estandar" ID="GridOrderHistoryDetails" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
        AutoGenerateColumns="False">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="Product_Name" HeaderText="<%$ Resources: , productName %>" />
            <asp:BoundField DataField="Quantity" HeaderText="<%$ Resources: , quantity %>" />
            <asp:BoundField DataField="Price" HeaderText="<%$ Resources: , price %>" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
    <br />
    <br />
    <asp:LinkButton ID="lnkbutton" runat="server" OnClick="BtnBackToPreviousPage_Click" Text="<%$ Resources:Common, back %>" />
    <br />

</asp:Content>
