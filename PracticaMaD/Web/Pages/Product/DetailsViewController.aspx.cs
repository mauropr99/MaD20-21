using System;
using System.Web;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Web.Pages.Product
{
    public partial class DetailsViewController : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string categoryName, productId;

            IUserService userService;

            IIoCManager iocManager =
                (IIoCManager)HttpContext.Current.Application["managerIoC"];

            userService = iocManager.Resolve<IUserService>();

            UserSession userSession =
                (UserSession)Context.Session[SessionManager.USER_SESSION_ATTRIBUTE];

            string userRole = "";
            try
            {
                userRole = userService.GetRolByUserId(userSession.UserId);
            }
            catch
            {
            }

            try
            {
                productId = Request.Params.Get("productId");
                categoryName = Request.Params.Get("categoryName");

                if (userRole == "admin")
                {
                    Response.Redirect("~/Pages/Product/UpdateDetails/Update" + categoryName + "DetailsView.aspx?productId=" + productId + "&categoryName=" + categoryName);
                }
                else
                {
                    Response.Redirect("~/Pages/Product/ViewDetails/" + categoryName + "DetailsView.aspx?productId=" + productId + "&categoryName="+categoryName);
                }
            }
            catch (ArgumentNullException)
            {
            }
        }
    }
}