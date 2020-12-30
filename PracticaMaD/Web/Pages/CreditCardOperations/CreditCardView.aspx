﻿<%@  Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" 
    CodeBehind="CreditCardView.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.CreditCardOperations.CreditCardView"
    meta:resourcekey="Page"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        <br />
        <br />
        <br />
        <asp:GridView ID="GridViewCreditCards" runat="server" AutoGenerateColumns="False" CellPadding="8" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridViewCreditCards_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="OwnerName" HeaderText="<%$ Resources: , creditCardOwner %>" />
                <asp:BoundField DataField="AnonymizedCreditCardNumber" HeaderText="<%$ Resources: , creditCardNumber %>" />
                <asp:BoundField DataField="CreditCardType" HeaderText="<%$ Resources: , creditCardType %>" />
                <asp:BoundField DataField="ExpirationDate" HeaderText="<%$ Resources: , creditCardExpirationDate %>" />
                <asp:TemplateField HeaderText="<%$ Resources: , isDefaultCreditCard %>">
                <ItemTemplate>
                    <asp:CheckBox ID="chkDefault" runat="server"/>
                </ItemTemplate>
            </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        <br />
        <asp:Button ID="btnAddNewCreditCard" runat="server" Text="<%$ Resources: , btnAddCreditCard %>" OnClick="btnAddNewCreditCard_Click" />
        <br />
        <br />
    </form>


   


</asp:Content>