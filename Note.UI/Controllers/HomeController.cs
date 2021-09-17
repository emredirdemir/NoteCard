using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Note.Business.Abstract;
using Note.UI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Note.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly INoteService _noteService; 
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, INoteService noteService)
        {
            _logger = logger;
            _noteService = noteService;
        }

        public async Task<IActionResult> Index()
        {
            NoteViewModel model = new NoteViewModel
            {
                noteCards = await Task.Run(() => _noteService.GetAll())
            };


            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
