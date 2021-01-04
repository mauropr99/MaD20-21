using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.OrderDao
{
    public class OrderDaoEntityFramework :
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

        #region IOrderLineDao Members. Specific Operations

        public List<Order> FindByUserId(long userId, int startIndex, int count)
        {
            #region Using Linq.

            DbSet<Order> orders = Context.Set<Order>();

            List<Order> result =
                (from o in orders
                 where o.userId == userId
                 orderby o.orderDate descending
                 select o).Skip(startIndex).Take(count).ToList();


            return result;

            #endregion Using Linq.

        }

        public List<OrderLine> FindOrderLinesByOrderId(long orderId)
        {
            DbSet<Order> order = Context.Set<Order>();

            List<OrderLine> result =
                (from o in order
                 where o.id == orderId
                 orderby o.id
                 select o.OrderLines).FirstOrDefault().ToList();


            return result;
        }

        #endregion IOrderDao Members
    }
}
