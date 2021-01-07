<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="UserExists.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.UserExists" meta:resourcekey="PageResource2" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
        <br />
        <asp:LinkButton ID="lnkbutton" runat="server" OnClick="BtnBackToPreviousPage_Click" Text="<%$ Resources:Common, back %>" />
        <br />
        <br />
        <asp:Label ID="lblUserName" runat="server" meta:resourcekey="lblUserNameResource1"></asp:Label>
        <br />
        <asp:TextBox ID="txtUserName" runat="server" meta:resourcekey="txtUserNameResource1"></asp:TextBox>
        <br />
        <asp:Label ID="lblUserExists" runat="server" meta:resourcekey="lblUserExistsResource1" Visible="False"></asp:Label>
        <br />
        <asp:Label ID="lblUserNotExists" runat="server" meta:resourcekey="lblUserNotExistsResource1" Visible="False"></asp:Label>
        <br />
        <asp:Button ID="btnUserExists" runat="server" OnClick="btnUserExists_Click" Text="<%$Resources: Common , searchButton %>" />
</asp:Content>
