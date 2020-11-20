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

        #endregion IUserProfileDao Members
    }
}
