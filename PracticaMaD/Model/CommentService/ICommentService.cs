using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.LabelDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public interface ICommentService
    {
        ICommentDao CommentDao { set; }
        ILabelDao LabelDao { set; }
        IUserDao UserDao { set; }

        [Transactional]
        Comment NewComment(long userId, long productId, string text, List<string> labels);

        [Transactional]
        Comment UpdateComment(long userId, long commentId, string text, List<string> labels);

        [Transactional]
        void RemoveComment(long userId, long commentId);

        [Transactional]
        CommentBlock ViewComments(long userId, long productId, int startIndex, int count);

        [Transactional]
        List<LabelDetails> ViewMostUsedLabels(int quantity);


    }
}
