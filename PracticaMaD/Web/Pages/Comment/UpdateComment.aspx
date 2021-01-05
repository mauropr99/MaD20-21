<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="UpdateComment.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment.UpdateComment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

    <div id="form">
        <form id="form1" runat="server">
            <div class="field">
                <asp:Label ID="lblCommentContent" CssClass="label" runat="server" Text="<%$ Resources: , lblCommentContent %>"></asp:Label>
                <span class="entry">
                    <asp:TextBox ID="txtCommentContent" Width="300px" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
                    <asp:RequiredFieldValidator ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtCommentContent"
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
            <asp:Button ID="btnUpdateComment" runat="server" Text="<%$ Resources: , updateCommentButton %>" OnClick="btnUpdateComment_Click" />
            <br />
            
        </form>
    </div>

</asp:Content>
