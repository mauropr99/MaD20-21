<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="CommentList.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment.CommentList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">

        <asp:DataList ID="DataListComments" runat="server" CellPadding="4" ForeColor="#333333">
            <AlternatingItemStyle BackColor="White" />
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <FooterTemplate>
                <asp:Label ID="lblLogin" runat="server"></asp:Label>
                <asp:Label ID="lblDate" runat="server"></asp:Label>
                <asp:Label ID="lblText" runat="server"></asp:Label>
            </FooterTemplate>
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderTemplate>
                <asp:Label ID="lblLabel" runat="server"></asp:Label>
            </HeaderTemplate>
            <ItemStyle BackColor="#FFFBD6" ForeColor="#333333" />

            <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        </asp:DataList>
        <br />
        <br />
                <div class="previousNextLinks">
            <span class="previousLink">
                <asp:HyperLink ID="lnkPrevious" Text="<%$ Resources:Common, Previous %>" runat="server"
                    Visible="False"></asp:HyperLink>
            </span><span class="nextLink">
                <asp:HyperLink ID="lnkNext" Text="<%$ Resources:Common, Next %>" runat="server" Visible="False"></asp:HyperLink>
            </span>
        </div>
    </form>
</asp:Content>
