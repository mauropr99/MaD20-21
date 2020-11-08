using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductDao
{
    public class ProductDaoEntityFramework :
        GenericDaoEntityFramework<Product, Int64>, IProductDao
    {
        #region IProductDao Members
        
        Product FindById(long id)
        {
            Product product = null;

            #region Option 1: Using Linq.

            DbSet<Product> products = Context.Set<Product>();

            var result =
                (from p in products
                 where p.id == id
                 select p);

            product = result.FirstOrDefault();

            #endregion Option 1: Using Linq.

            if (product == null)
                throw new InstanceNotFoundException(id,
                    typeof(Product).FullName);

            return product;

        }

        Product IProductDao.FindById(long id)
        {
            throw new NotImplementedException();
        }

        List<Product> FindByProductName(String product_name, int startIndex, int count)
        {
            #region Using Linq.

            DbSet<Product> products = Context.Set<Product>();

            var result = 
                (from p in products
                 where p.product_name == product_name
                 orderby p.id
                 select p).Skip(startIndex).Take(count).ToList();

            return result;

            #endregion Using Linq.
        }

        List<Product> IProductDao.FindByProductName(string product_name, int startIndex, int count)
        {
            throw new NotImplementedException();
        }

        List<Product> FindByProductNameAndCategoryId(String product_name, long categoryId,
            int startIndex, int count)
        {
            #region Using Linq.

            DbSet<Product> products = Context.Set<Product>();
            
            var result =
                (from p in products
                 where p.product_name == product_name
                 && p.categoryId == categoryId
                 orderby p.id
                 select p).Skip(startIndex).Take(count).ToList();


            return result;

            #endregion Using Linq.
        }

        List<Product> IProductDao.FindByProductNameAndCategoryId(string product_name, long categoryId, int startIndex, int count)
        {
            throw new NotImplementedException();
        }
        #endregion Members
    }
}
