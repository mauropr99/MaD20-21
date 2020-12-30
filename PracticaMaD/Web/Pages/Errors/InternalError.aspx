<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="InternalError.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Errors.InternalError"
    meta:resourcekey="Page" %>

<asp:Content ID="content" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">

    <br />
    <br />
    <asp:Label ID="lblErrorTitle" runat="server"
        meta:resourcekey="lblErrorTitle" />
    <br />
    <br />
    <asp:Label ID="lblRetryLater" runat="server"
        meta:resourcekey="lblRetryLater" />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />

    <asp:Localize ID="lclContent" runat="server" meta:resourcekey="lclContent" />
</asp:Content>