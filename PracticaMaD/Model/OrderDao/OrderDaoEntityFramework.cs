using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.OrderDao
{
    public class OrderDaoEntityFramework:
        GenericDaoEntityFramework<Order_Table, Int64>, IOrderDao
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
        public Order_Table FindById(long id)
        {
            Order_Table order = null;

            DbSet<Order_Table> orders = Context.Set<Order_Table>();

            var result =
                (from o in orders
                 where o.id == id
                 select o);

            order = result.FirstOrDefault();

            if (order == null)
                throw new InstanceNotFoundException(id,
                    typeof(Order_Table).FullName);

            return order;
        }

        #endregion IUserProfileDao Members
    }
}
