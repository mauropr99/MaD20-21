using System.Collections.Generic;


namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    public class ProductBlock
    {
        public List<Product> Products { get; private set; }
        public bool ExistMoreProducts { get; private set; }

        public ProductBlock(List<Product> products, bool existMoreProducts)
        {
            this.Products = products;
            this.ExistMoreProducts = existMoreProducts;
        }
    }
}
