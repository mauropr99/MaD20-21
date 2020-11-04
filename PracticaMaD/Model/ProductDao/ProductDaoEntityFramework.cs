using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Dao;
using Microsoft.EntityFrameworkCore;

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

        List<Product> FindByProductNameAndCategoryId(String product_name, long categoryId,
            int startIndex, int count)
        {
            #region Using Linq.

            DbSet<Product> products = Context.Set<Product>();

            var result =
                (from p in products
                 where p.product_name == product_name
                 and p.categoryId == categoryId
                 orderby p.id
                 select p).Skip(startIndex).Take(count).ToList();

            return result;

            #endregion Using Linq.
        }
        #endregion Members
    }
}
