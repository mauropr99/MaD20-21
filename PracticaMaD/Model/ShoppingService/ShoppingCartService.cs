using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;
using System.Collections.Generic;


namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
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
        

        public ShoppingCartDetails AddToShoppingCart(long productId, 
            short quantity, Boolean giftWrap)
        {
            Product product = ProductDao.FindById(productId);

            //Check if the product is inside the shopping cart already
            foreach (OrderLine line in shoppingCart.OrderLines)
            {
                if (line.productId == productId)
                {
                    //Update product quantity
                    line.quantity += quantity;
                    //Update total price
                    shoppingCart.TotalPrice += quantity + product.price;
                    return shoppingCart;
                }
            }

            //If it is a new product in the shopping cart
            OrderLine orderLine = new OrderLine();
            orderLine.quantity = quantity;
            orderLine.price = product.price;
            orderLine.productId = product.id;
            shoppingCart.OrderLines.Add(orderLine);

            foreach (OrderLine line in shoppingCart.OrderLines)
            {
                shoppingCart.TotalPrice += line.price * line.quantity;
            }
            return shoppingCart;
        }


        public ShoppingCartDetails RemoveFromShoppingCart(long productId)
        {
            //Check if the product is inside the shopping cart 
            foreach (OrderLine line in shoppingCart.OrderLines)
            {
                if (line.productId == productId)
                {
                    //Remove element from collection
                    shoppingCart.OrderLines.Remove(line);
                    //Update total price
                    shoppingCart.TotalPrice -= line.quantity * line.price;
                }
            }

            return shoppingCart;
        }

        public ShoppingCartDetails UpdateProductFromShoppingCart(long productId, short quantity, bool giftWrap)
        {
            foreach (OrderLine line in shoppingCart.OrderLines)
            {
                if (line.productId == productId)
                {
                    //Update total price
                    shoppingCart.TotalPrice += (quantity - line.quantity) * line.price;
                    //Update quantity
                    line.quantity = quantity;
                    
                }
            }

            return shoppingCart;
        }

        public ShoppingCartDetails AddProductLinkToShoppingCart(string productLink)
        {
            throw new NotImplementedException();
        }




        #endregion IShoppingCartService Members
    }
}
