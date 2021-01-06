using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService.Exceptions
{
    [Serializable]
    public class ProductAlreadyCommentedException : Exception
    {
        public ProductAlreadyCommentedException(long productId)
            : base("Product already commented by the user")
        {
            this.ProductId = productId;
        }

    public long ProductId { get; private set; }
}
}
