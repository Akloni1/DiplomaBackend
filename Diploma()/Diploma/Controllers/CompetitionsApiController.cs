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
    [Route("api/competitions")]
    [ApiController]
    public class CompetitionsApiController : ControllerBase
    {



        private readonly BoxContext _context;
        private readonly IMapper _mapper;
        private CompetitionsBoxersApiController _competitionsBoxersApiController;
        private CompetitionsClubsApiController _competitionsClubsApiController;
        public CompetitionsApiController(BoxContext context, IMapper mapper)
        {
            _competitionsBoxersApiController = new CompetitionsBoxersApiController(context, mapper);
            _competitionsClubsApiController = new CompetitionsClubsApiController(context, mapper);
            _context = context;
            _mapper = mapper;
        }


        [Authorize]
        [HttpGet] // GET
        [ProducesResponseType(200, Type = typeof(IEnumerable<CompetitionsViewModel>))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<CompetitionsViewModel>>> GetCompetitions()
        {
            var competitions = _mapper.Map<IEnumerable<Competitions>, IEnumerable<CompetitionsViewModel>>(await _context.Competitions.ToListAsync());
            return Ok(competitions);
        }








        [Authorize]
        [HttpGet("{id}")] // GET: /api/Competitions/5
        [ProducesResponseType(200, Type = typeof(CompetitionsViewModel))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var competition = _mapper.Map<CompetitionsViewModel>(await _context.Competitions.FirstOrDefaultAsync(m => m.CompetitionsId == id));
            if (competition == null) return NotFound();
            return Ok(competition);
        }


        [Authorize(Roles = "admin,lead")]

        [HttpPost] // POST: api/Competitions
        public async Task<ActionResult<InputCompetitionsViewModel>> PostCompetition(InputCompetitionsViewModel inputModel)
        {

            inputModel.IsStarted = false;
            var competition = _context.Add(_mapper.Map<Competitions>(inputModel)).Entity;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = competition.CompetitionsId }, _mapper.Map<InputCompetitionsViewModel>(inputModel));
        }


        [Authorize(Roles = "admin,lead")]

        [HttpPut("{id}")] // PUT: api/Competitions/5
        public async Task<IActionResult> UpdateBoxer(int id, EditCompetitionsViewModel editModel)
        {
            try
            {
                var competition = _mapper.Map<Competitions>(editModel);
                competition.CompetitionsId = id;
                _context.Update(competition);
                await _context.SaveChangesAsync();

                return Ok(_mapper.Map<EditCompetitionsViewModel>(competition));
            }
            catch (DbUpdateException)
            {
                if (!await CompetitionExists(id))
                {
                    return BadRequest();
                }
                else
                {
                    throw;
                }
            }
        }

        [Authorize(Roles = "admin,lead")]

        [HttpDelete("{id}")] // DELETE: api/Competitions/5
        public async Task<ActionResult<DeleteCompetitionsViewModel>> DeleteCompetition(int id)
        {



            await _competitionsBoxersApiController.DeleteCompetitionBoxers(id);

            await _competitionsClubsApiController.DeleteCompetitionClubs(id);

            var competition = await _context.Competitions.FindAsync(id);
            if (competition == null) return NotFound();
            _context.Competitions.Remove(competition);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<DeleteCompetitionsViewModel>(competition));
        }

        private async Task<bool> CompetitionExists(int id)
        {
            return await _context.Competitions.AnyAsync(e => e.CompetitionsId == id);
        }
    }
}
