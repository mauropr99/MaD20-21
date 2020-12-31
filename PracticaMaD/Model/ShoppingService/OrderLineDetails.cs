using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    [Serializable()]
    public class OrderLineDetails
    {
        #region Properties Region

        public long Product_Id { get; private set; }

        public string Product_Name { get; private set; }

        public short Quantity { get; private set; }

        public decimal Price { get; private set; }

        public OrderLineDetails(long product_Id, string product_Name, short quantity, decimal price)
        {
            Product_Id = product_Id;
            Product_Name = product_Name;
            Quantity = quantity;
            Price = price;
        }



        public override string ToString()
        {
            string strOrderLineDetails;

            strOrderLineDetails =
                "[ productName = " + Product_Name + " | " +
                "quantity = " + Quantity + " | " +
                "price = " + Price + " ] ";

            return strOrderLineDetails;
        }

        public override bool Equals(object obj)
        {
            return obj is OrderLineDetails details &&
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

        #endregion Properties Region
    }

}
