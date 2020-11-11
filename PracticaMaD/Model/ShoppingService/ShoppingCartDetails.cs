using System;
using System.Collections.Generic;


namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{

    [Serializable()]
    public class ShoppingCartDetails
    {
        #region Properties Region

        public long UserId { get; set; }

        public decimal TotalPrice { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }

        #endregion

        public ShoppingCartDetails()
        {
            this.TotalPrice = 0;
            this.OrderLines = orderLines;
        }

        public ShoppingCartDetails(decimal totalPrice, ICollection<OrderLine> orderLines)
        {
            this.TotalPrice = totalPrice;
            this.OrderLines = orderLines;
        }

        public override bool Equals(object obj)
        {

            ShoppingCartDetails target = (ShoppingCartDetails)obj;

            return (this.TotalPrice == target.TotalPrice)
                  && (this.OrderLines.Equals(target.OrderLines));

        }

        public override int GetHashCode()
        {
            var hashCode = -1395028349;
            hashCode = hashCode * -1521134295 + UserId.GetHashCode();
            hashCode = hashCode * -1521134295 + TotalPrice.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<ICollection<OrderLine>>.Default.GetHashCode(OrderLines);
            return hashCode;
        }
    }
}
