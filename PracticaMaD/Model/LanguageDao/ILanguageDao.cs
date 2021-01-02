using System;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.LanguageDao
{
    public interface ILanguageDao : IGenericDao<Language, Int64>
    {
        Language FindByNameAndCountry(String languageName, string languageCountry);

        Language FindByUserId(long userId);
    }
}
