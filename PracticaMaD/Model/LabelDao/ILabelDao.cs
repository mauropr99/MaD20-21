using System;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.LabelDao
{
    public interface ILabelDao: IGenericDao<Label, Int64>
    {
        Boolean ExistByName(string labelName);

        Label FindByLabelName(string labelName);

        List<Label> FindLabelsByCommentId(long commentId);

    }
}
