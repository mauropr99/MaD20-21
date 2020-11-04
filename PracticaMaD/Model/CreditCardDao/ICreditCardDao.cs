using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.CreditCardDao
{
    public interface ICreditCardDao : IGenericDao<CreditCard, Int64>
    {
        CreditCard FindById(long id);
    }
}
