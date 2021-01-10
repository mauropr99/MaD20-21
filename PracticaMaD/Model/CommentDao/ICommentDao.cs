using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
    public interface ICommentDao : IGenericDao<Comment, long>
    {
        /// <summary>
        /// Returns a list of comments pertaining to a given product identifier.
        /// </summary>
        /// <param name="productId">the product identifier </param>
        /// <param name="startIndex">the index of the first comment to return (starting in 0)</param>
        /// <param name="count">the maximum number of commmets to return</param>
        /// <returns>the list of comments</returns>
        List<Comment> FindCommentsByProductId(long productId, int startIndex, int count);

        /// <exception cref="InstanceNotFoundException"></exception>
        void AddLabel(Label label, long commentId);

        /// <exception cref="InstanceNotFoundException"></exception>
        void RemoveLabel(Label label, long commentId);

        /// <exception cref="InstanceNotFoundException"></exception>
        Comment FindCommentsByUserId(long productId, long userId);


    }
}
