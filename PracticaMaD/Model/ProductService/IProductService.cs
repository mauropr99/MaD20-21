using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.BookDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.ComputerDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    public interface IProductService
    {
        IProductDao ProductDao { set; }

        ICategoryDao CategoryDao { set; }

        IBookDao BookDao { set; }

        IComputerDao ComputerDao { set; }

        ICommentDao CommentDao { set; }

        [Transactional]
        ProductBlock ViewCatalog(string productName, int startIndex, int count);

        [Transactional]
        ProductBlock ViewCatalog(string productName, string categoryName, int startIndex, int count);

        [Transactional]
        ProductBlock ViewProductsByLabels(string label, int startIndex, int count);

        [Transactional]
        void UpdateProduct(Product product);

        [Transactional]
        void UpdateComputer(Computer product);
        [Transactional]

        [Transactional]
        bool HasComments(long productId);

        [Transactional]
        void UpdateBook(Book product);

        [Transactional]
        List<Category> ViewAllCategories();

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        Book FindBook(long productId);

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        Computer FindComputer(long productId);
    }
}

