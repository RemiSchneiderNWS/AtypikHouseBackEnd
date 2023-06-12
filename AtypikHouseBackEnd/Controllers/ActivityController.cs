using Microsoft.AspNetCore.Mvc;
using AtypikHouseBackEnd.Models;
using System;
using AtypikHouseBackEnd.Context;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AtypikHouseBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActivityController : Controller
    {
         
        private readonly AppDbContext context;

        public ActivityController(AppDbContext context) {
            this.context = context;
        }

        [HttpGet("AllActivity")]
        public ActionResult<Activity> GetAllActivity(){
            return Ok();
        }

        [HttpPost("createActivity")]
        public ActionResult<Activity> CreateActivity(Activity activity){
            context.activity.Add(activity);
            context.SaveChanges();
            return Ok(activity);
        }
    }
}
