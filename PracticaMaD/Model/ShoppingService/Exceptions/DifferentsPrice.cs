using System;


namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions
{
    public class DifferentPrice : Exception
    {
        public DifferentPrice(decimal productPrice, String productName)
            : base("The price of = " + productName + " has been modified after you added this product in the shopping cart: Actual price = " + productPrice)
        {
            this.ProductPrice = productPrice;
            this.ProductName = productName;
        }

        public decimal ProductPrice { get; private set; }
        public String ProductName { get; private set; }

    }
}
