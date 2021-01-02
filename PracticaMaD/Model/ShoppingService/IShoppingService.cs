using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    public interface IShoppingService
    {
        IOrderDao OrderDao { set; }
        IProductDao ProductDao { set; }
        IUserDao UserDao { set; }
        ICreditCardDao CreditCardDao { set; }


        Order BuyProducts(long userId, List<ShoppingCartDetails> shoppingCart,
            string postalAddress, long creditCardId, string description);

        List<ShoppingCartDetails> ViewShoppingCart();

        void AddToShoppingCart(long productId);

        void RemoveFromShoppingCart(long productId);

        void MarkAsGift(long productId);

        [Transactional]
        OrderBlock FindOrdersByUserId(long userId, int startIndex, int count);

        [Transactional]
        List<OrderLineDetails> ViewOrderLineDetails(long orderId);

    }
}

