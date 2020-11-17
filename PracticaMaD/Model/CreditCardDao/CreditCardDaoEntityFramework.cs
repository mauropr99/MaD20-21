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

        public ICollection<CreditCard> FindCreditCardsByUserLogin(string login)
        {

            #region Option 1: Using Linq.
            DbSet<User> users = Context.Set<User>();

            var userResult =
                (from u in users
                 where u.login == login
                 select u).First();


            DbSet<CreditCard> creditCards = Context.Set<CreditCard>();
            ICollection<CreditCard> foundCreditCards;

            var result =
                (from c in creditCards
                 where c.User_Table.Contains(userResult)
                 select c);

            foundCreditCards = result.ToArray();

            #endregion Option 1: Using Linq

            return foundCreditCards;
        }

        #endregion ICreditCardDao Members
    }
}
