using Es.Udc.DotNet.PracticaMaD.Model.OrderDao;
using Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    public interface IShoppingService
    {
        IOrderDao OrderDao { get;  set; }

        IOrderLineDao OrderLineDao { get;  set; }

        IUserDao UserDao { get; set; }

        Order_Table BuyProducts(User_Table user, ICollection<OrderLine> orderLines,
            string postalAddress, CreditCard creditCard);

    }
}

