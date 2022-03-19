using System.Linq;
using Diploma;
using Diploma.ViewModels.BoxingClubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Diploma.Controllers
{
    public class HomeController: Controller
    {
        private readonly BoxContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(BoxContext context,ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        // GET: /
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


       


        public IActionResult Competitions()
        {
            return View(_context.BoxingClubs.Select(m => new BoxingClubsViewModel
            {
                BoxingClubId = m.BoxingClubId,
                ClubName = m.ClubName,
                ClubAddress = m.ClubAddress
            }).ToList());
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = _context.BoxingClubs
                .Where(e => e.BoxingClubId == id)
                .Select(e => new BoxingClubsViewModel
                {
                    BoxingClubId = e.BoxingClubId,
                    ClubName = e.ClubName,
                    ClubAddress = e.ClubAddress
                }).FirstOrDefault();
            
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

    }
}