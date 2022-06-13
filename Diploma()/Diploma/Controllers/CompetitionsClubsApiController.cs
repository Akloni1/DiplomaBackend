using AutoMapper;
using Diploma.ViewModels.BoxingClubs;
using Diploma.ViewModels.CompetitionsClubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma.Controllers
{
    [Route("api/competitions/clubs")]
    [ApiController]
    public class CompetitionsClubsApiController : ControllerBase
    {




        private readonly BoxContext _context;
        private readonly IMapper _mapper;

        public CompetitionsClubsApiController(BoxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [Authorize(Roles = "admin,coach,boxer,lead")]
        [HttpGet("{id}")]  // GET: Выводит клубы участвующие в соревнованих 
        [ProducesResponseType(200, Type = typeof(IEnumerable<BoxingClubsViewModel>))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<BoxingClubsViewModel>>> GetClubsByIdCompetition(int id)
        {
            var competitionsClubs = _mapper.Map<IEnumerable<CompetitionsClubs>, IEnumerable<CompetitionsClubsViewModel>>(await _context.CompetitionsClubs.Where(a => a.CompetitionsId == id).ToListAsync());
            var idClubs = competitionsClubs.Select(h => h.BoxingClubId).ToList();
            List<BoxingClubsViewModel> clubs = new List<BoxingClubsViewModel>();

            foreach (int ClubId in idClubs)
            {
                clubs.Add(_mapper.Map<BoxingClubsViewModel>(await _context.BoxingClubs.Where(a => a.BoxingClubId == ClubId).FirstOrDefaultAsync()));
            }

          /*  IEnumerable<BoxingClubsViewModel> clubsConcat = null;
            if (clubs.Count > 0)
            {
                clubsConcat = clubs[0];
                for (int i = 0; i < clubs.Count - 1; i++)
                {
                    clubsConcat = (clubsConcat ?? Enumerable.Empty<BoxingClubsViewModel>()).Concat(clubs[i + 1] ?? Enumerable.Empty<BoxingClubsViewModel>());
                }
            }
            //    else NotFound();
            else
            {
                List<BoxingClubsViewModel> boxerViewModels = new List<BoxingClubsViewModel>();
                return boxerViewModels;
            }*/

            return Ok(clubs);
        }





        [Authorize(Roles = "admin,lead")]
        [HttpPost] // POST: /api/competitions/clubs / добавление клуба в соревнования
        public async Task<ActionResult<InputCompetitionsClubsViewModel>> PostCompetitionClub(InputCompetitionsClubsViewModel inputModel)
        {

            var competitionClub = _context.Add(_mapper.Map<CompetitionsClubs>(inputModel)).Entity;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = competitionClub.CompetitionsId }, _mapper.Map<InputCompetitionsClubsViewModel>(inputModel));
        }



        [Authorize(Roles = "admin,lead")]
        [HttpDelete("{id}")] // DELETE: /api/competitions/clubs/1/ удаляет все клубы привязанные к соревнованию 
       
        public async Task<ActionResult<IEnumerable<DeleteCompetitionsClubsViewModel>>> DeleteCompetitionClubs(int id)
        {
            var clubs = await _context.CompetitionsClubs.Where(a => a.CompetitionsId == id).ToListAsync();
            if (clubs == null) return NotFound();

            _context.CompetitionsClubs.RemoveRange(clubs);
            await _context.SaveChangesAsync();      
            var res = _mapper.Map<IEnumerable<CompetitionsClubs>, IEnumerable<DeleteCompetitionsClubsViewModel>>(clubs);
            //  return Ok(clubs);
            return Ok(res);
        }



        [Authorize(Roles = "admin,lead")]
        [HttpDelete] // DELETE: /api/competitions/boxer / Удаляет конкретный БК из соревнования 
        public async Task<ActionResult<DeleteCompetitionsClubsViewModel>> DeleteBoxingClubParticipating([FromQuery] DeleteCompetitionsClubsViewModel viewModel)
        {
            var club = await _context.CompetitionsClubs.Where(a => a.CompetitionsId == viewModel.CompetitionsId && a.BoxingClubId == viewModel.BoxingClubId).FirstOrDefaultAsync();
            if (club == null) return NotFound();
            _context.CompetitionsClubs.Remove(club);
            await _context.SaveChangesAsync();
            return Ok(club);
        }

    }
}
