using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;


namespace Es.Udc.DotNet.PracticaMaD.Model.LanguageDao
{
    public interface ILanguageDao : IGenericDao<Language, Int64>
    {
        Language FindByName(String name);
        ICollection<Language> FindLanguagesByCountry(String country);
    }
}
