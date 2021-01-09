using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
    public class CommentDaoEntityFramework :
        GenericDaoEntityFramework<Comment, long>, ICommentDao
    {
        public List<Comment> FindCommentsByProductId(long productId, int startIndex, int count)
        {
            #region Using Linq.

            DbSet<Comment> comments = Context.Set<Comment>();

            List<Comment> result =
                (from c in comments
                 where c.productId == productId
                 orderby c.commentDate descending
                 select c).Skip(startIndex).Take(count).ToList();

            return result;


            #endregion Using Linq.
        }

        /// <exception cref="InstanceNotFoundException"></exception>
        public Comment FindCommentsByUserId(long productId, long userId)
        {
            #region Using Linq.

            DbSet<Comment> comments = Context.Set<Comment>();

            Comment result =
                (from c in comments
                 where c.productId == productId && c.userId == userId
                 select c).FirstOrDefault();

            if (result == null)
                throw new InstanceNotFoundException("",
                    typeof(Comment).FullName);

            return result;

            #endregion Using Linq.
        }

        /// <exception cref="InstanceNotFoundException"></exception>
        public void AddLabel(Label label, long commentId)
        {
            var query = Find(commentId);

            query.Labels.Add(label);

            Update(query);
        }

        /// <exception cref="InstanceNotFoundException"></exception>
        public void RemoveLabel(Label label, long commentId)
        {
            var query = Find(commentId);

            query.Labels.Remove(label);

            Update(query);
        }

    }
}

