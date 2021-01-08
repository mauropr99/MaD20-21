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

        public List<Computer> FindByProductName(String productName, int startIndex, int count)
        {
            #region Using Linq.
            string cacheObjectName = "FindByProductName" + productName + startIndex + count;
            var cachedObject = CacheUtil.GetFromCache<List<Computer>>(cacheObjectName);

            if (cachedObject == null)
            {
                DbSet<Computer> products = Context.Set<Computer>();

                List<Computer> result =
                    (from p in products
                     where p.product_name.Contains(productName)
                     orderby p.product_name
                     select p).Skip(startIndex).Take(count).ToList();

                CacheUtil.AddToCache<List<Computer>>(cacheObjectName, result);

                return result;
            }

            return cachedObject;


            #endregion Using Linq.
        }

        #endregion Members
    }
}
