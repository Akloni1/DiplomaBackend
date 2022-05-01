using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using AutoMapper;
using Diploma.Cryptography;
using Diploma.ViewModels.Authorization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Controllers.AccountController
{
    [ApiController]
    [Route("api/token")]
    public class AccountController : Controller
    {

        private readonly BoxContext _context;
        private readonly IMapper _mapper;
        private readonly PwdHash _pwdHash;

        public AccountController(BoxContext context, IMapper mapper)
        {
            _pwdHash = new PwdHash();
            _context = context;
            _mapper = mapper;
        }




        [HttpPost]
        public async Task<IActionResult> Token(Authorization authorization)
        {

            authorization.Password = _pwdHash.sha256encrypt(authorization.Password, authorization.Login);

            var identity = await GetIdentity(authorization.Login, authorization.Password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }



            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name,

            };

            return Json(response);
        }

        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
           // Coaches coach1 = await _context.Coaches.FirstOrDefaultAsync(x => x.Login == username && x.Password == password);
            Coaches coach = await _context.Coaches.FirstOrDefaultAsync(x => x.Login == username && x.Password == password);
            Boxers boxer = await _context.Boxers.FirstOrDefaultAsync(x => x.Login == username && x.Password == password);
            Admin admin = await _context.Admins.FirstOrDefaultAsync(x => x.Login == username && x.Password == password);
            Lead lead = await _context.Leads.FirstOrDefaultAsync(x => x.Login == username && x.Password == password);
           

            if (coach != null)
            {
                var claims = new List<Claim>
                   {
                       new Claim(ClaimsIdentity.DefaultNameClaimType, coach.Login),
                       new Claim(ClaimsIdentity.DefaultRoleClaimType, coach.Role)
                   };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            else if (boxer != null)
            {
                var claims = new List<Claim>
                   {
                      new Claim(ClaimsIdentity.DefaultNameClaimType, boxer.Login),
                       new Claim(ClaimsIdentity.DefaultRoleClaimType, boxer.Role)
                   };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;

            }
            else if (admin != null)
            {
                var claims = new List<Claim>
                   {
                      new Claim(ClaimsIdentity.DefaultNameClaimType, admin.Login),
                       new Claim(ClaimsIdentity.DefaultRoleClaimType, admin.Role)
                   };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;

            }

            else if (lead != null)
            {
                var claims = new List<Claim>
                   {
                      new Claim(ClaimsIdentity.DefaultNameClaimType, lead.Login),
                       new Claim(ClaimsIdentity.DefaultRoleClaimType, lead.Role)
                   };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;

            }

            // если пользователя не найдено
            return null;
        }


    }
}
