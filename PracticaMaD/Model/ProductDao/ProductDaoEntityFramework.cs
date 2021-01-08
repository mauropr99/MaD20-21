﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Caching;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.Util;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductDao
{
    public class ProductDaoEntityFramework :
        GenericDaoEntityFramework<Product, Int64>, IProductDao
    {
        #region IProductDao Members

        public List<Product> FindByProductName(String productName, int startIndex, int count)
        {
            #region Using Linq.

            string cacheObjectName = "FindByProductName" + productName + startIndex + count;
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

        public Product FindByProductName(String productName)
        {
            #region Using Linq.
            Product product = new Product();

            DbSet<Product> products = Context.Set<Product>();

            var result =
                (from p in products
                 where p.product_name == productName
                 select p);

            product = result.FirstOrDefault();

            if (product == null)
                throw new InstanceNotFoundException(productName,
                    typeof(User).FullName);


            return product;

            #endregion Using Linq.
        }

        public List<Product> FindByProductNameAndCategoryName(String productName, string categoryName,
            int startIndex, int count)
        {
            #region Using Linq.

            string cacheObjectName = "FindByProductNameAndCategoryName" + productName + categoryName + startIndex + count;
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

        public string GetCategoryName(long productId)
        {

            DbSet<Product> products = Context.Set<Product>();

            string categoryName =
                (from p in products
                 where p.id == productId
                 select p.Category.name).FirstOrDefault();

            return categoryName;
        }

        public List<Product> FindByLabel(string label, int startIndex, int count)
        {
            #region Using Linq.

            
            DbSet<Label> labels = Context.Set<Label>();

            List<Product> result =
                (from l in labels
                    where l.lab == label
                    select  from p in l.Comments select p.Product).FirstOrDefault().Distinct().Skip(startIndex).Take(count).ToList();

            return result;
            
            #endregion Using Linq.
        }

        #endregion Members
    }
}
