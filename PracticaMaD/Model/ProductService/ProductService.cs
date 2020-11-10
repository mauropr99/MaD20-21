using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Ninject;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    public class ProductService: GenericDaoEntityFramework<Product, long>, IProductService
    {
        public ProductService()
        {
        }

        [Inject]
        public IProductDao ProductDao { private get; set; }

        #region IProductService Members

        public ProductBlock FindProductsByNameAndCategory(string productName, string categoryName, int startIndex, int count)
        {
            List<Product> products;

            /*
            * Find count+1 products to determine if there exist more products above
            * the specified range.
            */
            if (categoryName == null)
            {
                products = ProductDao.FindByProductName(productName, startIndex, count + 1);

            } 
            else
            {
                products = ProductDao.FindByProductNameAndCategoryId(productName, categoryName, startIndex, count + 1);
            }

            bool existMoreProducts = (products.Count == count + 1);

            if (existMoreProducts)
            {
                products.RemoveAt(count);
            }

            return new ProductBlock(products, existMoreProducts);

        }

        public void UpdateProduct(long productId, string productName,  int stock, decimal price)
        {
      
                Product prod = ProductDao.FindById(productId);
                prod.product_name = productName;
                prod.stock = stock;
                prod.price = price;
                ProductDao.Update(prod);
  
        }
       
        #endregion IProductService Members

    }

}
