using System;


namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService.Exceptions
{
    public class DifferentsUsers : Exception
    {
        public DifferentsUsers(long userId)
            : base("The comment doesn't own to = " + userId)
        {
            this.UserId = userId;
        }

        public long UserId { get; private set; }
    }
}
