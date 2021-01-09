using System;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.LanguageDao
{
    public interface ILanguageDao : IGenericDao<Language, Int64>
    {
        /// <exception cref="InstanceNotFoundException"></exception>
        Language FindByNameAndCountry(String languageName, string languageCountry);

        /// <exception cref="InstanceNotFoundException"></exception>
        Language FindByUserId(long userId);
    }
}
