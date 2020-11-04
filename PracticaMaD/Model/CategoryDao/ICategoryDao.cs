using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryDao
{
    public interface IUserDao : IGenericDao<Category, Int64>
    {
        Category FindByName(String name);
    }
}
