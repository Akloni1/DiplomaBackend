
using AutoMapper;
using Diploma.ViewModels.Boxers;
using Diploma.ViewModels.CompetitionsBoxers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Diploma.Controllers
{
    [Route("api/competitions/boxers/comparison")]
    [ApiController]
    public class CompetitionsBoxersComparisonApiController: ControllerBase { 

    private readonly BoxContext _context;
    private readonly IMapper _mapper;

    public CompetitionsBoxersComparisonApiController(BoxContext context, IMapper mapper)
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
            if (boxers.Count > 0)
            {
                boxersConcat = boxers[0];
                for (int i = 0; i < boxers.Count - 1; i++)
                {
                    boxersConcat = (boxersConcat ?? Enumerable.Empty<BoxerViewModel>()).Concat(boxers[i + 1] ?? Enumerable.Empty<BoxerViewModel>());
                }
            }
            else return NotFound();



            DateTime timeNow = DateTime.Now;
            List<BoxerViewModel> list_14_16;
            List<BoxerViewModel> list_16_18;
            List<BoxerViewModel> list_18_25;
            List<BoxerViewModel> list_25_30;
            List<BoxerViewModel> list_30_40;

            foreach (var boxer in boxersConcat)
            {
                var dateOfBirth = (DateTime)boxersConcat.Select(a => a.DateOfBirth).First();
                int age = timeNow.Year - dateOfBirth.Year;
               
                if(age>=14 && age < 16)
                {
                   
                }
                else if (age >= 16 && age < 18)
                {

                }
                else if (age >= 18 && age < 25)
                {

                }
                else if (age >= 25 && age < 31)
                {

                }
                else if (age >= 31 && age < 41)
                {

                }
                else
                {
                   
                }
            }
           /* var dateOfBirth = (DateTime)boxersConcat.Select(a => a.DateOfBirth).First();
            int age = timeNow.Year - dateOfBirth.Year;*/

            return Ok(boxersConcat);
        }

    }
}
