<%@ Page meta:resourcekey="Page" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="CommentAdd.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment.CommentAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

    <div id="form">
        <br />
        <asp:LinkButton ID="lnkbutton" runat="server" OnClick="BtnBackToPreviousPage_Click" CausesValidation="false" Text="<%$ Resources:Common, back %>" />
        <br />
        <br />
        <div class="field">
            <asp:Label ID="lblCommentAdd" CssClass="label" runat="server" Text="<%$ Resources: , lblCommentAdd %>"></asp:Label>
            <span class="entry">
                <asp:TextBox ID="txtCommentAdd" Width="300px" CssClass="field" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
                <asp:RequiredFieldValidator ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtCommentAdd"
                    runat="server" Display="Dynamic" />
            </span>
        </div>
        <div class="field">
            <asp:Label ID="lblLabelAdd" CssClass="label" runat="server" Text="<%$ Resources: , lblLabelAdd %>"></asp:Label>
            <span class="entry">
                <asp:TextBox ID="txtLabelContent" runat="server"></asp:TextBox>&nbsp &nbsp &nbsp
                    <asp:Button ID="ButtonAddLabel" runat="server" Text="<%$ Resources: , lblLabelAdd %>" OnClick="BtnAddLabel_Click" />
                <br />
                <br />
                <asp:GridView ID="GridViewLabels" runat="server" AutoGenerateColumns="true" OnRowCommand="GridViewComments_RowCommand">
                    <Columns>
                        <asp:ButtonField Text="<%$ Resources: , delete %>" CommandName="Deletelabel" />
                    </Columns>
                </asp:GridView>
            </span>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="btnAddComment" runat="server" Text="<%$ Resources: , addCommentButton %>" OnClick="BtnAddComment_Click" />
        <br />

    </div>

</asp:Content>
