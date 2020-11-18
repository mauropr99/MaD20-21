using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Ninject;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    public interface IProductService
    {
        IProductDao ProductDao { set; }

        /// <summary>
        /// Finds products using the product identifier and category.
        /// </summary>
        /// <param name="productName">The name of the product.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="startIndex">the index (starting from 0) of the first
        /// object to return.</param>
        /// <param name="count">The maximum number of objects to return.</param>
        /// <returns>The list of products within an AccountBlock.</returns>
        [Transactional]
        ProductBlock ViewOrderHistorical(string productName, string categoryName,  int startIndex, int count);

        [Transactional]
        void UpdateProduct(long productId, string productName, int stock, decimal price);
    }
}

