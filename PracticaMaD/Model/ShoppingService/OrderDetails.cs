using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    [Serializable()]
    public class OrderDetails
    {
        #region Properties Region

        public long Id { get; private set; }

        public DateTime Date { get; private set; }

        public string Description { get; private set; }

        public decimal TotalPrice { get; private set; }

        #endregion Properties Region

        public OrderDetails(long id, DateTime date, string description, decimal totalPrice)
        {
            Id = id;
            Date = date;
            Description = description;
            TotalPrice = totalPrice;
        }

        public override bool Equals(object obj)
        {
            return obj is OrderDetails details &&
                   Id == details.Id &&
                   Date == details.Date &&
                   Description == details.Description &&
                   TotalPrice == details.TotalPrice;
        }

        public override int GetHashCode()
        {
            int hashCode = -1009457508;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + Date.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + TotalPrice.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            string strOrderDetails;

            strOrderDetails =
                "[ date = " + Date + " | " +
                "description = " + Description + " | " +
                "totalPrice = " + TotalPrice + " ] ";

            return strOrderDetails;
        }

    }
}
