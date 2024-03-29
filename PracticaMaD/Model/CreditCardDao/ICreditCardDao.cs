﻿using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao
{
    public interface ICreditCardDao : IGenericDao<CreditCard, long>
    {
        List<CreditCard> FindCreditCardsByUserId(long userId);

        /// <exception cref="InstanceNotFoundException"></exception>
        void AddUser(User user, long creditCardId);
    }
}
