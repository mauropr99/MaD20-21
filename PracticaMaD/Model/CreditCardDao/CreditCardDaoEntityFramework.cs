using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;

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

        public List<CreditCard> FindCreditCardsByUserId(long userId)
        {
            #region Option 1: Using Linq.

            DbSet<User> users = Context.Set<User>();

            List<CreditCard> result =
                (from u in users
                 where u.id == userId
                 select u.CreditCards).FirstOrDefault().ToList();

            return result;

            #endregion Option 1: Using Linq
        }

        public void AddUser(User user, long creditCardId)
        {
            DbSet<CreditCard> creditCards = Context.Set<CreditCard>();

            var query = this.Find(creditCardId);

            query.User_Table.Add(user);

            this.Update(query);
        }


        #endregion ICreditCardDao Members
    }
}
