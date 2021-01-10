using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.LabelDao
{
    public interface ILabelDao : IGenericDao<Label, long>
    {
        bool ExistByName(string labelName);

        /// <exception cref="InstanceNotFoundException"></exception>
        Label FindByLabelName(string labelName);

        List<Label> FindLabelsByCommentId(long commentId);

        List<Label> FindMostUsedLabel(int quantity);

    }
}
