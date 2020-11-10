using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    public interface IShoppingService
    {
        IOrderDao OrderDao { set; }

        IProductDao ProductDao { set; }

        Order_Table BuyProducts(User_Table user, ICollection<OrderLine> orderLines,
            string postalAddress, CreditCard creditCard);

        ShoppingCartDetails AddToShoppingCart(long productId, short quantity, Boolean giftWrap);

        ShoppingCartDetails RemoveFromShoppingCart(long productId);

        ShoppingCartDetails UpdateProductFromShoppingCart(long productId, short quantity, Boolean giftWrap);

        ShoppingCartDetails AddProductLinkToShoppingCart(System.String productLink);

        [Transactional]
        OrderBlock FindOrdersByUserId(long userId, int startIndex, int count);

    }
}

