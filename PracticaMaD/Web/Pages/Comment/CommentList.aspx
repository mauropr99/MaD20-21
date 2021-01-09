<%@ Page meta:resourcekey="Page" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="CommentList.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment.CommentList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
        <br />
        <asp:LinkButton ID="lnkbutton" runat="server" OnClick="BtnBackToPreviousPage_Click" Text="<%$ Resources:Common, back %>" />
        <br />
        <br />

        <asp:label id="txtEmptyComment" text="<%$ resources:, emptycomment %>" runat="server" text-align="center" font-size="large"/>

        <asp:GridView ID="GridViewComments" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                AutoGenerateColumns="False" OnRowCommand="GridViewComments_RowCommand" DataKeyNames="Id,UserId,labels" CssClass="estandar" >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="Login" HeaderText="<%$ Resources: , login %>" />
                    <asp:BoundField DataField="Text" ItemStyle-Width="600px" HeaderText="<%$ Resources: , comment %>" >
                        <ItemStyle Width="600px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Date" HeaderText="<%$ Resources: , date %>" />
                    <asp:ButtonField Text="<%$ Resources: , delete %>" CommandName="DeleteComment"/>
                    <asp:ButtonField Text="<%$ Resources: , edit %>" CommandName="EditComment"/>
                    <asp:BoundField DataField="Id" Visible="false"/>
                    <asp:BoundField DataField="UserId" Visible="false"/>
                    <asp:BoundField DataField="labels" Visible="false"/>
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
</asp:Content>
