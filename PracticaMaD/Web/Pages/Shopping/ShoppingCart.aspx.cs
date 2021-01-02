using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ProductService;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping
{
    public partial class ShoppingCart : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int priceCell = 2;

            IShoppingService shoppingService = SessionManager.GetShoppingService();
            List<ShoppingCartDetails> shoppingCart = shoppingService.ViewShoppingCart();

            this.GridViewCart.DataSource = shoppingCart;
            this.GridViewCart.DataBind();



            for (int i = 0; i < GridViewCart.Rows.Count; i++)
            {
                GridViewCart.Rows[i].Cells[priceCell].Text = shoppingCart[i].Price.ToString("C2");
                CheckBox cb = (CheckBox)GridViewCart.Rows[i].FindControl("gift");
                cb.Checked = shoppingCart[i].GiftWrap;
            }
        }

        protected void GridViewCart_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            IShoppingService shoppingService = SessionManager.GetShoppingService();

            int index = Convert.ToInt32(e.CommandArgument);

            try
            {
                long productId = long.Parse(GridViewCart.DataKeys[index].Values[0].ToString());

                switch (e.CommandName)
                {
                    case "AddItem":
                        shoppingService.AddToShoppingCart(productId);
                        break;

                    case "RemoveItem":
                        shoppingService.RemoveFromShoppingCart(productId);
                        break;

                        //case "CheckGift":
                        //    shoppingService.MarkAsGift(productId);
                        //    break;
                }

                Page_Load(sender, e);
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            catch { }

        }

        //protected void chkview_CheckedChanged(object sender, EventArgs e)
        //{
        //    IShoppingService shoppingService = SessionManager.GetShoppingService();

        //    GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
        //    int index = row.RowIndex;

        //    Page.Response.Redirect("hola");
        //    try
        //    {
        //        long productId = long.Parse(GridViewCart.DataKeys[index].Values[0].ToString());
        //        shoppingService.MarkAsGift(productId);

        //    }
        //    catch { }
        //}

    }
}