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

namespace Diploma.Controllers.AccountController
{
    [ApiController]
    [Route("/token")]
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
           public IActionResult Token(Authorization authorization)
           {

            authorization.Password = _pwdHash.sha256encrypt(authorization.Password, authorization.Login);

               var identity = GetIdentity(authorization.Login, authorization.Password);
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

           private ClaimsIdentity GetIdentity(string username, string password)
           {
            Coaches coach = _context.Coaches.FirstOrDefault(x => x.Login == username && x.Password == password);
            Boxers boxer= _context.Boxers.FirstOrDefault(x => x.Login == username && x.Password == password);
          //  Person person = people.FirstOrDefault(x => x.Login == username && x.Password == password);
              // Boxers boxer = boxers.FirstOrDefault(x => x.Login == username && x.Password == password);

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

               // если пользователя не найдено
               return null;
           }


    }
}
