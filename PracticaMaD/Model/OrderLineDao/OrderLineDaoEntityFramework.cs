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

        public List<OrderLine> FindByOrderId(long orderId)
        {
            DbSet<OrderLine> orderLine = Context.Set<OrderLine>();

            var result =
                (from o in orderLine
                 where o.orderId == orderId
                 orderby o.id
                 select o).ToList();

            if (result.Count > 0)
                return result[0].Order_Table.OrderLines.ToList();
            else return result;
        }
        #endregion Public Constructors

    }
}
