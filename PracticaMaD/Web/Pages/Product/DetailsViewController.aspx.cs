using System;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Pages.Product
{
    public partial class DetailsViewController : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string categoryName, productId;
            try
            {
                productId = Request.Params.Get("productId");
                categoryName = Request.Params.Get("categoryName");

                switch (categoryName)
                {
                    case "Books":
                        Response.Redirect("~/Pages/Product/BookDetailsView.aspx?productId=" + productId);
                        break;
                    case "Computers":
                        Response.Redirect("~/Pages/Product/ComputerDetailsView.aspx?productId=" + productId);
                        break;
                    default:
                        break;
                }

               

            }
            catch (ArgumentNullException)
            {
            }
        }
    }
}