using System;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Errors
{
    public partial class OutOfStock : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string productName = Request.Params.Get("productName");
            string stock = Request.Params.Get("stock");
            string orderedStock = Request.Params.Get("OrderedStock");
            lblErrorTitle.Text = GetLocalResourceObject("OutOfStock").ToString() + " " + productName + " "
                                + GetLocalResourceObject("productStock").ToString() + " " + stock + " "
                                + GetLocalResourceObject("orderedStock").ToString() + " " + orderedStock;
        }
    }
}