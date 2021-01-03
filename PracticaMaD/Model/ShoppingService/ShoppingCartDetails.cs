using System;
using System.Collections.Generic;


namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{

    [Serializable()]
    public class ShoppingCartDetails
    {
        public long Product_Id { get; set; }

        public string Product_Name { get; set; }

        public string CategoryName { get; set; }

        public short Quantity { get; set; }

        public decimal Price { get; set; }

        public bool GiftWrap { get; set; }

        public ShoppingCartDetails(long product_Id, string product_Name, string categoryName, short quantity, decimal price, bool giftWrap)
        {
            Product_Id = product_Id;
            Product_Name = product_Name;
            CategoryName = categoryName;
            Quantity = quantity;
            Price = price;
            GiftWrap = giftWrap;
        }


        public override string ToString()
        {
            string strShoppingCartDetails;

            strShoppingCartDetails =
                "[ productName = " + Product_Name + " | " +
                "quantity = " + Quantity + " | " +
                "price = " + Price + " ] ";

            return strShoppingCartDetails;
        }

        public override bool Equals(object obj)
        {
            var details = obj as ShoppingCartDetails;
            return details != null &&
                   Product_Id == details.Product_Id &&
                   Product_Name == details.Product_Name &&
                   CategoryName == details.CategoryName &&
                   Quantity == details.Quantity &&
                   Price == details.Price &&
                   GiftWrap == details.GiftWrap;
        }

        public override int GetHashCode()
        {
            var hashCode = 852866481;
            hashCode = hashCode * -1521134295 + Product_Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Product_Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CategoryName);
            hashCode = hashCode * -1521134295 + Quantity.GetHashCode();
            hashCode = hashCode * -1521134295 + Price.GetHashCode();
            hashCode = hashCode * -1521134295 + GiftWrap.GetHashCode();
            return hashCode;
        }
    }
}
