using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.LabelDao
{
    public class LabelDaoEntityFramework :
        GenericDaoEntityFramework<Label, Int64>, ILabelDao
    {
        #region ILabelDao Members. Specific Operations

        public Boolean ExistByName(string labelName)
        {
            #region Using Linq.

            DbSet<Label> labels = Context.Set<Label>();

            var result =
               (from l in labels
                where l.lab == labelName
                select l);

            return result != null;

            #endregion Using Linq.
        }

        public List<Label> FindByCommentId(long commentId)
        {
            #region Using Linq.

            DbSet<Label> labels = Context.Set<Label>();

            List<Label> result =
                (from l in labels
                 where l.id == commentId
                 orderby l.lab
                 select l).ToList();


            return result;

            #endregion Using Linq.
        }

        #endregion ILabelDao Members. Specific Operations

    }
}
