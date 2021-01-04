using System;


namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions
{
    public class NotEnoughStock : Exception
    {
        public NotEnoughStock(String productName, int stock, int lineQuantity)
            : base("Not enough stock of product = " + productName + "/ available stock = " + stock)
        {
            this.ProductName = productName;
            this.Stock = stock;
            this.LineQuantity = lineQuantity;
        }

        public String ProductName { get; private set; }

        public int Stock { get; private set; }

        public int LineQuantity { get; private set; }
    }
}
