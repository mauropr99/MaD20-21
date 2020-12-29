<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="CreditCardAdd.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.CreditCardOperations.CreditCardAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
        <asp:Label ID="lblCreditCardOwner" runat="server" Text="<%$ Resources: , creditCardOwner %>"></asp:Label>
        <asp:TextBox ID="txtCreditCardOwner" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ErrorMessage="Required" ForeColor = "Red" ControlToValidate="txtCreditCardOwner"
            runat="server" Display = "Dynamic" />
        <br />
        <br />
        <asp:Label ID="lblCreditCardNumber" runat="server" Text="<%$ Resources: , creditCardNumber %>"></asp:Label>
        <asp:TextBox ID="txtCreditCardNumber" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ErrorMessage="Required" ForeColor = "Red" ControlToValidate="txtCreditCardNumber"
            runat="server" Display = "Dynamic" />
        <br />
        <br />
        <asp:Label ID="lblCreditCardCvv" runat="server" Text="<%$ Resources: , creditCardCvv %>"></asp:Label>
        <asp:TextBox ID="txtCreditCardCvv" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ErrorMessage="Required" ForeColor = "Red" ControlToValidate="txtCreditCardCvv"
            runat="server" Display = "Dynamic" />
        <br />
        <br />
        <asp:Label ID="lblCreditCardType" runat="server" Text="<%$ Resources: , creditCardType %>"></asp:Label>
        <asp:DropDownList ID="DropDownCreditCardTypeList" runat="server" >
            <asp:ListItem Selected="True" Value="debit" Text="<%$ Resources: , debit %>"></asp:ListItem>
            <asp:ListItem Value="credit" Text="<%$ Resources: , credit %>"></asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="lblExpirationDate" runat="server" Text="<%$ Resources: , expirationDate %>"></asp:Label>
        <asp:TextBox ID="txtExpirationDate" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ErrorMessage="Required" ForeColor = "Red" ControlToValidate="txtExpirationDate"
            runat="server" Display = "Dynamic" />
        <asp:RegularExpressionValidator runat="server" ForeColor = "Red" ControlToValidate="txtExpirationDate"
        ValidationExpression="<%$ Resources: , validationDateExpression %>"
        ErrorMessage="Invalid date format."/>
        <br />
        <br />
        <asp:Button ID="btnAddCreditCard" runat="server" Text="<%$ Resources: , addCreditCard %>" OnClick="btnAddCreditCard_Click" />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </form>
</asp:Content>
