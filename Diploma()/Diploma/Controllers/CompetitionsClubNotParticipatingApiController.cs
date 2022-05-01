using AutoMapper;
using Diploma.ViewModels.BoxingClubs;
using Diploma.ViewModels.CompetitionsClubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma.Controllers
{
    [Route("api/competitions/boxingClub/not/participating")]
    [ApiController]
    public class CompetitionsClubNotParticipatingApiController: ControllerBase
    {
        private readonly BoxContext _context;
        private readonly IMapper _mapper;

        public CompetitionsClubNotParticipatingApiController(BoxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        [Authorize(Roles = "admin,lead")]
        [HttpGet("{id}")]  // GET: /api/competitions/boxer/1
        [ProducesResponseType(200, Type = typeof(IEnumerable<BoxingClubsViewModel>))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<BoxingClubsViewModel>>> GetBoxingClubsNotParticipatingByIdCompetition(int id)
        {

            var competitionsClubs = _mapper.Map<IEnumerable<CompetitionsClubs>, IEnumerable<CompetitionsClubsViewModel>>(await _context.CompetitionsClubs.Where(a => a.CompetitionsId == id).ToListAsync());
            var idClubs = competitionsClubs.Select(h => h.BoxingClubId).ToList();

           

            var result = _mapper.Map<IEnumerable<BoxingClubs>, IEnumerable<BoxingClubsViewModel>>(from l1 in await _context.BoxingClubs.ToListAsync() where !idClubs.Contains(l1.BoxingClubId) select l1);
            return Ok(result);

        }
    }
}
