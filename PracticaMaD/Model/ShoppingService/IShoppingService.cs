using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
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
        ICategoryDao CategoryDao { set; }

        [Transactional]
        Order BuyProducts(long userId, List<ShoppingCartDetails> shoppingCart,
            string postalAddress, long creditCardId, string description);

        [Transactional]
        Decimal Subtotal();

        List<ShoppingCartDetails> ViewShoppingCart();

        void AddToShoppingCart(long productId);

        void ClearShoppingCart();

        void AddToShoppingCart(long productId, short quantity);

        void RemoveFromShoppingCart(long productId);

        void MarkAsGift(long productId);

        [Transactional]
        OrderBlock FindOrdersByUserId(long userId, int startIndex, int count);

        [Transactional]
        List<OrderLineDetails> ViewOrderLineDetails(long orderId);
        
        int TotalProducts();

    }
}

