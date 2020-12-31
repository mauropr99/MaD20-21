using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;



namespace Es.Udc.DotNet.PracticaMaD.Model.ComputerDao
{
    public class ComputerDaoEntityFramework :
        GenericDaoEntityFramework<Computer, Int64>, IComputerDao
    {
        #region IProductDao Members

        public List<Computer> FindByProductName(String product_name, int startIndex, int count)
        {
            #region Using Linq.

            DbSet<Computer> products = Context.Set<Computer>();

            List<Computer> result =
                (from p in products
                 where p.product_name.Contains(product_name)
                 orderby p.product_name
                 select p).Skip(startIndex).Take(count).ToList();

            return result;

            #endregion Using Linq.
        }

        #endregion Members
    }
}
