using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao
{
    public interface ICreditCardDao : IGenericDao<CreditCard, Int64>
    {
        List<CreditCard> FindCreditCardsByUserId(long userId);

        void AddUser(User user, long creditCardId);
    }
}
