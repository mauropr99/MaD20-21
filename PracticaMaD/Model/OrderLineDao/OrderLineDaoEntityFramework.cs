using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao
{
    public class OrderLineDaoEntityFramework :
        GenericDaoEntityFramework<OrderLine, Int64>, IOrderLineDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public OrderLineDaoEntityFramework()
        {
        }
        #endregion Public Constructors
        
        #region IOrderLineDao Members. Specific Operations
        public OrderLine FindById(long id)
        {
            OrderLine orderLine = null;

            DbSet<OrderLine> orderLines = Context.Set<OrderLine>();

            var result =
                (from o in orderLines
                 where o.id == id
                 select o);

            orderLine = result.FirstOrDefault();

            if (orderLine == null)
                throw new InstanceNotFoundException(id,
                    typeof(OrderLine).FullName);

            return orderLine;
        }

        public List<OrderLine> FindByOrderId(long orderId)
        {
            DbSet<OrderLine> orderLines = Context.Set<OrderLine>();

            var result =
                 (from a in orderLines
                  where a.orderId == orderId
                  orderby a.id
                  select a).ToList();

            return result;
        }

        public Product FindByProductId(long productId)
        {
            Product product = null;

            DbSet<Product> products = Context.Set<Product>();

            var result =
                (from p in products
                 where p.id == productId
                 select p);

            product = result.FirstOrDefault();

            if (product == null)
                throw new InstanceNotFoundException(productId,
                    typeof(OrderLine).FullName);

            return product;
        }

        #endregion IUserProfileDao Members
    }
}
