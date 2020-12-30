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


        protected void Btn_AddItem(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            IShoppingService shoppingService = iocManager.Resolve<IShoppingService>();

            ShoppingCartDetails cart = (ShoppingCartDetails)GridViewCart.SelectedRow.DataItem;

            shoppingService.UpdateProductFromShoppingCart(cart.Product_Id, 1);
        }

        protected void Btn_RemoveItem(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)Application["managerIoC"];
            IShoppingService shoppingService = iocManager.Resolve<IShoppingService>();

            ShoppingCartDetails cart = (ShoppingCartDetails)GridViewCart.SelectedRow.DataItem;

            if (cart.Quantity == 1)
                shoppingService.RemoveFromShoppingCart(cart.Product_Id);
            else
                shoppingService.UpdateProductFromShoppingCart(cart.Product_Id, -1);
        }

    }
}