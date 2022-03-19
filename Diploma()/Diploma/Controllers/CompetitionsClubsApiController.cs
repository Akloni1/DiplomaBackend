﻿using AutoMapper;
using Diploma.ViewModels.BoxingClubs;
using Diploma.ViewModels.CompetitionsClubs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diploma.Controllers
{
    [Route("api/competitions/clubs")]
    [ApiController]
    public class CompetitionsClubsApiController : ControllerBase
    {




        private readonly BoxContext _context;
        private readonly IMapper _mapper;

        public CompetitionsClubsApiController(BoxContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet("{id}")]  // GET: /api/competitions/boxer/1
        [ProducesResponseType(200, Type = typeof(IEnumerable<BoxingClubsViewModel>))]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<BoxingClubsViewModel>> GetClubsByIdCompetition(int id)
        {
            var competitionsClubs = _mapper.Map<IEnumerable<CompetitionsClubs>, IEnumerable<CompetitionsClubsViewModel>>(_context.CompetitionsClubs.Where(a => a.CompetitionsId == id).ToList());
            var idClubs = competitionsClubs.Select(h => h.BoxingClubId).ToList();
            List<IEnumerable<BoxingClubsViewModel>> clubs = new List<IEnumerable<BoxingClubsViewModel>>();

            foreach (int ClubId in idClubs)
            {
                clubs.Add(_mapper.Map<IEnumerable<BoxingClubs>, IEnumerable<BoxingClubsViewModel>>(_context.BoxingClubs.Where(a => a.BoxingClubId == ClubId).ToList()));
            }

            IEnumerable<BoxingClubsViewModel> clubsConcat = null;
            if (clubs.Count > 0)
            {
                clubsConcat = clubs[0];
                for (int i = 0; i < clubs.Count - 1; i++)
                {
                    clubsConcat = (clubsConcat ?? Enumerable.Empty<BoxingClubsViewModel>()).Concat(clubs[i + 1] ?? Enumerable.Empty<BoxingClubsViewModel>());
                }
            }
            else return NotFound();

            return Ok(clubsConcat);
        }





        [HttpPost] // POST: /api/competitions/clubs
        public ActionResult<InputCompetitionsClubsViewModel> PostClub(InputCompetitionsClubsViewModel inputModel)
        {

            var competitionClub = _context.Add(_mapper.Map<CompetitionsClubs>(inputModel)).Entity;
            _context.SaveChanges();

            return CreatedAtAction("GetById", new { id = competitionClub.CompetitionsId }, _mapper.Map<InputCompetitionsClubsViewModel>(inputModel));
        }

        

        [HttpDelete("{id}")] // DELETE: /api/competitions/clubs/1
       
        public ActionResult<IEnumerable<DeleteCompetitionsClubsViewModel>> DeleteCompetitionClubs(int id)
        {
            var clubs = _context.CompetitionsClubs.Where(a => a.CompetitionsId == id);
            if (clubs == null) return NotFound();

            _context.CompetitionsClubs.RemoveRange(clubs);
            _context.SaveChanges();      
            var c = _mapper.Map<IEnumerable<CompetitionsClubs>, IEnumerable<DeleteCompetitionsClubsViewModel>>(clubs);
            //  return Ok(clubs);
            return Ok(c);
        }





    }
}