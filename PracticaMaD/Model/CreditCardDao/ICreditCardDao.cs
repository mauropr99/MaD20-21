﻿using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao
{
    public interface ICreditCardDao : IGenericDao<CreditCard, Int64>
    {
        CreditCard FindById(long id);

        ICollection<CreditCard> FindCreditCardsByUserLogin(string login);
    }
}
