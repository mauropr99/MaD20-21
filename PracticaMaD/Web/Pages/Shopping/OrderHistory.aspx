<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="OrderHistory.aspx.cs" Inherits="Web.Pages.Shopping.OrderHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

    <div id="form">
        <form id="form1" runat="server">
            <br />
            <br />
            <br />
            <asp:GridView ID="GridOrderHistory" CssClass="estandar" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                AutoGenerateColumns="False">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:HyperLinkField DataTextField="Description" HeaderText="<%$ Resources: , description %>" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="OrderHistoryDetails.aspx?orderId={0}" NavigateUrl="~/Pages/Shopping/OrderHistoryDetails.aspx" />
                    <asp:BoundField DataField="Date" HeaderText="<%$ Resources: , orderDate %>" />
                    <asp:BoundField DataField="TotalPrice" HeaderText="<%$ Resources: , totalPrice %>" />
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
