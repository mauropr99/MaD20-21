using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
    public interface ICommentDao : IGenericDao<Comment, Int64>
    {
        /// <summary>
        /// Returns a list of comments pertaining to a given product identifier.
        /// </summary>
        /// <param name="productId">the product identifier </param>
        /// <param name="startIndex">the index of the first comment to return (starting in 0)</param>
        /// <param name="count">the maximum number of commmets to return</param>
        /// <returns>the list of comments</returns>
        List<Comment> FindByProductId(long productId, int startIndex, int count);
        List<Label> FindLabelsByCommentId(long commentId);
    }
}
