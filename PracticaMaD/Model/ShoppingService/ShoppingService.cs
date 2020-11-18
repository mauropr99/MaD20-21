using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using System;
using Ninject;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    public class ShoppingService : IShoppingService
    {

        [Inject]
        public IOrderDao OrderDao { private get; set; }

        [Inject]
        public IProductDao ProductDao { private get; set; }

        [Inject]
        public IOrderLineDao OrderLineDao { private get; set; }

        private ShoppingCartDetails shoppingCart = new ShoppingCartDetails();


        #region IShoppingService Members

        public Order BuyProducts(User user, ICollection<OrderLine> orderLines,
            string postalAddress, CreditCard creditCard, string description)
        {

            //Check expiration date
            if (creditCard.expirationDate < DateTime.Now)
            {
                throw new CreditCardAlreadyExpired(creditCard.creditCardNumber);
            }

            //Calculate total price
            decimal totalPrice = 0;
            foreach (OrderLine line in orderLines)
            {
                totalPrice += line.quantity * line.price;
            }

            Order order = new Order();
            order.postalAddress = postalAddress;
            order.orderDate = DateTime.Now;
            order.totalPrice = totalPrice;
            order.CreditCard = creditCard;
            order.OrderLines = orderLines;
            order.User_Table = user;
            order.description = description;

            //Llamar al DAO para crear el order.
            OrderDao.Create(order);

            //Preguntar a ellos
            CleanShoppingCart();

            return order;
        }

        public ShoppingCartDetails AddToShoppingCart(long productId,
            short quantity, Boolean giftWrap)
        {
            Product product = ProductDao.Find(productId);

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

        private void CleanShoppingCart()
        {
            foreach (OrderLine line in shoppingCart.OrderLines)
            {
                
                //Remove element from collection
                shoppingCart.OrderLines.Remove(line);
                //Update total price
                shoppingCart.TotalPrice -= line.quantity * line.price;
                
            }
        }

        public OrderBlock FindOrdersByUserId(long userId, int startIndex, int count)
        {
            List<Order> orders;

            /*
            * Find count+1 orders to determine if there exist more orders above
            * the specified range.
            */
            orders = OrderDao.FindByUserId(userId, startIndex, count + 1);

            bool existMoreOrders = (orders.Count == count + 1);

            if (existMoreOrders)
            {
                orders.RemoveAt(count);
            }

            List<OrderDetails> detailOrders = new List<OrderDetails>();

            foreach(Order order in orders)
            {
                detailOrders.Add(new OrderDetails(order.id, order.orderDate, order.description, order.totalPrice));
            }

            return new OrderBlock(detailOrders, existMoreOrders);
        }

        public List<OrderLineDetails> ViewOrderDetails(long orderId)
        {

            List<OrderLine> orderLines;

            orderLines = OrderLineDao.FindByOrderId(orderId);

            List<OrderLineDetails> detailLineOrders = new List<OrderLineDetails>();

            foreach(OrderLine orderLine in orderLines)
            {
                detailLineOrders.Add(new OrderLineDetails(orderLine.id, orderLine.Product.product_name, orderLine.quantity, orderLine.price));
            }

            return detailLineOrders;
        }

        #endregion IShoppingService Members

    }
}
