using Es.Udc.DotNet.ModelUtil.Dao;

namespace Es.Udc.DotNet.PracticaMaD.Model.BookDao
{
    public class BookDaoEntityFramework :
        GenericDaoEntityFramework<Book, long>, IBookDao
    {
        #region IProductDao Members


        #endregion Members
    }
}
