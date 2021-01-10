using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService.Exceptions
{
    /// <summary>
    /// Public <c>ModelException</c> which captures the error 
    /// with the passwords of the users.
    /// </summary>
    [Serializable]
    public class ProductAlreadyCommentedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="ProductAlreadyCommentedException"/> class.
        /// </summary>
        /// <param name="productId"><c>id of the product</c> that causes the error.</param>
        public ProductAlreadyCommentedException(long productId)
            : base("Product already commented by the user")
        {
            ProductId = productId;
        }

        public long ProductId { get; private set; }
    }
}
