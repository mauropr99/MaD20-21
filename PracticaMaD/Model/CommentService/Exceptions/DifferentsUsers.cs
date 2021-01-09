using System;


namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService.Exceptions
{
    /// <summary>
    /// Public <c>ModelException</c> which captures the error 
    /// with the passwords of the users.
    /// </summary>
    [Serializable]
    public class DifferentsUsers : Exception
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="DifferentsUsers"/> class.
        /// </summary>
        /// <param name="userId"><c>user</c> that causes the error.</param>
        public DifferentsUsers(long userId)
            : base("The comment doesn't own to = " + userId)
        {
            UserId = userId;
        }

        public long UserId { get; private set; }
    }
}
