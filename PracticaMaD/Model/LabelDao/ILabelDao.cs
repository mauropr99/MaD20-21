using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.LabelDao
{
    public interface ILabelDao : IGenericDao<Label, Int64>
    {
        Boolean ExistByName(string labelName);

        Label FindByLabelName(string labelName);

        List<Label> FindLabelsByCommentId(long commentId);

        List<Label> FindMostUsedLabel(int quantity);

    }
}
