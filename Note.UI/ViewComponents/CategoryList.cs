using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Note.Business.Abstract;
using Note.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.UI.ViewComponents
{
    public class CategoryList:ViewComponent
    {
        private ICategoryService _service;

        public CategoryList(ICategoryService service)
        {
            _service = service;
        }

        public async Task<ViewViewComponentResult> InvokeAsync()
        {
            CategoryViewModel model = new CategoryViewModel
            {
                categories = await Task.Run(() => _service.GetCategories())
            };
            return View(model);
        }
    }
}
