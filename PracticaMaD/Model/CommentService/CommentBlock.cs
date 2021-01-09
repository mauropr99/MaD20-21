using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public class CommentBlock
    {

        public List<CommentDetails> Comments { get; private set; }
        public bool ExistMoreComments { get; private set; }

        public CommentBlock(List<CommentDetails> comments, bool existMoreComments)
        {
            Comments = comments;
            ExistMoreComments = existMoreComments;
        }
    }
}
