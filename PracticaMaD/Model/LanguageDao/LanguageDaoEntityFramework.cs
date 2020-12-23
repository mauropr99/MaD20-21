using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Es.Udc.DotNet.PracticaMaD.Model.LanguageDao
{
    /// <summary>
    /// Specific Operations for Language
    /// </summary>
    public class LanguageDaoEntityFramework :
        GenericDaoEntityFramework<Language, Int64>, ILanguageDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public LanguageDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region ILanguageDao Members. Specific Operations

        public Language FindByNameAndCountry(string languageName, string languageCountry)
        {
            Language language = null;

            #region Option 1: Using Linq.

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

            return language;
        }

        public List<Language> FindLanguagesByCountry(string country)
        {

            List<Language> language = null;

            #region Option 1: Using Linq.

            DbSet<Language> languages = Context.Set<Language>();

            var result =
                (from u in languages
                 where u.country == country
                 select u).Skip(0).ToList();


            language = result.ToList();

            #endregion Option 1: Using Linq.

            if (language == null)
                throw new InstanceNotFoundException(country,
                    typeof(Language).FullName);

            return result;
        }

        public Language FindByUserId(long userId)
        {
            #region Using Linq.

            DbSet<User> users = Context.Set<User>();

            Language result =
                (from u in users
                 where u.id == userId
                 select u.Language).FirstOrDefault();


            return result;

            #endregion Using Linq.
        }

        #endregion ILanguageDao Members
    }
}
