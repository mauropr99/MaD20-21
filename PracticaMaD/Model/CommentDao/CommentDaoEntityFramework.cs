using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
    class CommentDaoEntityFramework :
        GenericDaoEntityFramework<Comment, Int64>, ICommentDao
    {
        List<Comment> FindByProductId(long productId)
        {
            #region Using Linq.

            DbSet<Comment> comments = Context.Set<Comment>();

            var result =
                (from c in comments
                 where c.productId == productId
                 orderby c.commentDate
                 select c).Skip(0).ToList();

            return result;

            #endregion Using Linq.
        }

        List<Comment> ICommentDao.FindByProductId(long productId)
        {
            throw new NotImplementedException();
        }
    }
}
