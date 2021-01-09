using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao
{
    public class OrderLineDaoEntityFramework :
        GenericDaoEntityFramework<OrderLine, Int64>, IOrderLineDao
    {
        #region Public Constructors

        public List<OrderLine> FindByOrderId(long orderId)
        {
            DbSet<OrderLine> orderLine = Context.Set<OrderLine>();

            var result =
                (from o in orderLine
                 where o.orderId == orderId
                 orderby o.id
                 select o).ToList();

            return result;

        }
        #endregion Public Constructors

    }
}
