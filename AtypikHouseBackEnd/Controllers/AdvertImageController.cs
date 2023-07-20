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
    public class AdvertImageController : Controller
    {
        public int UserGuid => int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
        private readonly AdvertImageRepository advertImageRepository;
        public AdvertImageController(AdvertImageRepository advertImageRepository)
        {
            this.advertImageRepository = advertImageRepository;
        }

        [AllowAnonymous]
        [HttpGet("{idAdvert}")]
        public ActionResult<IEnumerable<AdvertImage>> GetImageByAdvertId(int idAdvert)
        {        
            IEnumerable< AdvertImage > Images = advertImageRepository.getImagesByAdvertId(idAdvert);
            if(Images == null)
            {
                return NotFound();
            }

            return Ok(Images);
        }
        [Authorize]
        [HttpPost]
        public ActionResult CreateImageForAdvert(AdvertImagePayload image)
        {
            AdvertImage advertImage = advertImageRepository.iniAdvertImage(image);
            if(advertImage.Advert == null ) { 
                return NotFound("Annonce associée introuvable");
            }
            advertImageRepository.Add(advertImage);
            advertImageRepository.Save();
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public ActionResult DeleteImage(int id)
        {
            AdvertImage imageToDelete = advertImageRepository.FromId(id);
            if(imageToDelete == null)
            {
                return NotFound();
            }
            advertImageRepository.Delete(imageToDelete);


            return Ok();
        }

    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        