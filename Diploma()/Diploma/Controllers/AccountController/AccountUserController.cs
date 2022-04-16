using System.Collections.Generic;
using System.Linq;
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

        public dynamic GetUserByToken()
        {
            var boxer= _context.Boxers.Where(a => a.Login == User.Identity.Name).FirstOrDefault();
            var coach = _context.Coaches.Where(a => a.Login == User.Identity.Name).FirstOrDefault();
            var admin = _context.Admins.Where(a => a.Login == User.Identity.Name).FirstOrDefault();
            var lead = _context.Leads.Where(a => a.Login == User.Identity.Name).FirstOrDefault();
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

        public dynamic GetUserByLogin([FromQuery] string login)
        {
            var boxer = _context.Boxers.Where(a => a.Login == login).FirstOrDefault();
            var coach = _context.Coaches.Where(a => a.Login == login).FirstOrDefault();
            var admin = _context.Admins.Where(a => a.Login == login).FirstOrDefault();
            var lead = _context.Leads.Where(a => a.Login == login).FirstOrDefault();
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
