using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.OrderDao
{
    public interface IOrderDao : IGenericDao<Order, Int64>
    {

        /// <summary>
        /// Returns a list of orders that matches to a given user identifier.
        /// </summary>
        /// <param name="userId">the user identifier</param>
        /// <param name="startIndex">the index of the first order to return (starting in 0)</param>
        /// <param name="count">the maximum number of orders to return</param>
        /// <returns>the list of orders</returns>
        List<Order> FindByUserId(long userId, int startIndex, int count);

        List<OrderLine> FindOrderLinesByOrderId(long orderId);

    }
}
