﻿<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="CreditCardAdd.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.CreditCardOperations.CreditCardAdd"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

    <div id="form">
        <div class="field">
            <asp:Label ID="lblCreditCardOwner" CssClass="label" runat="server" Text="<%$ Resources: , creditCardOwner %>"></asp:Label>
            <span class="entry">
                <asp:TextBox ID="txtCreditCardOwner" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtCreditCardOwner"
                    runat="server" Display="Dynamic" />
            </span>
        </div>
        <div class="field">
            <asp:Label ID="lblCreditCardNumber" CssClass="label" runat="server" Text="<%$ Resources: , creditCardNumber %>"></asp:Label>
            <span class="entry">
                <asp:TextBox ID="txtCreditCardNumber" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtCreditCardNumber"
                    runat="server" Display="Dynamic" />
                <asp:RegularExpressionValidator runat="server" ForeColor="Red" ControlToValidate="txtCreditCardNumber"
                    ValidationExpression="^[0-9]{16}$"
                    ErrorMessage="<%$ Resources: , invalidLength %>" />
            </span>
        </div>
        <div class="field">
            <asp:Label ID="lblCreditCardCvv" CssClass="label" runat="server" Text="<%$ Resources: , creditCardCvv %>"></asp:Label>
            <span class="entry">
                <asp:TextBox ID="txtCreditCardCvv" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtCreditCardCvv"
                    runat="server" Display="Dynamic" />
            </span>
        </div>
        <div class="field">
            <asp:Label ID="lblCreditCardType" CssClass="label" runat="server" Text="<%$ Resources: , creditCardType %>"></asp:Label>
            <span class="entry">
                <asp:DropDownList ID="DropDownCreditCardTypeList" runat="server">
                    <asp:ListItem Value="debit" Text="<%$ Resources: , debit %>"></asp:ListItem>
                    <asp:ListItem Value="credit" Text="<%$ Resources: , credit %>"></asp:ListItem>
                </asp:DropDownList>
            </span>
        </div>
        <div class="field">
            <asp:Label ID="lblExpirationDate" CssClass="label" runat="server" Text="<%$ Resources: , expirationDate %>"></asp:Label>
            <span class="entry">
                <asp:TextBox ID="txtExpirationDate" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtExpirationDate"
                    runat="server" Display="Dynamic" />
                <asp:RegularExpressionValidator runat="server" ForeColor="Red" ControlToValidate="txtExpirationDate"
                    ValidationExpression="<%$ Resources: , validationDateExpression %>"
                    ErrorMessage="<%$ Resources: , invalidDateFormat %>" />
            </span>
        </div>
        <br />
        <br />
        <asp:Button ID="btnAddCreditCard" runat="server" Text="<%$ Resources: , addCreditCard %>" OnClick="BtnAddCreditCard_Click" />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
</asp:Content>
