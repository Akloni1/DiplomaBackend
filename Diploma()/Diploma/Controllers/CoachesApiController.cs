using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Diploma.Services.CoachesServices;
using Diploma.ViewModels.BoxingClubs;
using Diploma.ViewModels.Coaches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Controllers
{
    [Route("api/coach")]
    [ApiController]
    public class CoachesApiController : ControllerBase
    {

        private readonly ICoachesServices _coachesServices;

        public CoachesApiController(ICoachesServices coachesServices)
        {
            _coachesServices = coachesServices;
        }

        [Authorize]
        [HttpGet] // GET: /api/boxers
        [ProducesResponseType(200, Type = typeof(IEnumerable<CoachViewModel>))]  
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<CoachViewModel>> GetCoaches()
        {
            var coaches = _coachesServices.GetAllCoaches();
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
            return Ok(coach);
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