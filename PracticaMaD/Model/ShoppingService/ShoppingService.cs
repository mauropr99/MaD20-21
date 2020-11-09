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
    public class ShoppingService : IShoppingService
    {

        public IOrderDao OrderDao { get; set; }
        public IOrderLineDao OrderLineDao { get;  set; }
        public IUserDao UserDao { get;  set; }


        #region IShoppingService Members

        public Order_Table BuyProducts(User_Table user, ICollection<OrderLine> orderLines,
            string postalAddress, CreditCard creditCard)
        {

            //Check expiration date
            if (creditCard.expirationDate < DateTime.Now)
            {
                //return exception
            }

            //Calculate total price
            decimal totalPrice = 0;
            foreach (OrderLine line in orderLines)
            {
                totalPrice += line.quantity * line.price;
            }

            Order_Table order = new Order_Table();
            order.postalAddress = postalAddress;
            order.orderDate = DateTime.Now;
            order.totalPrice = totalPrice;
            order.CreditCard = creditCard;
            order.OrderLines = orderLines;
            order.User_Table = user;

            //Llamar al DAO para crear el order.
            OrderDao.Create(order);

            return order;
        }

        #endregion IShoppingService Members
        

    }
}
