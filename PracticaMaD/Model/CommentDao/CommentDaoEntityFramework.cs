using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.PracticaMaD.Model.Util;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
    public class CommentDaoEntityFramework :
        GenericDaoEntityFramework<Comment, Int64>, ICommentDao
    {
        public List<Comment> FindCommentsByProductId(long productId, int startIndex, int count)
        {
            #region Using Linq.

            string cacheObjectName = "FindCommentsByProductId" + productId + startIndex + count;
            var cachedObject = CacheUtil.GetFromCache<List<Comment>> (cacheObjectName);

            if (cachedObject == null)
            {

                DbSet<Comment> comments = Context.Set<Comment>();

                List<Comment> result =
                    (from c in comments
                     where c.productId == productId
                     orderby c.commentDate descending
                     select c).Skip(startIndex).Take(count).ToList();

                CacheUtil.AddToCache<List<Comment>> (cacheObjectName, result);

                return result;

            }

            return cachedObject;
            #endregion Using Linq.
        }

        public Comment FindCommentsByUserId(long productId,long userId)
        {
            #region Using Linq.

            DbSet<Comment> comments = Context.Set<Comment>();

            Comment result =
                (from c in comments
                 where c.productId == productId && c.userId == userId 
                 select c).FirstOrDefault();

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

