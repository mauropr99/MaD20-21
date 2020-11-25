using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Ninject;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    public interface IProductService
    {
        IProductDao ProductDao { set; }

        ICategoryDao CategoryDao { set; }


        [Transactional]
        ProductBlock ViewCatalog(string productName, int startIndex, int count);

        [Transactional]
        ProductBlock ViewCatalog(string productName, string categoryName,  int startIndex, int count);

        [Transactional]
        void UpdateProduct(long productId, string productName, int stock, decimal price);[Transactional]

        [Transactional]
        List<Category> ViewAllCategories();
    }
}

