using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;
using System.Collections.Generic;


namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingCartService
{
    public class ShoppingCartService : IShoppingCartService
    {
        [Inject]
        public IOrderDao OrderDao { private get; set; }

        [Inject]
        IOrderLineDao OrderLineDao {  get; set; }

        [Inject]
        IProductDao ProductDao { get; set; }

        [Inject]
        IUserDao UserDao { get; set; }
        IOrderLineDao IShoppingCartService.OrderLineDao { set => throw new NotImplementedException(); }

        private ShoppingCartDetails shoppingCart = new ShoppingCartDetails();
        
        #region IShoppingCartService Members
        

        ShoppingCartDetails AddToShoppingCart(long productId, 
            short quantity, Boolean giftWrap)
        {
            Product product = ProductDao.FindById(productId);

            //Check stock
            if (product.stock < quantity)
            {
                //return exception
            }

            OrderLine orderLine = new OrderLine();
            orderLine.quantity = quantity;
            orderLine.price = product.price;
            orderLine.productId = product.id;

            //shoppingCart.OrderLines.add(orderLine);
            return shoppingCart;
        }

        public ShoppingCartResult AddToShoppingCart(Order_Table order, long productId, short quantity, bool giftWrap)
        {
            throw new NotImplementedException();
        }

        public ShoppingCartResult RemoveFromShoppingCart(Order_Table order, long productId)
        {
            throw new NotImplementedException();
        }

        public ShoppingCartResult UpdateProductFromShoppingCart(Order_Table order, long productId, short quantity, bool giftWrap)
        {
            throw new NotImplementedException();
        }

        public ShoppingCartResult AddProductLinkToShoppingCart(Order_Table order, string productLink)
        {
            throw new NotImplementedException();
        }

        public Order_Table BuyProducts(Order_Table order)
        {
            throw new NotImplementedException();
        }



        #endregion IShoppingCartService Members
    }
}
