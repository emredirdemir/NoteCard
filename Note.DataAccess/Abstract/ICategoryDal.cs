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
        Task AddCategory(Category category);
        Task DeleteCategory(int Id);
        Task<List<Category>> ListCategory();
        Task Update(Category category);
    }
}
