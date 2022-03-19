using System.Linq;
using Diploma;
using Diploma.ViewModels.Boxers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Diploma.Controllers
{
    public class BoxersController: Controller
    {
        private readonly BoxContext _context;
        public readonly ILogger<HomeController> _logger;

        public BoxersController(BoxContext context, ILogger<HomeController> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_context.Boxers.Select(m => new BoxerViewModel
            {
                BoxerId = m.BoxerId,
                FirstName = m.FirstName,
                LastName = m.LastName,
                MiddleName = m.MiddleName
            }).ToList());
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = _context.Boxers
                .Where(e => e.BoxerId == id)
                .Select(e => new BoxerViewModel
                {
                    BoxerId = e.BoxerId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    MiddleName = e.MiddleName,
                    DateOfBirth = e.DateOfBirth,
                    TrainingExperience = e.TrainingExperience,
                    NumberOfFightsHeld = e.NumberOfFightsHeld,
                    NumberOfWins = e.NumberOfWins,
                    Discharge = e.Discharge,
                    CoachId = e.CoachId,
                    BoxingClubId = e.BoxingClubId
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
        public IActionResult Create([Bind("FirstName,LastName,MiddleName,DateOfBirth,TrainingExperience,NumberOfFightsHeld,NumberOfWins,Discharge,CoachId,BoxingClubId")] BoxerViewModel inputModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(new Boxers
                {
                   
                    FirstName = inputModel.FirstName,
                    LastName = inputModel.LastName,
                    MiddleName = inputModel.MiddleName,
                    DateOfBirth = inputModel.DateOfBirth,
                    TrainingExperience = inputModel.TrainingExperience,
                    NumberOfFightsHeld = inputModel.NumberOfFightsHeld,
                    NumberOfWins = inputModel.NumberOfWins,
                    Discharge = inputModel.Discharge,
                    CoachId = inputModel.CoachId,
                    BoxingClubId = inputModel.BoxingClubId
                });
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(inputModel);
        }
        private bool BoxerExists(int id)
        {
            return _context.Boxers.Any(e => e.BoxerId == id);
        }

        
        
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editModel = _context.Boxers.Where(e => e.BoxerId == id).Select(e => new EditBoxerViewModel
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                MiddleName = e.MiddleName,
                DateOfBirth = e.DateOfBirth,
                TrainingExperience = e.TrainingExperience,
                NumberOfFightsHeld = e.NumberOfFightsHeld,
                NumberOfWins = e.NumberOfWins,
                Discharge = e.Discharge,
                CoachId = e.CoachId,
                BoxingClubId = e.BoxingClubId
            }).FirstOrDefault();
            if (editModel == null)
            {
                return NotFound();
            }

            return View(editModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("FirstName,LastName,MiddleName,DateOfBirth,TrainingExperience,NumberOfFightsHeld,NumberOfWins,Discharge,CoachId,BoxingClubId")] EditBoxerViewModel editModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var boxer = new Boxers
                    {
                        BoxerId = id,
                        FirstName = editModel.FirstName,
                        LastName = editModel.LastName,
                        MiddleName = editModel.MiddleName,
                        DateOfBirth = editModel.DateOfBirth,
                        TrainingExperience = editModel.TrainingExperience,
                        NumberOfFightsHeld = editModel.NumberOfFightsHeld,
                        NumberOfWins = editModel.NumberOfWins,
                        Discharge = editModel.Discharge,
                        CoachId = editModel.CoachId,
                        BoxingClubId = editModel.BoxingClubId
                    };
                    _context.Update(boxer);
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (!BoxerExists(id))
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

            var deleteModel = _context.Boxers.Where(m => m.BoxerId == id).Select(m => new DeleteBoxerViewModel
            {
                FirstName = m.FirstName,
                LastName = m.LastName,
                MiddleName = m.MiddleName,
                DateOfBirth = m.DateOfBirth,
                TrainingExperience = m.TrainingExperience,
                NumberOfFightsHeld = m.NumberOfFightsHeld,
                NumberOfWins = m.NumberOfWins,
                Discharge = m.Discharge,
                CoachId = m.CoachId,
                BoxingClubId = m.BoxingClubId
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
            var boxer = _context.Boxers.Find(id);
            _context.Boxers.Remove(boxer);
            _context.SaveChanges();
            _logger.LogError($"Boxer with id {boxer.BoxerId} has been deleted!");
            return RedirectToAction(nameof(Index));
        }

      
    }
}