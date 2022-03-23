
using AutoMapper;
using Diploma.ViewModels.Boxers;
using Diploma.ViewModels.CompetitionsBoxers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
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
        public ActionResult<IEnumerable<BoxersViewModel>> GetBoxersComparison(int id)
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
            // var res = new { };

            List<int> idDistributed = new List<int>();

            // IEnumerable<BoxersViewModel> res ;
            List<BoxersViewModel> res = new List<BoxersViewModel>();
            // BoxersViewModel couple = new BoxersViewModel();

            foreach (var boxer1 in boxersConcat)
            {

                if (idDistributed.Contains(boxer1.BoxerId))
                {
                    continue;
                }





                var dateOfBirth1 = (DateTime)boxer1.DateOfBirth;
                double age1 = timeNow.Year - dateOfBirth1.Year;
                foreach (var boxer2 in boxersConcat)
                {
                    if (idDistributed.Contains(boxer2.BoxerId))
                    {
                        continue;
                    }
                    if (idDistributed.Contains(boxer1.BoxerId))
                    {
                        continue;
                    }

                    var dateOfBirth2 = (DateTime)boxer2.DateOfBirth;
                    double age2 = timeNow.Year - dateOfBirth2.Year;
                    if (boxer1.BoxerId == boxer2.BoxerId) continue;

                    if (age1 / 100d * 20d >= Math.Abs(age1 - age2))
                    {
                        if ((double)boxer1.Weight / 100d * 20d >= Math.Abs((int)boxer1.Weight - (int)boxer2.Weight))
                        {
                            if ((double)boxer1.NumberOfFightsHeld / 100d * 20d >= Math.Abs((int)boxer1.NumberOfFightsHeld - (int)boxer2.NumberOfFightsHeld))
                            {
                                if ((double)boxer1.NumberOfWins / 100d * 20d >= Math.Abs((int)boxer1.NumberOfWins - (int)boxer2.NumberOfWins))
                                {
                                    if ((double)boxer1.TrainingExperience / 100d * 20d >= Math.Abs((double)boxer1.TrainingExperience - (double)boxer2.TrainingExperience))
                                    {
                                        if (((string)boxer1.Discharge).Equals((string)boxer2.Discharge))
                                        {
                                            BoxersViewModel couple = new BoxersViewModel();
                                            couple.boxer1 = boxer1;
                                            couple.boxer2 = boxer2;
                                            res.Add(couple);



                                            //  var  couple = new { boxer1, boxer2 };
                                            //нужно реализовать добавление couple в обьект где будут все пары 

                                            idDistributed.Add(boxer1.BoxerId);
                                            idDistributed.Add(boxer2.BoxerId);


                                            //  res = { };
                                            //   jj = (jj ?? Enumerable.Empty <dynamic>()).Concat(couple ?? Enumerable.Empty<dynamic>());
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


            }

            List<BoxerViewModel> notPaired = boxersConcat.ToList();
            foreach (int idDist in idDistributed)
            {
                var model = (notPaired.Where(a => a.BoxerId == idDist).First());
                notPaired.Remove(model);

            }

            var final = new
            {
                res,
               
                    notPaired
                
            };

            return Ok(final);
        }

    }
}
