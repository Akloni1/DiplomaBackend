using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Diploma;
using Diploma.Services;
using Diploma.ViewModels.Boxers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Controllers
{
    [Route("api/boxers")]
    [ApiController]
    public class BoxersApiController : ControllerBase
    {

        private readonly IBoxersServices _boxersServices;

        public BoxersApiController(IBoxersServices boxersServices)
        {
            _boxersServices = boxersServices;
        }

        [Authorize]
        [HttpGet] // GET: /api/boxers
        [ProducesResponseType(200, Type = typeof(IEnumerable<BoxerViewModel>))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ICollection<BoxerViewModel>>> GetBoxers()
        {
            var boxers = await _boxersServices.GetAllBoxers();
            return Ok(boxers);
        }


        [Authorize]
        [HttpGet("{id}")] // GET: /api/boxers/5
        [ProducesResponseType(200, Type = typeof(BoxerViewModel))]
        [ProducesResponseType(404)]
        public IActionResult GetById(int id)
        {
            var boxer = _boxersServices.GetBoxer(id);
            if (boxer == null) return NotFound();
            return Ok(boxer);
        }

        [Authorize(Roles = "admin,coach,lead")]
        [HttpPost] // POST: api/boxers
        public ActionResult<BoxerViewModel> PostBoxer(InputBoxerViewModel inputModel)
        {

            var boxer = _boxersServices.AddBoxer(inputModel);
            //  return CreatedAtAction("GetById", new { id = boxer.BoxerId }, _mapper.Map<InputBoxerViewModel>(inputModel));
            if (boxer != null)
            {
                return boxer;
            }
                return BadRequest();
        }


        [Authorize(Roles = "admin,coach,lead")]
        [HttpPut("{id}")] // PUT: api/boxers/5
        public IActionResult UpdateBoxer(int id, EditBoxerViewModel editModel)
        {

            var boxer = _boxersServices.UpdateBoxer(id, editModel);
            if (boxer == null) return BadRequest();
            return Ok(boxer);
        }

        [Authorize(Roles = "admin,coach,lead")]
        [HttpDelete("{id}")] // DELETE: api/boxers/5
        public ActionResult<DeleteBoxerViewModel> DeleteBoxer(int id)
        {

            var boxer = _boxersServices.DeleteBoxer(id);
            if (boxer == null) return NotFound();
            return Ok(boxer);
        }


    }
}