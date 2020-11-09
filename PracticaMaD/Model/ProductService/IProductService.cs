<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;



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
        ProductBlock FindProductsByNameAndCategory(string productName, string categoryName,  int startIndex, int count);

        [Transactional]
        void UpdateProduct(long productId, string productName, int stock, decimal price);


    }
}
=======
﻿using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;



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
        ProductBlock FindProductsByNameAndCategory(string productName, string categoryName,  int startIndex, int count);

        [Transactional]
        void UpdateProduct(long productId, string productName, int stock, decimal price);


    }
}
>>>>>>> 979e3d2518bd9588b193c7bbec9949a7347d4b81