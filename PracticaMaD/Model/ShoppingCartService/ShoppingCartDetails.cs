﻿using System;
using Es.Udc.DotNet.PracticaMaD.Model;
using System.Collections.Generic;


namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingCartService
{

    [Serializable()]
    public class ShoppingCartDetails
    {
        #region Properties Region

        public decimal TotalPrice { get; private set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }

        #endregion

        public ShoppingCartDetails()
        {
            this.TotalPrice = 0;
            this.OrderLines = OrderLines;
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
    }
}
