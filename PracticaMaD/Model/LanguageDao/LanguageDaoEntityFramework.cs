using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.Util;

namespace Es.Udc.DotNet.PracticaMaD.Model.LanguageDao
{
    /// <summary>
    /// Specific Operations for Language
    /// </summary>
    public class LanguageDaoEntityFramework :
        GenericDaoEntityFramework<Language, Int64>, ILanguageDao
    {
        #region ILanguageDao Members. Specific Operations
        /// <exception cref="InstanceNotFoundException"></exception>
        public Language FindByNameAndCountry(string languageName, string languageCountry)
        {
            Language language = null;

            #region Option 1: Using Linq.

            string cacheObjectName = "FindByNameAndCountry?languageName=" + languageName + "&languageCountry=" + languageCountry;
            var cachedObject = CacheUtil.GetFromCache<Language>(cacheObjectName);

            if (cachedObject == null)
            {

                DbSet<Language> languages = Context.Set<Language>();

                var result =
                    (from u in languages
                     where u.name == languageName &&
                        u.country == languageCountry
                     select u);

                language = result.FirstOrDefault();

                #endregion Option 1: Using Linq.

                if (language == null)
                    throw new InstanceNotFoundException(languageName,
                        typeof(Language).FullName);

                    CacheUtil.AddToCache<Language>(cacheObjectName, language);

                return language;
            }

            return cachedObject;
        }

        /// <exception cref="InstanceNotFoundException"></exception>
        public Language FindByUserId(long userId)
        {
            #region Using Linq.

            DbSet<User> users = Context.Set<User>();

            Language result =
                (from u in users
                 where u.id == userId
                 select u.Language).FirstOrDefault();

            if (result == null)
                throw new InstanceNotFoundException("",
                    typeof(Language).FullName);

            return result;

            #endregion Using Linq.
        }

        #endregion ILanguageDao Members
    }
}
