using System.Collections.Generic;
using System.Linq;
using Es.Udc.DotNet.PracticaMaD.Model.BookDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.ComputerDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Ninject;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    public class ProductService : IProductService
    {
        public ProductService()
        {
        }

        [Inject]
        public IProductDao ProductDao { private get; set; }
  
        [Inject]
        public ICategoryDao CategoryDao { private get; set; }

        [Inject]
        public IBookDao BookDao { private get; set; }

        [Inject]
        public IComputerDao ComputerDao { private get; set; }

        [Inject]
        public ICommentDao CommentDao { private get; set; }

        #region IProductService Members

        public ProductBlock ViewCatalog(string productName, int startIndex, int count)
        {
            List<Product> products;

            products = ProductDao.FindByProductName(productName, startIndex, count + 1);

            List<ProductDetails> productsDetails = new List<ProductDetails>();

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

        public ProductBlock ViewCatalog(string productName, string categoryName, int startIndex, int count)
        {
            List<Product> products;

            products = ProductDao.FindByProductNameAndCategoryName(productName, categoryName, startIndex, count + 1);

            List<ProductDetails> productsDetails = new List<ProductDetails>();

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

        public ProductBlock ViewProductsByLabels(string label, int startIndex, int count)
        {
            List<Product> products;

            products = ProductDao.FindByLabel(label, startIndex, count + 1);

            List<ProductDetails> productsDetails = new List<ProductDetails>();

            foreach (Product product in products)
            {
                productsDetails.Add(new ProductDetails(product.id, product.product_name, product.price,
                        product.releaseDate, product.stock, ProductDao.GetCategoryName(product.id)));
            }

            bool existMoreProducts = (productsDetails.Count == count + 1);

            if (existMoreProducts)
            {
                products.RemoveAt(count);
            }

            return new ProductBlock(productsDetails, existMoreProducts);

        }

        public void UpdateProduct(Product product)
        {
            ProductDao.Update(product);
        }

        public List<Category> ViewAllCategories()
        {
            return CategoryDao.GetAllElements().ToList();
        }

        public bool HasComments(long productId)
        {
            return (CommentDao.FindCommentsByProductId(productId,0,1).Count != 0);
        }

        public Book FindBook(long productId)
        {
            Book book = BookDao.Find(productId);

            return book;
        }

        public Computer FindComputer(long productId)
        {
            Computer computer = ComputerDao.Find(productId);

            return computer;
        }

        public void UpdateComputer(Computer product)
        {
            ComputerDao.Update(product);
        }

        public void UpdateBook(Book product)
        {
            BookDao.Update(product);
        }

        #endregion IProductService Members

    }

}
