using System;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.OrderDao
{
    public interface IOrderDao : IGenericDao<Order_Table, Int64>
    {
        /// <summary>
        /// Finds a The order
        /// </summary>
        /// <param id="id">id</param>
        /// <returns>The Order</returns>
        /// <exception cref="InstanceNotFoundException"/>
        Order_Table FindById(long id);

         /// <summary>
        /// Returns a list of orders that matches to a given user identifier.
        /// </summary>
        /// <param name="userId">the user identifier</param>
        /// <param name="startIndex">the index of the first order to return (starting in 0)</param>
        /// <param name="count">the maximum number of orders to return</param>
        /// <returns>the list of orders</returns>
        List<Order_Table> FindByUserId(long userId, int startIndex, int count);

    }
}
