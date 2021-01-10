using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.LanguageDao
{
    public interface ILanguageDao : IGenericDao<Language, long>
    {
        /// <exception cref="InstanceNotFoundException"></exception>
        Language FindByNameAndCountry(string languageName, string languageCountry);

        /// <exception cref="InstanceNotFoundException"></exception>
        Language FindByUserId(long userId);
    }
}
