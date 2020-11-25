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
        
    }
}
