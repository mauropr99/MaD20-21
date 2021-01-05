<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="CommentAdd.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment.CommentAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

    <div id="form">
        <form id="form1" runat="server">
            <div class="field">
                <asp:Label ID="lblCommentAdd" CssClass="label" runat="server" Text="<%$ Resources: , lblCommentAdd %>"></asp:Label>
                <span class="entry">
                    <asp:TextBox ID="txtCommentAdd" Width="300px" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
                    <asp:RequiredFieldValidator ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtCommentAdd"
                        runat="server" Display="Dynamic" />
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
            
        </form>
    </div>

</asp:Content>
