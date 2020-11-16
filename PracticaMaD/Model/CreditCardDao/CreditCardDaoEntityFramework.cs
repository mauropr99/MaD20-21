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

        public CreditCard FindById(long id)
        {
            CreditCard creditCard = null;

            #region Option 1: Using Linq.

            DbSet<CreditCard> creditCards = Context.Set<CreditCard>();

            var result =
                (from u in creditCards
                 where u.id == id
                 select u);

            creditCard = result.FirstOrDefault();

            #endregion Option 1: Using Linq.

           
            if (creditCard == null)
                throw new InstanceNotFoundException(id,
                    typeof(CreditCard).FullName);

            return creditCard;
        }

        public ICollection<CreditCard> FindCreditCardsByUserLogin(string login)
        {
            throw new NotImplementedException();
        }

        #endregion ICreditCardDao Members
    }
}
