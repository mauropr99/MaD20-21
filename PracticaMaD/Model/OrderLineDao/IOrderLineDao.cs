using System;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.OrderLineDao
{
    public interface IOrderLineDao : IGenericDao<OrderLine, Int64>
    {
        /// <summary>
        /// Finds a Orderlines by OrderId
        /// </summary>
        /// <param id="orderId">orderId</param>
        /// <returns>List of OrderLine</returns>
        List<OrderLine> FindByOrderId(long orderId);
        /// <summary>
        /// Finds a The order line
        /// </summary>
        /// <param id="id">id</param>
        /// <returns>The OrderLine</returns>
        /// <exception cref="InstanceNotFoundException"/>
        OrderLine FindById(long id);
    }
}
