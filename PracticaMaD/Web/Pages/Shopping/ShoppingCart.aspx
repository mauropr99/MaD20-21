<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" 
    CodeBehind="ShoppingCart.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping.ShoppingCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="form1" runat="server">
         <br />
            <br />
            &nbsp;<asp:Image CssClass="img" ID="imgEmptyCart" runat="server"
                               ImageUrl="~/Img/vagoneta.png" Height="258px" Width="278px" />
             <br />
             <br />
            <asp:Label ID="txtEmptyCart" Text="<%$ Resources:, empty %>" runat="server" text-align="center" Font-Size="Large"/>

            <asp:GridView ID="GridViewCart"  runat="server" CellPadding="8" ForeColor="#333333" GridLines="None"
                AutoGenerateColumns="False" OnRowCommand="GridViewCart_RowCommand" DataKeyNames="Product_Id" >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                   <asp:HyperLinkField DataTextField="Product_Name" HeaderText="<%$ Resources: , productName %>" 
                        DataNavigateUrlFields="Product_Id,CategoryName" 
                        DataNavigateUrlFormatString="~/Pages/Product/DetailsViewController.aspx?productId={0}&categoryName={1}"/>
                    <asp:BoundField DataField="Quantity" HeaderText="<%$ Resources: , quantity %>" />
                    <asp:BoundField DataField="Price" HeaderText="<%$ Resources: , price %>" />
                    <asp:ButtonField ButtonType="Button" CommandName="AddItem" Text="+" />
                    <asp:ButtonField ButtonType="Button" CommandName="RemoveItem" Text="-" />
                    <asp:ButtonField ButtonType="Button" CommandName="CheckGift" Text="<%$ Resources: , gift %>" />
                    <asp:TemplateField headertext="<%$ Resources: , gift %>">
                    <ItemTemplate>
                      <asp:CheckBox ID="gift" Checked="true"  runat="server" />
                    </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:CheckBoxField runat="server"  headertext="<%$ Resources: , gift %>"/>--%>
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
            Subtotal = <asp:Label ID="Subtotal" runat="server" text-align="center" />
            <br />
            <br />
            <asp:Button ID="btn_BuyProducts" runat="server" OnClick="Btn_BuyProducts" Text="<%$ Resources: , btn_BuyProducts %>" />
          </form>
    </div>
</asp:Content>
