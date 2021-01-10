using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao
{
    public interface IOrderLineDao : IGenericDao<OrderLine, long>
    {
        List<OrderLine> FindByOrderId(long orderId);
    }
}
