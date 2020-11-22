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
                select l).FirstOrDefault(); ;

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

        public Label FindByLabelName(string labelName)
        {
            #region Option 1: Using Linq.

            DbSet<Label> labels = Context.Set<Label>();

            Label result =
                (from u in labels
                 where u.lab == labelName
                 select u).FirstOrDefault();


                #endregion Option 1: Using Linq.

                if (result == null)
                    throw new InstanceNotFoundException(labelName,
                        typeof(Label).FullName);

                return result;
        }

    #endregion ILabelDao Members. Specific Operations

}
}
