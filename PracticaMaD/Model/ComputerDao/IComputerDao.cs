using Es.Udc.DotNet.ModelUtil.Dao;

using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ComputerDao
{
    public interface IComputerDao :IGenericDao<Computer, Int64>

    {
        List<Computer> FindByProductName(String product_name, int startIndex, int count);


    }
}
