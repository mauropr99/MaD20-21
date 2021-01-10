using System;


namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions
{
    /// <summary>
    /// Public <c>ModelException</c> which captures the error 
    /// with the passwords of the users.
    /// </summary>
    [Serializable]
    public class NotEnoughStock : Exception
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="NotEnoughStock"/> class.
        /// </summary>
        /// <param name="productName"><c>name of product</c> that causes the error.</param>
        /// <param name="stock"><c>stock</c> that causes the error.</param>
        /// <param name="lineQuantity"><c>quantity of a product</c> that causes the error.</param>
        public NotEnoughStock(string productName, int stock, int lineQuantity)
            : base("Not enough stock of product = " + productName + "/ available stock = " + stock)
        {
            ProductName = productName;
            Stock = stock;
            LineQuantity = lineQuantity;
        }

        public string ProductName { get; private set; }

        public int Stock { get; private set; }

        public int LineQuantity { get; private set; }
    }
}
