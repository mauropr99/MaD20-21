using System;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Errors
{
    public partial class OutOfStock : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String productName = Request.Params.Get("productName");
            String stock = Request.Params.Get("stock");
            String orderedStock = Request.Params.Get("OrderedStock");
            lblErrorTitle.Text = GetLocalResourceObject("OutOfStock").ToString() + " " + productName + " " 
                                + GetLocalResourceObject("productStock").ToString() + " " + stock + " " 
                                + GetLocalResourceObject("orderedStock").ToString() + " " + orderedStock ;
        }
    }
}