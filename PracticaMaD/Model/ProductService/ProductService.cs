using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Ninject;
using System.Linq;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    public class ProductService:  IProductService
    {
        public ProductService()
        {
        }

        [Inject]
        public IProductDao ProductDao { private get; set; }

        [Inject]
        public ICategoryDao CategoryDao { private get; set; }

        #region IProductService Members

        public ProductBlock ViewCatatalog(string productName, string categoryName, int startIndex, int count)
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
                products = ProductDao.FindByProductNameAndCategoryName(productName, categoryName, startIndex, count + 1);
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
      
                Product prod = ProductDao.Find(productId);
                prod.product_name = productName;
                prod.stock = stock;
                prod.price = price;
                ProductDao.Update(prod);
  
        }

        public List<Category> ViewAallCategories()
        {
           return  CategoryDao.GetAllElements().ToList();
        }

        #endregion IProductService Members

    }

}
