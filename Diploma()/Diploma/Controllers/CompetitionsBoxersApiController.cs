using AutoMapper;
using Diploma.ViewModels.Boxers;
using Diploma.ViewModels.Competitions;
using Diploma.ViewModels.CompetitionsBoxers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [Authorize]
        [HttpGet("{id}")]  // GET: /api/competitions/boxer/1 выводит боксеров которые учавтвуют в соревнованиях
        [ProducesResponseType(200, Type = typeof(IEnumerable<BoxerViewModel>))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<BoxerViewModel>>> GetBoxersByIdCompetition(int id)
        {
            var competitionsBoxers = _mapper.Map<IEnumerable<CompetitionsBoxers>, IEnumerable<CompetitionsBoxersViewModel>>(await _context.CompetitionsBoxers.Where(a => a.CompetitionsId == id).ToListAsync());
            var idBoxers = competitionsBoxers.Select(h => h.BoxerId).ToList();
            List<BoxerViewModel> boxers = new List<BoxerViewModel>();

            foreach (int BoxerId in idBoxers)
            {
                boxers.Add(_mapper.Map<BoxerViewModel>(await _context.Boxers.Where(a => a.BoxerId == BoxerId).FirstOrDefaultAsync()));
                // boxers.Add(_mapper.Map<IEnumerable<Boxers>, IEnumerable<BoxerViewModel>>( _context.Boxers.Where(a => a.BoxerId == BoxerId).ToListAsync()));
            }

            /* IEnumerable<BoxerViewModel> boxersConcat = null;
             if (boxers.Count>0)
             {
                  boxersConcat = boxers[0];
                 for (int i = 0; i < boxers.Count - 1; i++)
                 {
                     boxersConcat = (boxersConcat ?? Enumerable.Empty<BoxerViewModel>()).Concat(boxers[i + 1] ?? Enumerable.Empty<BoxerViewModel>());
                 }
             }
             //  else return NotFound();
             else
             {
                 List<BoxerViewModel> boxerViewModels = new List<BoxerViewModel>();
                 return boxerViewModels;
             }*/

            return Ok(boxers);
        }





        [Authorize(Roles = "admin,coach,boxer,lead")]
        [HttpPost] // POST: /api/competitions/boxer
        public async Task<ActionResult<InputCompetitionsBoxersViewModel>> PostCompetition(InputCompetitionsBoxersViewModel inputModel)
        {

            var competitionBoxer = _context.Add(_mapper.Map<CompetitionsBoxers>(inputModel)).Entity;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = competitionBoxer.CompetitionsId }, _mapper.Map<InputCompetitionsBoxersViewModel>(inputModel));
        }


        [Authorize(Roles = "admin,lead")]
        [HttpDelete("{id}")] // DELETE: /api/competitions/boxer/1 удаление всех боксеров из соревнования
        public async Task<ActionResult<DeleteCompetitionsBoxersViewModel>> DeleteCompetitionBoxers(int id)
        {
            var boxers = await _context.CompetitionsBoxers.Where(a => a.CompetitionsId == id).ToListAsync();
            if (boxers == null) { return NotFound(); }
            else
            {

                _context.CompetitionsBoxers.RemoveRange(boxers);
                await _context.SaveChangesAsync();
                return Ok(boxers);
            }
        }


        [Authorize(Roles = "admin,coach,boxer,lead")]
        [HttpDelete] // DELETE: /api/competitions/boxer удаление конкретного боксера из соревнований
        public async Task<ActionResult<DeleteCompetitionsBoxersViewModel>> DeleteBoxerParticipating([FromQuery] DeleteCompetitionsBoxersViewModel viewModel)
        {
            var boxer = await _context.CompetitionsBoxers.Where(a => a.CompetitionsId == viewModel.CompetitionsId && a.BoxerId == viewModel.BoxerId).FirstOrDefaultAsync();
            if (boxer == null) return NotFound();
            _context.CompetitionsBoxers.Remove(boxer);
            await _context.SaveChangesAsync();
            return Ok(boxer);
        }
    }
}
