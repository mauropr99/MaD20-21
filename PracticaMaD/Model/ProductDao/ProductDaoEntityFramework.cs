using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.Util;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductDao
{
    public class ProductDaoEntityFramework :
        GenericDaoEntityFramework<Product, long>, IProductDao
    {
        #region IProductDao Members

        public List<Product> FindByProductName(string productName, int startIndex, int count)
        {
            #region Using Linq.

            string cacheObjectName = "FindByProductName?productName=" + productName + "&startIndex=" + startIndex + "&count=" + count;
            var cachedObject = CacheUtil.GetFromCache<List<Product>>(cacheObjectName);

            if (cachedObject == null)
            {

                DbSet<Product> products = Context.Set<Product>();

                List<Product> result =
                    (from p in products
                     where p.product_name.Contains(productName)
                     orderby p.product_name
                     select p).Skip(startIndex).Take(count).ToList();

                CacheUtil.AddToCache<List<Product>>(cacheObjectName, result);

                return result;
            }

            return cachedObject;


            #endregion Using Linq.
        }

        /// <exception cref="InstanceNotFoundException"></exception>
        public Product FindByProductName(string productName)
        {
            #region Using Linq.
            Product product = new Product();

            public List<Product> FindByProductNameAndCategoryName(string productName, string categoryName,
                int startIndex, int count)
            {
                #region Using Linq.

                string cacheObjectName = "FindByProductNameAndCategoryName?productName=" + productName + "&startIndex=" + startIndex + "&count=" + count;
                var cachedObject = CacheUtil.GetFromCache<List<Product>>(cacheObjectName);

                if (cachedObject == null)
                {
                    DbSet<Product> products = Context.Set<Product>();

                    List<Product> result =
                        (from p in products
                         where p.product_name.Contains(productName) && p.Category.name == categoryName
                         orderby p.product_name
                         select p).Skip(startIndex).Take(count).ToList();

                    CacheUtil.AddToCache<List<Product>>(cacheObjectName, result);

                    return result;
                }

                return cachedObject;

                #endregion Using Linq.
            }

            /// <exception cref="InstanceNotFoundException"></exception>
            public string GetCategoryName(long productId)
            {

                DbSet<Product> products = Context.Set<Product>();

                string categoryName =
                    (from p in products
                     where p.id == productId
                     select p.Category.name).FirstOrDefault();

                if (categoryName == null)
                    throw new InstanceNotFoundException("",
                        typeof(Category).FullName);

                return categoryName;
            }

            public List<Product> FindByLabel(string label, int startIndex, int count)
            {
                #region Using Linq.


                DbSet<Label> labels = Context.Set<Label>();

                List<Product> result =
                    (from l in labels
                     where l.lab == label
                     select from p in l.Comments select p.Product).FirstOrDefault().Distinct().Skip(startIndex).Take(count).ToList();

                return result;

                #endregion Using Linq.
            }

            #endregion Members
        }
    }
