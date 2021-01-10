using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.LabelDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public interface ICommentService
    {
        ICommentDao CommentDao { set; }
        ILabelDao LabelDao { set; }
        IUserDao UserDao { set; }

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="ProductAlreadyCommentedException"/>
        [Transactional]
        Comment NewComment(long userId, long productId, string text, List<string> labels);

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        Comment UpdateComment(long userId, long commentId, string text, List<string> labels);

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void RemoveComment(long userId, long commentId);

        [Transactional]
        CommentBlock ViewComments(long productId, int startIndex, int count);

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        CommentDetails FindComment(long commentId);

        /// <exception cref="InstanceNotFoundException"></exception>
        [Transactional]
        List<LabelDetails> ViewMostUsedLabels(int quantity);

        /// <exception cref="InstanceNotFoundException"></exception>
        [Transactional]
        bool UserAlreadyCommented(long productId, long userId);
    }
}
