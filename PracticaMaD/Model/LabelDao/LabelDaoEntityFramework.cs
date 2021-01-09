using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.Util;

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
                select l).FirstOrDefault(); ;

            return result != null;

            #endregion Using Linq.
        }

        /// <exception cref="InstanceNotFoundException"></exception>
        public Label FindByLabelName(string labelName)
        {
            #region Option 1: Using Linq.

            DbSet<Label> labels = Context.Set<Label>();

            Label result =
                (from u in labels
                    where u.lab == labelName
                    select u).FirstOrDefault();

            if (result == null)
                throw new InstanceNotFoundException(labelName,
                    typeof(Label).FullName);


            return result;


            #endregion Option 1: Using Linq.

        }

        public List<Label> FindLabelsByCommentId(long commentId)
        {

            DbSet<Comment> comments = Context.Set<Comment>();

            var result =
                (from c in comments
                 where c.id == commentId
                 orderby c.id
                 select c.Labels).FirstOrDefault().ToList();

            return result;
        }

        public List<Label> FindMostUsedLabel(int quantity)
        {
            DbSet<Label> labels = Context.Set<Label>();

            var result =
                (from l in labels
                 orderby l.timesUsed descending, l.lab ascending
                 select l).Take(quantity).ToList();

            return result;
        }
    }

    #endregion ILabelDao Members. Specific Operations

}

