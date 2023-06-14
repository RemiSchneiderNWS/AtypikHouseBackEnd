using Microsoft.AspNetCore.Mvc;
using System;
using Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repositories;

namespace AtypikHouseBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActivityController : Controller
    {
        private readonly ActivityRepository Activities;

        public ActivityController(ActivityRepository activities)
        {
            Activities = activities;
        }

        [HttpGet]
        public IEnumerable<Activity> GetAllActivity()
        {
            return (Activities.All());
        }

        [HttpPost]
        public Activity CreateActivity(Activity activity)
        {
            Activities.Add(activity);
            Activities.Save();
            
            return (activity);
        }
    }
}
