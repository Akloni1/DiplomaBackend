using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Diploma;
using Diploma.Services;
using Diploma.ViewModels.Boxers;
using Diploma.ViewModels.Coaches;
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
            if (boxer != null)
            {
              return Ok(_mapper.Map<BoxerViewModel>(boxer));
            }
            else if (coach!=null)
            {
                return Ok(_mapper.Map<CoachViewModel>(coach));
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
            if (boxer != null)
            {
                return Ok(_mapper.Map<BoxerViewModel>(boxer));
            }
            else if (coach != null)
            {
                return Ok(_mapper.Map<CoachViewModel>(coach));
            }
            else
            {
                return null;
            }

        }

    }
}
