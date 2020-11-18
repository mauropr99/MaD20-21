using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public interface ICommentService
    {
        ICommentDao CommentDao { set; }

        [Transactional]
        Comment NewComment(long userId, long productId, string text, List<Label> labels);

        [Transactional]
        Comment UpdateComment(long commentId, string text, List<Label> labels);

        [Transactional]
        Comment RemoveComment(long commentId);

        [Transactional]
        CommentBlock ViewComments(long productId, int startIndex, int count);
    }
}
