using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Diploma.Services.CoachesServices;
using Diploma.ViewModels.BoxingClubs;
using Diploma.ViewModels.Coaches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Diploma.Controllers
{
    [Route("api/coach")]
    [ApiController]
    public class CoachesApiController : ControllerBase
    {

        private readonly ICoachesServices _coachesServices;
        private readonly ILogger _logger;

        public CoachesApiController(ICoachesServices coachesServices, ILogger<CoachesApiController> logger)
        {
            _coachesServices = coachesServices;
            _logger = logger;
        }

        [Authorize]
        [HttpGet] // GET: 
        [ProducesResponseType(200, Type = typeof(IEnumerable<CoachViewModel>))]  
        [ProducesResponseType(404)]
        public async Task<ActionResult<ICollection<CoachViewModel>>> GetCoaches()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var coaches = await _coachesServices.GetAllCoaches();
            
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            return Ok(coaches);
        }


        [Authorize]
        [HttpGet("{id}")] // GET: /api/boxers/5
        [ProducesResponseType(200, Type = typeof(CoachViewModel))]  
        [ProducesResponseType(404)]
        public IActionResult GetById(int id)
        {
            var coach = _coachesServices.GetCoach(id);
            if (coach == null) return NotFound();
            return Ok(coach);
        }

        [Authorize(Roles = "admin,lead")]
        [HttpPost] // POST: api/boxers
        public ActionResult<InputCoachViewModel> PostCoaches(InputCoachViewModel inputModel)
        {
            var coach = _coachesServices.AddCoach(inputModel);
            if (coach != null)
            {
                return Ok(coach);
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "admin,lead")]
        [HttpPut("{id}")] // PUT: api/boxers/5
        public IActionResult UpdateCoach(int id, EditCoachViewModel editModel)
        {

            var coach = _coachesServices.UpdateCoaches(id, editModel);
            if (coach == null) return BadRequest();
            return Ok(coach);
         
        }

        [Authorize(Roles = "admin,lead")]
        [HttpDelete("{id}")] // DELETE: api/boxingClubs/5
        public ActionResult<DeleteCoachViewModel> DeleteCoach(int id)
        {
            var coach = _coachesServices.DeleteCoach(id);
            if (coach == null) return NotFound();
            return Ok(coach);
        }

      
        
    }
}