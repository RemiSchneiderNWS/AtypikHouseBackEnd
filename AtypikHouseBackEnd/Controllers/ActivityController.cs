using Microsoft.AspNetCore.Mvc;
using System;
using Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Payloads;

namespace AtypikHouseBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActivityController : Controller
    {
        private readonly ActivityRepository Activities;
        public int UserGuid => int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
        public ActivityController(ActivityRepository activities)
        {
            Activities = activities;
        }

        [HttpGet]
        public IEnumerable<Activity> GetAllActivity()
        {
            return (Activities.All());
        }

        [HttpGet("GetActivityById/{id}")]
        public Activity GetActivity(int id) {

            return Activities.FromId(id);
        }

        [Authorize]
        [HttpPost]
        public ActionResult<Activity> CreateActivity(ActivityPayload activityPayload)
        {
            Activity activity = Activities.iniActivity(activityPayload);
            if (activity.Advert == null)
            {
                return NotFound("Annonce affectée introuvable");
            }
            if (activity.User == null)
            {
                return NotFound("Utilisateur affectée introuvable");
            }
            if (activity.Advert.User.Id != UserGuid)
            {
                return BadRequest("Vous n'etes pas autorisée à faire cette action")
            }
            Activities.Add(activity);
            Activities.Save();

            return (activity);
        }

        [Authorize]
        [HttpPut("{idActivity}")]
        public ActionResult<Activity> UpdateActivity(int idActivity,ActivityPayload activityPayload) {
            //TO DO vérification des données activity
            Activity activity = Activities.iniActivity(activityPayload);
            activity.Id = idActivity;
            if (activity.Advert == null)
            {
                return NotFound("Annonce affectée introuvable");
            }
            if (activity.User == null)
            {
                return NotFound("Utilisateur affectée introuvable");
            }
            if (activity.User.Id != UserGuid) {
                return BadRequest("Vous n'étes pas autorisé à faire cette action ");
            }
            Activities.Update(activity);    
            Activities.Save();

            return Ok();
        }


        [Authorize]
        [HttpDelete]
        public ActionResult<Activity> DeleteActivity(int id)
        {
            Activity activityToDelete =  Activities.FromId(id);
            if(activityToDelete == null)
            {
                return BadRequest("Activité introuvable");

            }

            if(activityToDelete.User.Id != UserGuid)
            {
                return BadRequest("Vous n'étes pas autorisé à faire cette action");
            }
            Activities.Delete(activityToDelete);
            Activities.Save();
            return Ok(activityToDelete);
        }
    }
}
