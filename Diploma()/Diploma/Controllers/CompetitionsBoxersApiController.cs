using AutoMapper;
using Diploma.ViewModels.Boxers;
using Diploma.ViewModels.Competitions;
using Diploma.ViewModels.CompetitionsBoxers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma.Controllers
{
    [Route("api/competitions/boxers")]
    [ApiController]
    public class CompetitionsBoxersApiController : ControllerBase
    {

        private readonly BoxContext _context;
        private readonly IMapper _mapper;

        public CompetitionsBoxersApiController(BoxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet("{id}")]  // GET: /api/competitions/boxer/1
        [ProducesResponseType(200, Type = typeof(IEnumerable<BoxerViewModel>))]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<BoxerViewModel>> GetBoxersByIdCompetition(int id)
        {
            var competitionsBoxers = _mapper.Map<IEnumerable<CompetitionsBoxers>, IEnumerable<CompetitionsBoxersViewModel>>(_context.CompetitionsBoxers.Where(a => a.CompetitionsId == id).ToList());
            var idBoxers = competitionsBoxers.Select(h => h.BoxerId).ToList();
            List<IEnumerable<BoxerViewModel>> boxers = new List<IEnumerable<BoxerViewModel>>();

            foreach (int BoxerId in idBoxers)
            {
                boxers.Add(_mapper.Map<IEnumerable<Boxers>, IEnumerable<BoxerViewModel>>(_context.Boxers.Where(a => a.BoxerId == BoxerId).ToList()));
            }

            IEnumerable<BoxerViewModel> boxersConcat = null;
            if (boxers.Count>0)
            {
                 boxersConcat = boxers[0];
                for (int i = 0; i < boxers.Count - 1; i++)
                {
                    boxersConcat = (boxersConcat ?? Enumerable.Empty<BoxerViewModel>()).Concat(boxers[i + 1] ?? Enumerable.Empty<BoxerViewModel>());
                }
            }
            else return NotFound();

            return Ok(boxersConcat);
        }





        [HttpPost] // POST: /api/competitions/boxer
        public ActionResult<InputCompetitionsBoxersViewModel> PostCompetition(InputCompetitionsBoxersViewModel inputModel)
        {

            var competitionBoxer = _context.Add(_mapper.Map<CompetitionsBoxers>(inputModel)).Entity;
            _context.SaveChanges();

            return CreatedAtAction("GetById", new { id = competitionBoxer.CompetitionsId }, _mapper.Map<InputCompetitionsBoxersViewModel>(inputModel));
        }



           [HttpDelete("{id}")] // DELETE: /api/competitions/boxer/1
        public ActionResult<DeleteCompetitionsBoxersViewModel> DeleteCompetitionBoxers(int id)
         {
             var boxers = _context.CompetitionsBoxers.Where(a=>a.CompetitionsId==id);
             if (boxers == null) return NotFound();
          
            _context.CompetitionsBoxers.RemoveRange(boxers);
       
             _context.SaveChanges();

            //  return Ok(_mapper.Map<DeleteCompetitionsBoxersViewModel>(boxers));
            return Ok(boxers);
        }



        [HttpDelete] // DELETE: /api/competitions/boxer
        public ActionResult<DeleteCompetitionsBoxersViewModel> DeleteBoxerParticipating([FromQuery] DeleteCompetitionsBoxersViewModel viewModel)
        {
            var boxer = _context.CompetitionsBoxers.Where(a => a.CompetitionsId == viewModel.CompetitionsId && a.BoxerId== viewModel.BoxerId);
            if (boxer == null) return NotFound();
            _context.CompetitionsBoxers.RemoveRange(boxer);
            _context.SaveChanges();
            return Ok(boxer);
        }
    }
}
