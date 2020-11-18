using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.OrderDao
{
    public class OrderDaoEntityFramework:
        GenericDaoEntityFramework<Order, Int64>, IOrderDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public OrderDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region IOrderDao Members. Specific Operations
        public Order FindById(long id)
        {
            #region Using Linq.
            Order order = null;

            DbSet<Order> orders = Context.Set<Order>();

            var result =
                (from o in orders
                 where o.id == id
                 select o);

            order = result.FirstOrDefault();

            if (order == null)
                throw new InstanceNotFoundException(id,
                    typeof(Order).FullName);

            return order;

            #endregion Using Linq.
        }

        public List<Order> FindByUserId(long userId, int startIndex, int count)
        {
            #region Using Linq.

            DbSet<Order> orders = Context.Set<Order>();

            List<Order> result =
                (from o in orders
                 where o.userId == userId
                 orderby o.orderDate
                 select o).Skip(startIndex).Take(count).ToList();


            return result;

            #endregion Using Linq.

        }

        #endregion IOrderDao Members
    }
}
