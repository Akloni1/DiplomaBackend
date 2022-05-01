using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Diploma;
using Diploma.Services;
using Diploma.ViewModels.Admins;
using Diploma.ViewModels.Boxers;
using Diploma.ViewModels.Coaches;
using Diploma.ViewModels.Lead;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Controllers.AccountController
{
    [ApiController]
    [Route("api")]
    public class AccountUserController: Controller
    {
        private readonly BoxContext _context;
        private readonly IMapper _mapper;
        

        public AccountUserController(BoxContext context, IMapper mapper)
        {
           
            _context = context;
            _mapper = mapper;
        }

        [Authorize]
        [Route("getuser")]

        public async Task<dynamic> GetUserByToken()
        {
            var boxer= await _context.Boxers.Where(a => a.Login == User.Identity.Name).FirstOrDefaultAsync();
            var coach = await _context.Coaches.Where(a => a.Login == User.Identity.Name).FirstOrDefaultAsync();
            var admin = await _context.Admins.Where(a => a.Login == User.Identity.Name).FirstOrDefaultAsync();
            var lead = await _context.Leads.Where(a => a.Login == User.Identity.Name).FirstOrDefaultAsync();
            if (boxer != null)
            {
              return Ok(_mapper.Map<BoxerViewModel>(boxer));
            }
            else if (coach!=null)
            {
                return Ok(_mapper.Map<CoachViewModel>(coach));
            }
            else if (admin != null)
            {
                return Ok(_mapper.Map<AdminViewModel>(admin));
            }
            else if (lead != null)
            {
                return Ok(_mapper.Map<LeadViewModel>(lead));
            }
            else
            {
                return Ok($"Пользователь не найден");
            }

        }

        [Route("getuserbylogin")]

        public async Task<dynamic> GetUserByLogin([FromQuery] string login)
        {
            var boxer = await _context.Boxers.Where(a => a.Login == login).FirstOrDefaultAsync();
            var coach = await _context.Coaches.Where(a => a.Login == login).FirstOrDefaultAsync();
            var admin = await _context.Admins.Where(a => a.Login == login).FirstOrDefaultAsync();
            var lead = await _context.Leads.Where(a => a.Login == login).FirstOrDefaultAsync();
            if (boxer != null)
            {
                return Ok(_mapper.Map<BoxerViewModel>(boxer));
            }
            else if (coach != null)
            {
                return Ok(_mapper.Map<CoachViewModel>(coach));
            }
            else if (admin != null)
            {
                return Ok(_mapper.Map<AdminViewModel>(admin));
            }
            else if (lead != null)
            {
                return Ok(_mapper.Map<LeadViewModel>(lead));
            }
            else
            {
                return null;
            }

        }

    }
}
