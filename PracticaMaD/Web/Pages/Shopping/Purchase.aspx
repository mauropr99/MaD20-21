<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    Codebehind="Purchase.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping.Purchase"
    meta:resourcekey="Page" %>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <div id="form">
        <form id="PurchaseForm" method="POST" runat="server">
             
            <div class="field">
                <asp:Label ID="lclCreditCards" runat="server" Text="<%$ Resources: , lclCreditCards %>"></asp:Label>
                <span class="entry">
                    <asp:DropDownList ID="DropDownCreditCardsList" runat="server" AutoPostBack="True" 
                    Width="130px">
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                <asp:HyperLink ID="lnkAddNewCreditCard" runat="server" Text="<%$ Resources: , lnkAddCreditCard %>" 
                    NavigateUrl="~/Pages/CreditCardOperations/CreditCardAdd.aspx"></asp:HyperLink>
                </span>
            </div>

            <div class="field">
                <asp:Label ID="lclPostalAddress1" runat="server" Text="<%$ Resources: , lclPostalAddress %>"></asp:Label>
                <span class="entry">
                    <asp:TextBox ID="txtPostalAddress" runat="server" Width="100" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPostalAddress" runat="server"
                        ControlToValidate="txtPostalAddress" Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/></span>
            </div>

            <div class="field">
                <asp:Label ID="lclDeliveryDescription1" runat="server" Text="<%$ Resources: , lclDeliveryDescription %>"></asp:Label>
                <span class="entry">
                    <asp:TextBox ID="txtDeliveryDescription" mode="multiline" runat="server" Width="100" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDeliveryDescription" runat="server"
                        ControlToValidate="txtDeliveryDescription" Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/></span>
            </div>
            
            <div class="button">
                <asp:Button ID="btnPurchase" runat="server" OnClick="BtnPurchaseClick" Text="<%$ Resources: , btnPurchase %>"/>
            </div>
        </form>
    </div>
</asp:Content>
