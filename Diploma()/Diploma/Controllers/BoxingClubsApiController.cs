using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Diploma.Services.BoxingClubsServices;
using Diploma.ViewModels.Boxers;
using Diploma.ViewModels.BoxingClubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Controllers
{
    [Route("api/boxingClubs")]
    [ApiController]
    public class BoxingClubsApiController:ControllerBase
    {
        
        private readonly IBoxingClubsServices _boxingClubsServices;


       public BoxingClubsApiController(IBoxingClubsServices boxingClubsServices)
        {      
            _boxingClubsServices = boxingClubsServices;
        }

        [HttpGet] // GET: /api/boxers
        [ProducesResponseType(200, Type = typeof(IEnumerable<BoxingClubsViewModel>))]  
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<BoxingClubsViewModel>> GetBoxingClubs()
        {
            var boxingClubs = _boxingClubsServices.GetAllBoxingClubs();
            return Ok(boxingClubs);
         
        }
        
        
        
        [HttpGet("{id}")] // GET: /api/boxers/5
        [ProducesResponseType(200, Type = typeof(BoxingClubsViewModel))]  
        [ProducesResponseType(404)]
        public IActionResult GetById(int id)
        {
            var boxingClubs = _boxingClubsServices.GetBoxingClub(id);
            if (boxingClubs == null) return NotFound();
            return Ok(boxingClubs);
        }
        
        [HttpPost] // POST: api/boxers
        public ActionResult<InputBoxingClubsViewModel> PostBoxingClubs(InputBoxingClubsViewModel inputModel)
        {
            var boxingClub = _boxingClubsServices.AddBoxingClub(inputModel); 
            return Ok(boxingClub);
        }
        
        [HttpPut("{id}")] // PUT: api/boxers/5
        public IActionResult UpdateBoxingClub(int id, EditBoxingClubsViewModel editModel)
        {
            var boxingClub = _boxingClubsServices.UpdateBoxingClub(id, editModel);
            if (boxingClub == null) return BadRequest();
            return Ok(boxingClub);
        }
        
        [HttpDelete("{id}")] // DELETE: api/boxingClubs/5
        public ActionResult<DeleteBoxingClubsViewModel> DeleteBoxingClubs(int id)
        {
            var boxingClub = _boxingClubsServices.DeleteBoxingClub(id);
            if (boxingClub == null) return NotFound();
            return Ok(boxingClub);
        }

       
        
    }
}