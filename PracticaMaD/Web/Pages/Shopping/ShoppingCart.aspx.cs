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

namespace Web.Pages.Shopping
{
    public partial class ShoppingCart : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            IShoppingService shoppingService = iocManager.Resolve<IShoppingService>();

            List<ShoppingCartDetails> shoppingCart = shoppingService.ViewShoppingCart();

            this.GridViewCart.DataSource = shoppingCart;
            this.GridViewCart.DataBind();
        }

        protected void GridViewCart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            IShoppingService shoppingService = iocManager.Resolve<IShoppingService>();

            int index = Convert.ToInt32(e.CommandArgument);
            long productId = long.Parse(GridViewCart.DataKeys[index].Values[0].ToString());

            switch (e.CommandName)
            {
                case "AddItem":
                    shoppingService.UpdateProductFromShoppingCart(productId, 1);
                    break;

                //case "RemoveItem":
                //    if (cart.Quantity == 1)
                //        shoppingService.RemoveFromShoppingCart(productId);
                //    else
                //        shoppingService.UpdateProductFromShoppingCart(cart.Product_Id, -1);
                //    break;

                //case "CheckGift":
                //    shoppingService.MarkAsGift(cart.Product_Id, !cart.GiftWrap);
                //    break;
            }

        }
    }
}