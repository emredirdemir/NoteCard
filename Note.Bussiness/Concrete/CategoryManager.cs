using Note.Business.Abstract;
using Note.DataAccess.Abstract;
using Note.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void AddCategory(Category category)
        {
            _categoryDal.AddCategory(category);
        }

        public void Delete(Category category)
        {
            _categoryDal.DeleteCategory(category.Id);
        }

        public Task<List<Category>> GetCategories()
        {
            return _categoryDal.ListCategory();
        }

        public void Update(Category category)
        {
            _categoryDal.Update(category);
        }
    }
}
