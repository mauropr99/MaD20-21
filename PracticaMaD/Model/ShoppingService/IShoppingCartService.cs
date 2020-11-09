using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    public interface IShoppingCartService
    {
        IOrderDao OrderDao { set; }

        IOrderLineDao OrderLineDao { set; }

        ShoppingCartDetails AddToShoppingCart(long productId, short quantity, Boolean giftWrap);

        ShoppingCartDetails RemoveFromShoppingCart(long productId);

        ShoppingCartDetails UpdateProductFromShoppingCart(long productId, short quantity, Boolean giftWrap);

        ShoppingCartDetails AddProductLinkToShoppingCart(System.String productLink);

    }
}

