using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payloads;
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
        public ActionResult<Advert> PostAdvert(AdvertPayload advertPayload)
        {
            Advert advert = AdvertRepository.iniAdvert(advertPayload); 
            advert.User = UserRepository.FromId(UserGuid);
            AdvertRepository.Add(advert);
            AdvertRepository.Save();
            return Ok(advert);
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult<Advert> UpdateAdvert(int id,AdvertPayload advertPayload)
        {
            Advert OldAdvert = AdvertRepository.FromId(id);
            Advert advertUpdated = AdvertRepository.iniAdvert(advertPayload);
            advertUpdated.User = OldAdvert.User;
            advertUpdated.Created_at = OldAdvert.Created_at;
            if (advertUpdated.User.Id != UserGuid)
            {
                return BadRequest("Vous n'etes pas Autorisé à faire cette action");
            }

            if(advertUpdated == null) {
                return BadRequest("Annonce introuvable");
            }
          
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
            AdvertRepository.Save();
            return Ok();
        }
        
    }
}
