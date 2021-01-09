<%@ Page meta:resourcekey="Page" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ProductsByLabelView.aspx.cs" Inherits="Web.Pages.Product.ProductsByLabelView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

    <div id="form">
            <br />
            <asp:Label ID="lblLabelInfo" runat="server" Text="<%$ Resources: , labelInfo %>"></asp:Label>&nbsp
            <asp:Label ID="lblLabelContent" runat="server"></asp:Label>
            <br />
            <br />
            <asp:GridView ID="GridViewCatalog"  runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                AutoGenerateColumns="False" OnRowCommand="GridViewCatalog_RowCommand" DataKeyNames="Id" CssClass="estandar" >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:HyperLinkField DataTextField="ProductName"  HeaderText="<%$ Resources: , productName %>" 
                        DataNavigateUrlFields="Id,CategoryName" NavigateUrl="~/Pages/Product/DetailsViewController.aspx" 
                        DataNavigateUrlFormatString="DetailsViewController.aspx?productId={0}&categoryName={1}"/>
                    <asp:BoundField DataField="CategoryName"   HeaderText="<%$ Resources: , category %>" />
                    <asp:BoundField DataField="ReleaseDate"  HeaderText="<%$ Resources: , releaseDate %>" />
                    <asp:BoundField DataField="Price" HeaderText="<%$ Resources: , price %>" />
                    <asp:ButtonField Text="<%$ Resources: , addItem %>" CommandName="AddToCart"/>
                    <asp:BoundField DataField="Id" Visible="false"/>
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
            <div class="previousNextLinks">
                <span class="previousLink">
                    <asp:HyperLink ID="lnkPrevious" Text="<%$ Resources:Common, Previous %>" runat="server"
                        Visible="False"></asp:HyperLink>
                </span><span class="nextLink">
                    <asp:HyperLink ID="lnkNext" Text="<%$ Resources:Common, Next %>" runat="server" Visible="False"></asp:HyperLink>
                </span>
            </div>
            <br />
            <br />
    </div>

</asp:Content>
