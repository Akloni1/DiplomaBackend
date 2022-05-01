
using AutoMapper;
using Diploma.Services.BoxersComparisonServices;
using Diploma.ViewModels.Boxers;
using Diploma.ViewModels.CompetitionsBoxers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma.Controllers
{
    [Route("api/competitions/boxers/comparison")]
    [ApiController]
    public class CompetitionsBoxersComparisonApiController : ControllerBase
    {

        private readonly BoxContext _context;
        private readonly IMapper _mapper;
        private readonly IBoxersComparisonServices _boxersComparisonServices;

        public CompetitionsBoxersComparisonApiController(BoxContext context, IMapper mapper, IBoxersComparisonServices boxersComparisonServices)
        {
            _context = context;
            _mapper = mapper;
            _boxersComparisonServices = boxersComparisonServices;
        }



        [Authorize(Roles = "admin,coach,boxer,lead")]
        [HttpGet("{id}")]  // GET: /api/competitions/boxer/1
        [ProducesResponseType(200, Type = typeof(IEnumerable<BoxerViewModel>))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<BoxersViewModel>>> GetBoxersComparison(int id)
        {
            var competitionsBoxers = _mapper.Map<IEnumerable<CompetitionsBoxers>, IEnumerable<CompetitionsBoxersViewModel>>(await _context.CompetitionsBoxers.Where(a => a.CompetitionsId == id).ToListAsync());
            var idBoxers = competitionsBoxers.Select(h => h.BoxerId).ToList();
            List<BoxerViewModel> boxers = new List<BoxerViewModel>();

            foreach (int BoxerId in idBoxers)
            {
                boxers.Add(_mapper.Map<BoxerViewModel>(await _context.Boxers.Where(a => a.BoxerId == BoxerId).FirstOrDefaultAsync()));
            }


            if (!(boxers.Count > 0))
            {

                return NotFound();

            }




            var final = _boxersComparisonServices.BoxersComparison(boxers, 20d, 20d, 20d, 20d, 20d);

            if (final.notPaired.Count > 1)
            {
                var final2 = _boxersComparisonServices.BoxersComparison(final.notPaired, 20d, 20d, 50d, 20d, 50d);
                BoxersViewModel couple = new BoxersViewModel();
                foreach (var item in final2.res)
                {

                    couple.boxer1 = item.boxer1;
                    couple.boxer2 = item.boxer2;
                    final.res.Add(couple);
                    final.notPaired.Remove(item.boxer1);
                    final.notPaired.Remove(item.boxer2);
                }
            }
            return Ok(final);
        }

    }
}
