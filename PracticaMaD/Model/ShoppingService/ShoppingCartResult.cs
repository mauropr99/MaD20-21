using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    [Serializable()]
    public class ShoppingCartResult
    {

        public ShoppingCartResult(long id, string postalAddress, System.DateTime orderDate,
            decimal totalPrice, ICollection<OrderLine> orderLines)
        {
            this.Id = id;
            this.PostalAddress = postalAddress;
            this.OrderDate = orderDate;
            this.TotalPrice = totalPrice;
            this.OrderLines = orderLines;
        }

        #region Properties Region

        public long Id { get; private set; }

        public string PostalAddress { get; private set; }

        public System.DateTime OrderDate { get; private set; }

        public decimal TotalPrice { get; private set; }

        public ICollection<OrderLine> OrderLines { get; private set; }


        #endregion Properties Region

        public override bool Equals(object obj)
        {
            ShoppingCartResult target = (ShoppingCartResult)obj;

            return (this.Id == target.Id)
                   && (this.PostalAddress == target.PostalAddress)
                   && (this.OrderDate == target.OrderDate)
                   && (this.TotalPrice == target.TotalPrice)
                   && (this.OrderLines.Equals(target.OrderLines));
        }


        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            string strLoginResult;

            strLoginResult =
                "[ PostalAddress = " + PostalAddress + " | " +
                "OrderDate = " + OrderDate + " | " +
                "TotalPrice = " + TotalPrice + " ]";

            return strLoginResult;
        }

    }
}