using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Shopping
{
    public partial class ShoppingCart : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int giftCell = 6, priceCell = 2;


            IShoppingService shoppingService = SessionManager.GetShoppingService();
            List<ShoppingCartDetails> shoppingCart = shoppingService.ViewShoppingCart();

            GridViewCart.DataSource = shoppingCart;
            GridViewCart.DataBind();

            for (int i = 0; i < GridViewCart.Rows.Count; i++)
            {
                GridViewCart.Rows[i].Cells[priceCell].Text = shoppingCart[i].Price.ToString("C2");
                GridViewCart.Rows[i].Cells[giftCell].Visible = shoppingCart[i].GiftWrap;
                CheckBox cb = (CheckBox)GridViewCart.Rows[i].FindControl("gift");
                cb.Enabled = false;
            }

            if (shoppingCart.Count != 0)
            {
                imgEmptyCart.Visible = false;
                txtEmptyCart.Visible = false;
            }
            else
            {
                btn_BuyProducts.Visible = false;
                Subtotal.Visible = false;
                LblSubtotal.Visible = false;
            }

            Subtotal.Text = shoppingService.Subtotal().ToString("C2");
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

                    case "CheckGift":
                        CheckBox cb = (CheckBox)GridViewCart.Rows[index].FindControl("gift");
                        cb.Visible = !cb.Visible;
                        shoppingService.MarkAsGift(productId);
                        break;
                }

                Page_Load(sender, e);
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            catch { }

        }

        protected void Btn_BuyProducts(object sender, EventArgs e)
        {
            IShoppingService shoppingService = SessionManager.GetShoppingService();
            List<ShoppingCartDetails> shoppingCart = shoppingService.ViewShoppingCart();

            if (shoppingCart.Count == 0)
            {
                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Product/Catalog.aspx"));
            }
            else
            {
                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Shopping/Purchase.aspx"));
            }
        }

    }
}