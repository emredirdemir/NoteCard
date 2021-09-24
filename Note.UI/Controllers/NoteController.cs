using Microsoft.AspNetCore.Mvc;
using Note.Business.Abstract;
using Note.Entities;
using Note.Entities.Concrete;
using Note.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.UI.Controllers
{
    public class NoteController : Controller
    {
        private INoteService _service;
        private ICategoryService _categoryService;

        public NoteController(INoteService service, ICategoryService categoryService)
        {
            _service = service;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            const int itemCount = 6;
            decimal totalItem = _service.CountOfNote();
            int PageCount = (int)Math.Ceiling(totalItem / itemCount);
            NoteViewModel model = new NoteViewModel
            {
                noteCards = await Task.Run(() => _service.GetAll(itemCount, page)),
                MyPage = new MyPage()
                {
                    CurrentPage = page,
                    PageCount = PageCount
                }
            };
            return View(model);
        }

        public async Task<IActionResult> GetByCategoryId(int Id, int page=1)
        {
            ViewBag.SelectedCategory = Id;
            const int itemCount = 6;

            decimal totalItem = _service.GetByCountWithCategory(Id);

            int PageCount = (int)Math.Ceiling(totalItem / itemCount);
            NoteViewModel model = new NoteViewModel
            {
                noteCards = await Task.Run(() => _service.GetByCategoryId(Id, itemCount, page)),
                MyPage = new MyPage()
                {
                    CurrentPage = page,
                    PageCount = PageCount
                }
            };
            return View(model);
        }
        public async Task<IActionResult> Update(int Id)
        {
            NoteViewModel model = new NoteViewModel
            {
                noteCard = await Task.Run(() => _service.GetNote(Id)),
                categories = await Task.Run(() => _categoryService.GetCategories())
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(NoteCard noteCard)
        {
            _service.Update(noteCard);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Create()
        {
            NoteViewModel model = new NoteViewModel()
            {
                categories = await Task.Run(() => _categoryService.GetCategories())
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NoteCard noteCard)
        {
            _service.Create(noteCard);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int Id)
        {
            _service.Delete(Id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int Id)
        {

            NoteViewModel noteCard = new NoteViewModel
            {
                noteCard = await Task.Run(() => _service.GetNote(Id))
            };

            return View(noteCard);
        }
    }
}
