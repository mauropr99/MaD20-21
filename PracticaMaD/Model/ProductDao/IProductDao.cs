using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductDao
{
    public interface IProductDao :IGenericDao<Product, Int64>

    {
        /// <summary>
        /// Returns a list of products that matches to a given name.
        /// </summary>
        /// <param name="product_name">the product name </param>
        /// <param name="startIndex">the index of the first product to return (starting in 0)</param>
        /// <param name="count">the maximum number of products to return</param>
        /// <returns>the list of products</returns>
        List<Product> FindByProductName(String product_name, int startIndex, int count);
        /// <summary>
        /// Returns a list of products that matches to a given name. If the category
        /// is not null, it returns a list of products pertaining to the given category too.
        /// </summary>
        /// <param name="product_name">the product name </param>
        /// <param name="category_name">the category name</param>
        /// <param name="startIndex">the index of the first product to return (starting in 0)</param>
        /// <param name="count">the maximum number of products to return</param>
        /// <returns>the list of products</returns>
        List<Product> FindByProductNameAndCategoryName(String product_name, string categoryName, int startIndex, int count);

        Product FindByProductName(String product_name);

        string GetCategoryName(long productId);
    }
}
