using System;


namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions
{
    public class NotEnoughStock : Exception
    {
        public NotEnoughStock(String productName, int stock)
            : base("Not enough stock of product = " + productName + "/ available stock = " + stock)
        {
            this.ProductName = productName;
            this.Stock = stock;
        }

        public String ProductName { get; private set; }

        public int Stock { get; private set; }
    }
}
