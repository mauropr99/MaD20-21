using System;
using Es.Udc.DotNet.ModelUtil.IoC;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment
{
    public partial class CommentAdd : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void BtnAddComment_Click(object sender, EventArgs e)
        {
            UserSession userSession =
                    (UserSession)Context.Session[SessionManager.USER_SESSION_ATTRIBUTE];

            if (userSession == null) Response.Redirect(Response.ApplyAppPathModifier("~/Pages/User/Authentication.aspx"));


            //1 Obtener contexto de inyección de dependencias

            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];

            //2 Obtener el servicio

            ICommentService commentService = iocManager.Resolve<ICommentService>();


            //3 Llamar al caso de uso (lectura de parámetros y actualización de la vista)

            String comment = txtCommentAdd.Text;

            long productId = long.Parse(Request.Params.Get("productId"));

            List<String> labels = new List<String>();

            commentService.NewComment(userSession.UserId, productId, comment, labels);

            Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Comment/CommentList.aspx?productId=" + Request.Params.Get("productId") + "&categoryName=" + Request.Params.Get("categoryName")));

        }

    }
}