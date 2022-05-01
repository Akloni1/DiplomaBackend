using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Diploma.Services.BoxingClubsServices;
using Diploma.ViewModels.Boxers;
using Diploma.ViewModels.BoxingClubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Controllers
{
    [Route("api/boxingClubs")]
    [ApiController]
    public class BoxingClubsApiController : ControllerBase
    {

        private readonly IBoxingClubsServices _boxingClubsServices;


        public BoxingClubsApiController(IBoxingClubsServices boxingClubsServices)
        {
            _boxingClubsServices = boxingClubsServices;
        }


        [Authorize]
        [HttpGet] // GET
        [ProducesResponseType(200, Type = typeof(IEnumerable<BoxingClubsViewModel>))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<BoxingClubsViewModel>>> GetBoxingClubs()
        {
            var boxingClubs = await _boxingClubsServices.GetAllBoxingClubs();
            return Ok(boxingClubs);

        }


        [Authorize]
        [HttpGet("{id}")] // GET: /api/boxers/5
        [ProducesResponseType(200, Type = typeof(BoxingClubsViewModel))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var boxingClubs = await _boxingClubsServices.GetBoxingClub(id);
            if (boxingClubs == null) return NotFound();
            return Ok(boxingClubs);
        }

        [Authorize(Roles = "lead")]
        [HttpPost] // POST: api/boxers
        public async Task<ActionResult<InputBoxingClubsViewModel>> PostBoxingClubs(InputBoxingClubsViewModel inputModel)
        {
            var boxingClub = await _boxingClubsServices.AddBoxingClub(inputModel);
            return Ok(boxingClub);
        }

        [Authorize(Roles = "lead")]
        [HttpPut("{id}")] // PUT: api/boxers/5
        public async Task<IActionResult> UpdateBoxingClub(int id, EditBoxingClubsViewModel editModel)
        {
            var boxingClub = await _boxingClubsServices.UpdateBoxingClub(id, editModel);
            if (boxingClub == null) return BadRequest();
            return Ok(boxingClub);
        }

        [Authorize(Roles = "lead")]
        [HttpDelete("{id}")] // DELETE: api/boxingClubs/5
        public async Task<ActionResult<DeleteBoxingClubsViewModel>> DeleteBoxingClubs(int id)
        {
            var boxingClub = await _boxingClubsServices.DeleteBoxingClub(id);
            if (boxingClub == null) return NotFound();
            return Ok(boxingClub);
        }



    }
}