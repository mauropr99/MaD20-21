using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryDao
{
    /// <summary>
    /// Specific Operations for Category
    /// </summary>
    public class CategoryDaoEntityFramework :
        GenericDaoEntityFramework<Category, Int64>, ICategoryDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public CategoryDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region ICategoryDao Members. Specific Operations

        /// <summary>
        /// Finds a Category by his name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public Category FindByName(string name)
        {
            Category category = null;

            #region Option 1: Using Linq.

            DbSet<Category> categories = Context.Set<Category>();

            var result =
                (from u in categories
                 where u.name == name
                 select u);

            category = result.FirstOrDefault();

            #endregion Option 1: Using Linq.

            if (category == null)
                throw new InstanceNotFoundException(name,
                    typeof(Category).FullName);

            return category;
        }

        #endregion ICategoryDao Members
    }
}
