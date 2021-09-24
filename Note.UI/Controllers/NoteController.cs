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
    public class NoteController : Controller
    {
        private INoteService _service;
        private ICategoryService _categoryService;

        public NoteController(INoteService service, ICategoryService categoryService)
        {
            _service = service;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            NoteViewModel model = new NoteViewModel
            {
                noteCards = await Task.Run(() => _service.GetAll())
            };
            return View(model);
        }

        public async Task<IActionResult> GetByCategoryId(int Id)
        {
            ViewBag.SelectedCategory = Id;
            NoteViewModel model = new NoteViewModel
            {
                noteCards = await _service.GetByCategoryId(Id),

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
