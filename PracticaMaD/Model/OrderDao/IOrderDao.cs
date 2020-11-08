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

    }
}
