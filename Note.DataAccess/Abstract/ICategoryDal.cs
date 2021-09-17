using Note.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.DataAccess.Abstract
{
    public interface ICategoryDal
    {
        void AddCategory(string CategoryName);
        void DeleteCategory(int Id);
        List<Category> ListCategory();
    }
}
