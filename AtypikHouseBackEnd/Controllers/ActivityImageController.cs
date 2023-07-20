using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payloads;
using Repositories;
using System.Security.Claims;

namespace AtypikHouseBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActivityImageController : Controller
    {
        public int UserGuid => int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
        private readonly ActivityImageRepository activityImageRepository;
        
        public ActivityImageController(ActivityImageRepository activityImageRepository)
        {
            this.activityImageRepository = activityImageRepository;
        }

        [AllowAnonymous]
        [HttpGet("{idActivity}")]
        public ActionResult<IEnumerable<ActivityImage>> Get(int idActivity) {

            IEnumerable<ActivityImage> activityImages = activityImageRepository.getImagesByActivityId(idActivity);

            if(activityImages == null)
            {
                return NotFound();
            }
            return Ok(activityImages);
        }

        [Authorize]
        [HttpPost]
        public ActionResult createImageForActivity(ActivityImagePayload activityImagePayload)
        {
            ActivityImage activityImage = activityImageRepository.iniActivityImage(activityImagePayload);
            if(activityImage.Activity == null )
            {
                return NotFound("Activitée associée Introuvable");
            }
            activityImageRepository.Add(activityImage);
            activityImageRepository.Save();
            return Ok();
        }

        [Authorize]
        [HttpDelete("{idImageActivity}")]

        public ActionResult ActionDeleteImage(int idImageActivity)
        {
            activityImageRepository.FromId(idImageActivity);
            activityImageRepository.Save();
            return Ok();
        }
    }
}
