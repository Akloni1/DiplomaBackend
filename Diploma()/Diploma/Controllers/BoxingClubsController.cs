using System.Linq;
using Diploma.ViewModels.BoxingClubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Diploma.Controllers
{
    public class BoxingClubsController : Controller
    {
         private readonly BoxContext _context;
        public readonly ILogger<HomeController> _logger;

        public BoxingClubsController(BoxContext context, ILogger<HomeController> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
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
        
        
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ClubName,ClubAddress")] BoxingClubsViewModel inputModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(new BoxingClubs
                {
                   
                    ClubName = inputModel.ClubName,
                    ClubAddress = inputModel.ClubAddress
                });
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(inputModel);
        }
        private bool BoxingClubExists(int id)
        {
            return _context.BoxingClubs.Any(e => e.BoxingClubId == id);
        }

        
        
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editModel = _context.BoxingClubs.Where(e => e.BoxingClubId == id).Select(e => new EditBoxingClubsViewModel
            {
                ClubName = e.ClubName,
                ClubAddress = e.ClubAddress
            }).FirstOrDefault();
            if (editModel == null)
            {
                return NotFound();
            }

            return View(editModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ClubName,ClubAddress")] EditBoxingClubsViewModel editModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var boxingClub = new BoxingClubs
                    {
                        BoxingClubId = id,
                        ClubName = editModel.ClubName,
                        ClubAddress = editModel.ClubAddress
                       
                    };
                    _context.Update(boxingClub);
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (!BoxingClubExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            
            return View(editModel);
        }
        
        
        
        [HttpGet]
        // GET: Boxer/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deleteModel = _context.BoxingClubs.Where(m => m.BoxingClubId == id).Select(m => new DeleteBoxingClubsViewModel
            {
                ClubName = m.ClubName,
                ClubAddress = m.ClubAddress
            }).FirstOrDefault();
            
            if (deleteModel == null)
            {
                return NotFound();
            }

            return View(deleteModel);
        }
        
        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var boxingClub = _context.BoxingClubs.Find(id);
            _context.BoxingClubs.Remove(boxingClub);
            _context.SaveChanges();
            _logger.LogError($"BoxingClub with id {boxingClub.BoxingClubId} has been deleted!");
            return RedirectToAction(nameof(Index));
        }
    }
}