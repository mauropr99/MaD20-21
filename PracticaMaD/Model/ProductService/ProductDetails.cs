using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ProductService
{
    public class ProductDetails
    {
        #region Properties Region

        public long Id { get; private set; }

        public string ProductName { get; private set; }

        public decimal Price { get; private set; }

        public DateTime ReleaseDate { get; private set; }

        public int Stock { get; private set; }

        public string CategoryName { get; private set; }

        #endregion Properties Region

        public ProductDetails(long id, string productName, decimal price, DateTime releaseDate, int stock, string categoryName)
        {
            Id = id;
            ProductName = productName;
            Price = price;
            ReleaseDate = releaseDate;
            Stock = stock;
            CategoryName = categoryName;
        }

        public override bool Equals(object obj)
        {
            return obj is ProductDetails details &&
                   Id == details.Id &&
                   ProductName == details.ProductName &&
                   Price == details.Price &&
                   ReleaseDate == details.ReleaseDate &&
                   Stock == details.Stock &&
                   CategoryName == details.CategoryName;
        }

        public override int GetHashCode()
        {
            int hashCode = -1750425166;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ProductName);
            hashCode = hashCode * -1521134295 + Price.GetHashCode();
            hashCode = hashCode * -1521134295 + ReleaseDate.GetHashCode();
            hashCode = hashCode * -1521134295 + Stock.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CategoryName);
            return hashCode;
        }


        public override string ToString()
        {
            string strProductDetails;

            strProductDetails =
                "[ productName = " + ProductName + " | " +
                "price = " + Price + " | " +
                "releaseDate = " + ReleaseDate + " | " +
                "stock = " + Stock + " | " +
                "categoryName" + CategoryName + " ] ";

            return strProductDetails;
        }



    }
}
