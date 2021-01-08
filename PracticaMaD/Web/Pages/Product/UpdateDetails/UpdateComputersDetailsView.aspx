<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="UpdateComputersDetailsView.aspx.cs" Inherits="Web.Pages.Product.UpdateComputersDetailsView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

        <br />
        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="BtnBackToPreviousPage_Click" Text="<%$ Resources:Common, back %>" />
        <br />
        <div class="field">
            <asp:Label ID="Label1" CssClass="label"  runat="server" Text="<%$ Resources: , computerName %>"></asp:Label>
            <span class="entry">
                <asp:TextBox ID="txtComputerNameContent" runat="server"></asp:TextBox>
            </span>
        </div>
        <br />
        <br />
        <div class="field">
            <asp:Label ID="Label2" CssClass="label"  runat="server" Text="<%$ Resources: , brand %>"></asp:Label>
            <span class="entry">
                <asp:TextBox ID="txtBrandContent" runat="server"></asp:TextBox>
            </span>
        </div>
        <br />
        <br />
        <div class="field">
            <asp:Label ID="Label3" CssClass="label"  runat="server" Text="<%$ Resources: , price %>"></asp:Label>
            <span class="entry">
                <asp:TextBox ID="txtPriceContent" runat="server"></asp:TextBox>
            </span>
        </div>
        <br />
        <br />
        <div class="field">
            <asp:Label ID="Label4" CssClass="label"  runat="server" Text="<%$ Resources: , availableStock %>"></asp:Label>
            <span class="entry">
                <asp:TextBox ID="txtStockContent" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator runat="server" ForeColor="Red" ControlToValidate="txtStockContent"
                    ValidationExpression="^(?!-1+$)[0-9]+$"
                    ErrorMessage="<%$ Resources: , ControlStock %>" />
            </span>
        </div>
        <br />
        <br />
        <div class="field">
            <asp:Label ID="Label5" CssClass="label"  runat="server" Text="<%$ Resources: , processor %>"></asp:Label>
            <span class="entry">
                <asp:TextBox ID="txtProcessorContent" runat="server"></asp:TextBox>
            </span>
        </div>
        <br />
        <br />
        <div class="field">
            <asp:Label ID="Label6" CssClass="label"  runat="server" Text="<%$ Resources: , operatingSystem %>"></asp:Label>
            <span class="entry">
                        <asp:TextBox ID="txtOperatingSystemContent" runat="server"></asp:TextBox>
            </span>
        </div>
        <br />
        <br />
        <asp:LinkButton ID="linkViewComment" runat="server" OnClick="Computer_Click" Text="<%$ Resources: , comments %>" />&nbsp &nbsp &nbsp
        <asp:LinkButton ID="btnNewComment" runat="server" Text="<%$ Resources:, newComment%>" OnClick="BtnNewComment_Click" />
        <br />
        <br />

        <asp:Button ID="Submit" runat="server" Text="<%$ Resources: , submit %>" OnClick="Submit_Click" />


</asp:Content>

