using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Transactions;
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


        Order BuyProducts(UserDetails user, List<OrderLineDetails> orderLinesDetails,
            string postalAddress, CreditCard creditCard, string description);

        List<ShoppingCartDetails> ViewShoppingCart();

        void AddToShoppingCart(long productId);

        void RemoveFromShoppingCart(long productId);

        void UpdateProductFromShoppingCart(long productId, short quantity);

        void MarkAsGift(long productId, bool giftWrap);

        [Transactional]
        OrderBlock FindOrdersByUserId(long userId, int startIndex, int count);

        [Transactional]
        List<OrderLineDetails> ViewOrderLineDetails(long orderId);

    }
}

