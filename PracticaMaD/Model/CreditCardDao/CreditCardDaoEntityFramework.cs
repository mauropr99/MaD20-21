using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao
{
    /// <summary>
    /// Specific Operations for CreditCard
    /// </summary>
    public class CreditCardDaoEntityFramework :
        GenericDaoEntityFramework<CreditCard, Int64>, ICreditCardDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public CreditCardDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region ICreditCardDao Members. Specific Operations

        public List<CreditCard> FindCreditCardsByUserLogin(string login)
        {
            #region Option 1: Using Linq.

            DbSet<User> users = Context.Set<User>();

            User userResult =
                (from u in users
                 where u.login == login
                 select u).First();

            

            DbSet<CreditCard> creditCards = Context.Set<CreditCard>();


            List<User> usersResult =
                (from c in creditCards
                 select c.User_Table).FirstOrDefault().ToList();

            var usersIds = usersResult.Select(s => s.id).ToList();


            List<CreditCard> result =
                (from c in creditCards
                 where usersIds.Contains(userResult.id)
                 orderby c.id
                 select c).ToList();

            return result;

            #endregion Option 1: Using Linq
        }

        #endregion ICreditCardDao Members
    }
}
