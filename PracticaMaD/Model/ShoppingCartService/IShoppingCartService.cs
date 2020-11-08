using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingCartService
{
    public interface IShoppingCartService
    {
        IOrderDao OrderDao { set; }

        IOrderLineDao OrderLineDao { set; }

        ShoppingCartResult AddToShoppingCart(Order_Table order, long productId, short quantity, Boolean giftWrap);

        ShoppingCartResult RemoveFromShoppingCart(Order_Table order, long productId);

        ShoppingCartResult UpdateProductFromShoppingCart(Order_Table order, long productId, short quantity, Boolean giftWrap);

        ShoppingCartResult AddProductLinkToShoppingCart(Order_Table order, System.String productLink);

        [Transactional]
        Order_Table BuyProducts(Order_Table order);

    }
}

