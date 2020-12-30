<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" 
    CodeBehind="ShoppingCart.aspx.cs" Inherits="Web.Pages.Shopping.ShoppingCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="form1" runat="server">
         <br />
            <br />
            <asp:GridView ID="GridViewCart"  runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                AutoGenerateColumns="False" onrowcommand="GridViewCart_RowCommand" DataKeyNames="Product_Id" >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="Product_Name" HeaderText="<%$ Resources: , productName %>" />
                    <asp:BoundField DataField="Quantity" HeaderText="<%$ Resources: , quantity %>" />
                    <asp:BoundField DataField="Price" HeaderText="<%$ Resources: , price %>" />
                    <asp:ButtonField ButtonType="Button" CommandName="AddItem" Text="+" />
                    <asp:ButtonField ButtonType="Button" CommandName="RemoveItem" Text="-" />
                     <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="Gift" runat="server" CommandName="CheckGift"  headertext="<%$ Resources: , gift %>"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:BoundField DataField="Product_Id"/>
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
          </form>
    </div>
</asp:Content>
