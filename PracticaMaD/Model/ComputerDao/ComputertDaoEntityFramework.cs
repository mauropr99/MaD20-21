using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.PracticaMaD.Model.Util;

namespace Es.Udc.DotNet.PracticaMaD.Model.ComputerDao
{
    public class ComputerDaoEntityFramework :
        GenericDaoEntityFramework<Computer, Int64>, IComputerDao
    {
        #region IProductDao Members

        

        #endregion Members
    }
}
