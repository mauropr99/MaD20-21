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

        public ProductBlock ViewCatalog(string productName, int startIndex, int count)
        {
            List<Product> products;

            products = ProductDao.FindByProductName(productName, startIndex, count + 1);

            List<ProductDetails> productsDetails = new List<ProductDetails>;
            
            foreach(Product product in products)
            {
                productsDetails.Add(new ProductDetails(product.id, product.product_name, product.price,
                        product.releaseDate, product.stock, ProductDao.GetCategoryName(product.id)));
            }

            bool existMoreProducts = (products.Count == count + 1);

            if (existMoreProducts)
            {
                products.RemoveAt(count);
            }

            return new ProductBlock(productsDetails, existMoreProducts);

        }

        public ProductBlock ViewCatalog(string productName, string categoryName, int startIndex, int count)
        {
            List<Product> products;

            products = ProductDao.FindByProductNameAndCategoryName(productName, categoryName, startIndex, count + 1);

            List<ProductDetails> productsDetails = new List<ProductDetails>;

            foreach (Product product in products)
            {
                productsDetails.Add(new ProductDetails(product.id, product.product_name, product.price,
                        product.releaseDate, product.stock, ProductDao.GetCategoryName(product.id)));
            }

            bool existMoreProducts = (products.Count == count + 1);

            if (existMoreProducts)
            {
                products.RemoveAt(count);
            }

            return new ProductBlock(productsDetails, existMoreProducts);

        }

        public void UpdateProduct(long productId, string productName,  int stock, decimal price)
        {
      
                Product prod = ProductDao.Find(productId);
                prod.product_name = productName;
                prod.stock = stock;
                prod.price = price;
                ProductDao.Update(prod);
  
        }

        public List<Category> ViewAllCategories()
        {
           return  CategoryDao.GetAllElements().ToList();
        }

        #endregion IProductService Members

    }

}
