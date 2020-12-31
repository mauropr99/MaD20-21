using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;



namespace Es.Udc.DotNet.PracticaMaD.Model.BookDao
{
    public class BookDaoEntityFramework :
        GenericDaoEntityFramework<Book, Int64>, IBookDao
    {
        #region IProductDao Members

        public List<Book> FindByProductName(String product_name, int startIndex, int count)
        {
            #region Using Linq.

            DbSet<Book> products = Context.Set<Book>();

            List<Book> result =
                (from p in products
                 where p.product_name.Contains(product_name)
                 orderby p.product_name
                 select p).Skip(startIndex).Take(count).ToList();

            return result;

            #endregion Using Linq.
        }

        #endregion Members
    }
}
