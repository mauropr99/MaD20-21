using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.LanguageDao
{
    public interface IUserDao : IGenericDao<Language, Int64>
    {
        Language FindByName(String name);
        List<Language> FindLanguagesByCountry(String country);
    }
}
