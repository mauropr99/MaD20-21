using System;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Ninject;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    public class ShoppingService : IShoppingService
    {
        [Inject]
        public IUserDao UserDao { private get; set; }

        [Inject]
        public IOrderDao OrderDao { private get; set; }

        [Inject]
        public IProductDao ProductDao { private get; set; }

        [Inject]
        public ICreditCardDao CreditCardDao { private get; set; }

        [Inject]
        public IOrderLineDao OrderLineDao { private get; set; }

        private List<ShoppingCartDetails> shoppingCart = new List<ShoppingCartDetails>();
        
        #region IShoppingService Members

        public Order BuyProducts(long userId, List<ShoppingCartDetails> shoppingCart,
            string postalAddress, long creditCardId, string description)
        {
            List<OrderLine> orderLines = new List<OrderLine>();
            Product product = new Product();
            decimal totalPrice = 0;

            User user = UserDao.Find(userId);
            CreditCard creditCard = CreditCardDao.Find(creditCardId);

            //Check expiration date
            if (creditCard.expirationDate < DateTime.Now)
            {
                throw new CreditCardAlreadyExpired(creditCard.creditCardNumber);
            }

            //Calculate total price
           
            foreach (ShoppingCartDetails line in shoppingCart)
            {
                OrderLine orderLine = new OrderLine();
                product = ProductDao.Find(line.Product_Id);
                if (product.stock < line.Quantity)
                {
                    throw new NotEnoughStock(product.product_name, product.stock);
                }
                product.stock -= line.Quantity;
                ProductDao.Update(product);
                if (!product.price.Equals(line.Price))
                {
                    throw new DifferentPrice(product.price, product.product_name);
                }
                orderLine.price = product.price;
                orderLine.productId = product.id;
                orderLine.quantity = line.Quantity;
                OrderLineDao.Create(orderLine);

                totalPrice += line.Quantity * line.Price;
                orderLines.Add(orderLine);
            }

            Order order = new Order
            {
                postalAddress = postalAddress,
                orderDate = DateTime.Now,
                totalPrice = totalPrice,
                CreditCard = creditCard,
                OrderLines = orderLines,
                userId = userId,
                description = description
            };

            //Llamar al DAO para crear el order.
            OrderDao.Create(order);
            order = OrderDao.Find(order.id);
            foreach (OrderLine line in orderLines)
            {
                line.orderId = order.id;
                OrderLineDao.Update(line);
            }

            shoppingCart.Clear();

            return order;
        }

        public List<ShoppingCartDetails> ViewShoppingCart()
        { 
            return shoppingCart;
        }

        public void AddToShoppingCart(long productId)
        {
            Product product = ProductDao.Find(productId);
            bool existsInCart = false;

            //Check if the product is inside the shopping cart already
            foreach (ShoppingCartDetails line in shoppingCart)
            {
                if (line.Product_Id == productId)
                {
                    //Update product quantity
                    line.Quantity += 1;
                    existsInCart = true;
                    break;

                }
            }

            //If it is a new product in the shopping cart
            if (!existsInCart)
            {
                ShoppingCartDetails shoppingCartLine = new ShoppingCartDetails
                {
                    Quantity = 1,
                    Product_Name = product.product_name,
                    Price = product.price,
                    Product_Id = product.id, 
                    GiftWrap = false
                };

                shoppingCart.Add(shoppingCartLine);
            }

        }


        public void RemoveFromShoppingCart(long productId)
        {
            
            //Check if the product is inside the shopping cart 
            foreach (ShoppingCartDetails line in shoppingCart)
            {
                if (line.Product_Id == productId)
                {
                    if (line.Quantity == 1)
                    {
                        //Remove element from collection
                        shoppingCart.Remove(line);
                    }
                    else
                    {
                        //Update product quantity
                        line.Quantity -= 1;
                    }

                    break;   
                }
            }
        }

        public void MarkAsGift(long productId)
        {
            foreach (ShoppingCartDetails line in shoppingCart)
            {
                if (line.Product_Id == productId)
                {
                    line.Product_Name = "NO FUNCIONA";
                    //line.GiftWrap = !line.GiftWrap;
                    break;
                }
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

            foreach (Order order in orders)
            {
                detailOrders.Add(new OrderDetails(order.id, order.orderDate, order.description, order.totalPrice));
            }

            return new OrderBlock(detailOrders, existMoreOrders);
        }


        public List<OrderLineDetails> ViewOrderLineDetails(long orderId)
        {
            List<OrderLine> orderLines = new List<OrderLine>();

            orderLines = OrderLineDao.FindByOrderId(orderId);

            List<OrderLineDetails> detailLineOrders = new List<OrderLineDetails>();

            foreach (OrderLine orderLine in orderLines)
            {
                detailLineOrders.Add(new OrderLineDetails(orderLine.productId, orderLine.Product.product_name, orderLine.quantity, orderLine.price));
            }

            return detailLineOrders;
        }

        #endregion IShoppingService Members
    }
}
