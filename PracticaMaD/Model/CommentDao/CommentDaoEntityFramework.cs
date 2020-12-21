using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
    public class CommentDaoEntityFramework :
        GenericDaoEntityFramework<Comment, Int64>, ICommentDao
    {
        public List<Comment> FindByProductId(long productId, int startIndex, int count)
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


        public void AddLabel(Label label, long commentId)
        {

            var query = this.Find(commentId);

            query.Labels.Add(label);

            this.Update(query);
        }

    }
}

