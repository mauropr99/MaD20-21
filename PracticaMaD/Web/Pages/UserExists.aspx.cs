using System;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages
{
    public partial class UserExists : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString();
            }
        }

        protected void BtnBackToPreviousPage_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);
        }
        /// <summary>
        /// Handles the Click event of the btnUserExists control.
        /// </summary>
        /// <param name="sender"> The source of the event. </param>
        /// <param name="e"> The <see cref="EventArgs"/> instance containing the event data. </param>
        protected void BtnUserExists_Click(object sender, EventArgs e)
        {
            // 1) Obtener contexto de Inyección de Dependencias

            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];

            // 2) Obtención del Servicio

            IUserService userService = iocManager.Resolve<IUserService>();

            // 3) Llamada al caso de uso (lectura de parámetros y actualización vista)

            lblUserExists.Visible = false;
            lblUserNotExists.Visible = false;

            string loginName =
                txtUserName.Text;

            bool userExists = userService.UserExists(loginName);

            if (userExists)

                lblUserExists.Visible = true;
            else

                lblUserNotExists.Visible = true;
        }

        protected void btnUserExists_Click(object sender, EventArgs e)
        {
            // 1) Obtener contexto de Inyección de Dependencias

            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];

            // 2) Obtención del Servicio

            IUserService userService = iocManager.Resolve<IUserService>();

            // 3) Llamada al caso de uso (lectura de parámetros y actualización vista)

            lblUserExists.Visible = false;
            lblUserNotExists.Visible = false;

            string loginName =
                txtUserName.Text;

            bool userExists = userService.UserExists(loginName);

            if (userExists)

                lblUserExists.Visible = true;
            else

                lblUserNotExists.Visible = true;
        }
    }
}