using System;


namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService.Exceptions
{
    /// <summary>
    /// Public <c>ModelException</c> which captures the error 
    /// with the passwords of the users.
    /// </summary>
    public class DifferentPrice : Exception
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="DifferentPrice"/> class.
        /// </summary>
        /// <param name="productPrice"><c>price</c> that causes the error.</param>
        ///  /// <param name="productName"><c>product</c> that causes the error.</param>
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
