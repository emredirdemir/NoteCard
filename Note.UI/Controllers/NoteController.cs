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

        public NoteController(INoteService service)
        {
            _service = service;
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
            NoteViewModel model = new NoteViewModel
            {
                noteCards = await _service.GetByCategoryId(Id)
            };
            return View(model);
        }
        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Update(NoteCard noteCard)
        {
            _service.Update(noteCard);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(NoteCard noteCard)
        {
            _service.Create(noteCard);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int Id)
        {
            _service.Delete(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
