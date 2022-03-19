using AutoMapper;
using Diploma.ViewModels.Boxers;
using Diploma.ViewModels.CompetitionsBoxers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Diploma.Controllers
{
    [Route("api/competitions/boxers/not/participating")]
    [ApiController]
    public class CompetitionsBoxersNotParticipatingApiController : ControllerBase
    {

        private readonly BoxContext _context;
        private readonly IMapper _mapper;

        public CompetitionsBoxersNotParticipatingApiController(BoxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        [HttpGet("{id}")]  // GET: /api/competitions/boxer/1
        [ProducesResponseType(200, Type = typeof(IEnumerable<BoxerViewModel>))]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<BoxerViewModel>> GetBoxersNotParticipatingByIdCompetition(int id)
        {

            var competitionsBoxers = _mapper.Map<IEnumerable<CompetitionsBoxers>, IEnumerable<CompetitionsBoxersViewModel>>(_context.CompetitionsBoxers.Where(a => a.CompetitionsId == id).ToList());
            var idBoxers = competitionsBoxers.Select(h => h.BoxerId).ToList();

            //Можно использовать в CompetitionsBoxersApiController
            /* var competitionsBoxers1 = _context.Boxers.Join(_context.CompetitionsBoxers,
                   b => b.BoxerId,
                  c => c.BoxerId,

                  (b, c)=> new
                  {

                    BoxerId = b.BoxerId,
                    FirstName = b.FirstName,
                    CompetitionsId =c.CompetitionsId,
                    Id = c.BoxerId,

                  }).Where(a => a.CompetitionsId == id);*/

            var result = _mapper.Map<IEnumerable<Boxers>, IEnumerable<BoxerViewModel>>(from l1 in _context.Boxers where !idBoxers.Contains(l1.BoxerId) select l1);
            return Ok(result);
            
        }



    }
}
