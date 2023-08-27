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
        private readonly CriteriaAdvertRepository CriteriaAdvertRepository;
        public AdvertController(AdvertRepository advertRepository, UserRepository userRepository, CriteriaAdvertRepository criteriaAdvertRepository = null)
        {
            UserRepository = userRepository;
            AdvertRepository = advertRepository;
            CriteriaAdvertRepository = criteriaAdvertRepository;    
        }

        [HttpGet("getAdvertByTimestamp/{page}")]
        public ActionResult<IEnumerable<Advert>> GetAdvert(int page) {
            IEnumerable<Advert> Adverts;
          
            Adverts = AdvertRepository.getAllAdverts().OrderByDescending(a => a.Id).Skip((page - 1) * 10).Take(10); 

            return Ok(Adverts);
        }
        [HttpGet("getAdvertsBySearch/{indication}")]
        public ActionResult<IEnumerable<Advert>> GetAdvertBySearch(string indication){
            IEnumerable<Advert>? Adverts = AdvertRepository.getBySearch(indication);
            if(Adverts == null)
            {
                return NotFound("Aucune annonces trouvées");
            }
            return Ok(Adverts);
        }
       

   
        [HttpGet("getAdvertbyId/{id}")]
        public ActionResult<Advert> GetAdvertbyId(int id) { 
            Advert advertById = AdvertRepository.getAdvertById(id);
            if(advertById == null) {
                return BadRequest("Annonce introuvable");
            }
            return advertById;
        }

        [HttpPost("GetAdvertByCriteria")]
        public ActionResult<IEnumerable<Advert>> GetAdvertByCriteria([FromBody] List<int> listCriteriaId)
        {
            IEnumerable<Advert> adverts = CriteriaAdvertRepository.getAdvertByCriterias(listCriteriaId);
            return Ok(adverts);
        }


        [Authorize]
        [HttpPost]
        public ActionResult<Advert> PostAdvert(AdvertPayload advertPayload)
        {
            Advert advert = AdvertRepository.iniAdvert(advertPayload); 
            User user = UserRepository.FromId(UserGuid);
            advert.User = user;
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
