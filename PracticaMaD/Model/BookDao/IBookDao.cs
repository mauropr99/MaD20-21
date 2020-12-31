using System;
using System.Collections.Generic;
using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.BookDao
{
    public interface IBookDao : IGenericDao<Book, Int64>

    {
        List<Book> FindByProductName(String product_name, int startIndex, int count);


    }
}
