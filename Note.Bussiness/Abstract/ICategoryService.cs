using Note.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Business.Abstract
{
    public interface ICategoryService
    {
        void AddCategory(Category category);
        void Delete(Category category);
        void Update(Category category);

        Task<List<Category>> GetCategories();
    }
}
