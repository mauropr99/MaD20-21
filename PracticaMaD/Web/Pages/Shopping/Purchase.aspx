<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    Codebehind="Purchase.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping.Purchase"
    meta:resourcekey="Page" %>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <div id="form">
        <form id="PurchaseForm" method="POST" runat="server">
            
            <div class="field">
                <span class="label"><asp:Localize ID="lclCreditCards" runat="server" meta:resourcekey="lclCreditCards" /></span><span class="entry">
                    <asp:DropDownList ID="creditCards" runat="server" AutoPostBack="True" 
                    Width="100px" onselectedindexchanged="CreditCardsSelectedIndexChanged">
                    </asp:DropDownList></span>
            </div>

            <div class="field">
                <span class="label"><asp:Localize ID="lclPostalAddress" runat="server" meta:resourcekey="lclPostalAddress" /></span><span class="entry">
                    <asp:TextBox ID="txtPostalAddress" runat="server" Width="100" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPostalAddress" runat="server"
                        ControlToValidate="txtPostalAddress" Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/></span>
            </div>

            <div class="field">
                <span class="label"><asp:Localize ID="lclDeliveryDescription" runat="server" meta:resourcekey="lclDeliveryDescription" /></span><span class="entry">
                    <asp:TextBox ID="txtDeliveryDescription" mode="multiline" runat="server" Width="100" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDeliveryDescription" runat="server"
                        ControlToValidate="txtDeliveryDescription" Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/></span>
            </div>
            
            <div class="button">
                <asp:Button ID="btnPurchase" runat="server" OnClick="BtnPurchaseClick" meta:resourcekey="btnPurchase"/>
            </div>
        </form>
    </div>
</asp:Content>
