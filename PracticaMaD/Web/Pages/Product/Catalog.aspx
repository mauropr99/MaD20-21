<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="Catalog.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Product.Catalog"
    %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="form1" runat="server">
            <br />
            <br />
            <asp:TextBox ID="txtProductName" runat="server"></asp:TextBox>
            <asp:Button ID="BtnViewCatalog" runat="server" Text="<%$ Resources: , search %>" OnClick="BtnViewCatalog_Click" />
            <br />
            <br />
            <asp:Label ID="lblCategory" runat="server" Text="<%$ Resources: , category %>"></asp:Label>
            <asp:DropDownList ID="DropDownCategoryList" runat="server">
            </asp:DropDownList>
            <br />
            <br />
            <br />
            <asp:GridView ID="GridViewCatalog"  runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                AutoGenerateColumns="False" OnRowCommand="GridViewCatalog_RowCommand" DataKeyNames="Id" >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:HyperLinkField DataTextField="ProductName" HeaderText="<%$ Resources: , productName %>" 
                        DataNavigateUrlFields="Id,CategoryName" NavigateUrl="~/Pages/Product/DetailsViewController.aspx" 
                        DataNavigateUrlFormatString="DetailsViewController.aspx?productId={0}&categoryName={1}"/>
                    <asp:BoundField DataField="CategoryName" HeaderText="<%$ Resources: , category %>" />
                    <asp:BoundField DataField="ReleaseDate" HeaderText="<%$ Resources: , releaseDate %>" />
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
        </form>
    </div>
</asp:Content>
