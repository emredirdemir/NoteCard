using Microsoft.AspNetCore.Mvc;
using Note.Business.Abstract;
using Note.Entities.Concrete;
using Note.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.UI.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            CategoryViewModel model = new CategoryViewModel
            {

                categories = await Task.Run(() => _service.GetCategories())
            };
            return View(model);
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            _service.AddCategory(category);
            return RedirectToAction("Index", "Note");
        }

        public IActionResult Delete(Category category)
        {
            _service.Delete(category);
            return RedirectToAction("Index", "Note");
        }

        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(Category category)
        {

            _service.Update(category);
            return RedirectToAction("Index", "Note");
        }
    }
}
