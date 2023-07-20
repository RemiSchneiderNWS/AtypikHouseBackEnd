using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc;
using System;
using Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repositories;
using AtypikHouseBackEnd.Services;
using System.Text.RegularExpressions;
using Payloads;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Security.Cryptography;

namespace AtypikHouseBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {  
        public int UserGuid => int.Parse( User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
        private readonly UserRepository UserRepo;
        private readonly IConfiguration _config;
      
        public UserController(UserRepository user, IConfiguration config)
        {
            UserRepo = user;
            _config = config;
        }

        [HttpPost]
        public ActionResult<bool> CreateUser(CreateUserPayload user)
        {
            if (!ModelState.IsValid)
                return false;

            string passwordHashed = Hashing.Sha512(user.Password);
            user.Password = passwordHashed;
            /* var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
             if (!emailRegex.IsMatch(user.Mail))
             {

                 return BadRequest("L'adresse e-mail n'est pas au bon format.");
             }*/
            if (UserRepo.UserExist(user.Mail.ToLowerInvariant()))
            {
                return BadRequest("Email déjà utilisé");
            }
            UserRepo.Add(user.iniUser());
            UserRepo.Save();
            return true;
        }

        [HttpPost("login")]
        public ActionResult<User> Login(LogUser UserLog)
        {
            UserLog.Password = Hashing.Sha512(UserLog.Password);
            User user = UserRepo.Login(UserLog);
            if (user == null)
            {
                return BadRequest("Mot de passe ou Identifiant Incorect");
            }
            user.AccessToken = GenerateToken(user);
            return user; 
        }

        [Authorize]
        [HttpPost("refresh")]
        public string RefreshToken()
        {
            User user = UserRepo.FromId(UserGuid);
            string token = GenerateToken(user);
            user.RefreshToken = token;
            UserRepo.Update(user);
            UserRepo.Save();
            return token;
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,"" + user.Id),
                new Claim(ClaimTypes.Role, "" + user.Role),               
                new Claim(ClaimTypes.Email,"" + user.Mail)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }


    }
}
