using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryDao
{
    public interface ICategoryDao : IGenericDao<Category, long>
    {
        /// <exception cref="InstanceNotFoundException"></exception>
        Category FindByName(string name);
    }
}
