using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.PracticaMaD.Model.Util;

namespace Es.Udc.DotNet.PracticaMaD.Model.BookDao
{
    public class BookDaoEntityFramework :
        GenericDaoEntityFramework<Book, Int64>, IBookDao
    {
        #region IProductDao Members

       
        #endregion Members
    }
}
