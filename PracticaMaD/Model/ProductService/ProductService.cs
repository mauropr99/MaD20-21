using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.PracticaMaD.Model.ProductDao;
using Ninject;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    public class ProductService: IProductService
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

        #endregion IProductService Members
    }
}
