using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    public interface IProductService
    {
        IProductDao ProductDao { set; }

        ICategoryDao CategoryDao { set; }


        [Transactional]
        ProductBlock ViewCatalog(string productName, int startIndex, int count);

        [Transactional]
        ProductBlock ViewCatalog(string productName, string categoryName, int startIndex, int count);

        [Transactional]
        void UpdateProduct(Product product);

        [Transactional]
        void UpdateComputer(Computer product);

        [Transactional]
        void UpdateBook(Book product);

        [Transactional]
        List<Category> ViewAllCategories();

        [Transactional]
        Book FindBook(long productId);

        [Transactional]
        Computer FindComputer(long productId);
    }
}

