using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;

namespace AtypikHouseBackEnd.Controllers
{  
    [ApiController]
    [Route("[controller]")]
    public class AdvertController : Controller
    {
        public int UserGuid => int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value);

        private readonly AdvertRepository AdvertRepository;
        private readonly UserRepository UserRepository;
        public AdvertController(AdvertRepository advertRepository) { 
            AdvertRepository= advertRepository;
        }

        [HttpGet("getAdvertByTimestamp/{page}")]
        public ActionResult<IEnumerable<Advert>> GetAdvert(int page) {
            IEnumerable<Advert> Adverts = AdvertRepository.All().OrderByDescending(a => a.Id).Skip((page - 1) * 10).Take(10); 

            return Ok(Adverts);
        }

        [Authorize]
        [HttpGet("getAdvertbyId/{id}")]
        public ActionResult<Advert> GetAdvertbyId(int id) { 
            Advert advertById = AdvertRepository.FromId(id);
            if(advertById == null) {
                return BadRequest("Annonce introuvable");
            }
            return advertById;
        }

        [Authorize]
        [HttpPost]
        public ActionResult<Advert> PostAdvert(Advert advert)
        {
            advert.User = UserRepository.FromId(UserGuid);
            AdvertRepository.Add(advert);
            AdvertRepository.Save();
            return Ok(advert);
        }

        [Authorize]
        [HttpPost("updateAdvert")]
        public ActionResult<Advert> UpdateAdvert(Advert advert)
        {
            Advert advertUpdated = AdvertRepository.FromId(advert.Id);
            if(advertUpdated.User.Id != UserGuid)
            {
                return BadRequest("Vous n'etes pas Autorisé à faire cette action");
            }

            if(advertUpdated == null) {
                return BadRequest("Annonce introuvable");
            }
            advertUpdated.Name= advert.Name;
            advertUpdated.Type  = advert.Type;  
            advertUpdated.Tenants = advert.Tenants;
            advertUpdated.Status = advert.Status;
            advertUpdated.Up = advert.Up;
            advertUpdated.Adress = advert.Adress;
            advertUpdated.City = advert.City;
            advertUpdated.Postal = advert.Postal;
            advertUpdated.Price = advert.Price; 
            advertUpdated.Describe  = advert.Describe;  
            advertUpdated.CriLimit = advert.CriLimit;

            AdvertRepository.Update(advertUpdated);
            AdvertRepository.Save();
            return Ok(advertUpdated);
        }

        [Authorize]
        [HttpDelete("deleteAdvert/{id}")]

        public ActionResult<Advert> DeleteAdvert(int id)
        {
            Advert advertToDelete = AdvertRepository.FromId(id);    
            if(advertToDelete == null) {
                return BadRequest("Annonce introuvable");
            }

            if(advertToDelete.User.Id != UserGuid)
            {
                return BadRequest("Vous n'etes pas autorisé à faire cette action");
            }

            AdvertRepository.Delete(advertToDelete);
            return Ok();
        }
        
    }
}
