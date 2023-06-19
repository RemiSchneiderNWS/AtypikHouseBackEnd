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

namespace AtypikHouseBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UserRepository User; 
        
        public UserController(UserRepository user)
        {
            User = user;
        }

        [HttpPost]
        public bool CreateUser(CreateUserPayload user)
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
            User.Add(user);
            User.Save();
            return true;
        }

    }
}
