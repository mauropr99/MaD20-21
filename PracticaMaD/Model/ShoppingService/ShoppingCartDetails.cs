using System;
using System.Collections.Generic;


namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{

    [Serializable()]
    public class ShoppingCartDetails
    {
        public long Product_Id { get; set; }

        public string Product_Name { get; set; }

        public short Quantity { get; set; }

        public decimal Price { get; set; }

        public bool GiftWrap { get; set; }

        public ShoppingCartDetails()
        {
        }

        public ShoppingCartDetails(long product_Id, string product_Name, short quantity, decimal price)
        {
            Product_Id = product_Id;
            Product_Name = product_Name;
            Quantity = quantity;
            Price = price;
            GiftWrap = false;
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
            return obj is ShoppingCartDetails details &&
                   Product_Id == details.Product_Id &&
                   Product_Name == details.Product_Name &&
                   Quantity == details.Quantity &&
                   Price == details.Price;
        }

        public override int GetHashCode()
        {
            var hashCode = 343921072;
            hashCode = hashCode * -1521134295 + Product_Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Product_Name);
            hashCode = hashCode * -1521134295 + Quantity.GetHashCode();
            hashCode = hashCode * -1521134295 + Price.GetHashCode();
            return hashCode;
        }
    }
}
