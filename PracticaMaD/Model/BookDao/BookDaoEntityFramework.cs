using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.PracticaMaD.Model.Util;

namespace Es.Udc.DotNet.PracticaMaD.Model.BookDao
{
    public class BookDaoEntityFramework :
        GenericDaoEntityFramework<Book, Int64>, IBookDao
    {
        #region IProductDao Members

        public List<Book> FindByProductName(String productName, int startIndex, int count)
        {
            #region Using Linq.
            string cacheObjectName = "FindByProductName" + productName + startIndex + count;
            var cachedObject = CacheUtil.GetFromCache<List<Book>>(cacheObjectName);

            if (cachedObject == null)
            {
                DbSet<Book> products = Context.Set<Book>();

                List<Book> result =
                    (from p in products
                     where p.product_name.Contains(productName)
                     orderby p.product_name
                     select p).Skip(startIndex).Take(count).ToList();

                CacheUtil.AddToCache<List<Book>>(cacheObjectName, result);

                return result;
            }

            return cachedObject;

            #endregion Using Linq.
        }

        #endregion Members
    }
}
