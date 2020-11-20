using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    [Serializable()]
    public class OrderLineDetails
    {
        #region Properties Region

        public string Product_Name { get; private set; }

        public short Quantity { get; private set; }

        public decimal Price { get; private set; }

        public OrderLineDetails(string product_Name, short quantity, decimal price)
        {
            Product_Name = product_Name;
            Quantity = quantity;
            Price = price;
        }

        public override bool Equals(object obj)
        {
            return obj is OrderLineDetails details &&
                   Product_Name == details.Product_Name &&
                   Quantity == details.Quantity &&
                   Price == details.Price;
        }

        public override int GetHashCode()
        {
            int hashCode = 1293590212;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Product_Name);
            hashCode = hashCode * -1521134295 + Quantity.GetHashCode();
            hashCode = hashCode * -1521134295 + Price.GetHashCode();
            return hashCode;
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

        #endregion Properties Region
    }

}
