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
    public class CriteriasController : Controller
    {
        public int UserGuid => int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value);

        public readonly CriteriaRepository criteriaRepository;
        public readonly CriteriaAdvertRepository criteriaAdvertRepository;

        public CriteriasController(CriteriaRepository criteriaRepository, CriteriaAdvertRepository criteriaAdvertRepository)
        {
            this.criteriaRepository = criteriaRepository;
            this.criteriaAdvertRepository= criteriaAdvertRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Criterias>> getAllCriterias()
        {
            return Ok(criteriaRepository.All());
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateCriteria(Criterias criteria)
        {
            criteriaRepository.Add(criteria);
            criteriaRepository.Save();
            return Ok();
        }

        [Authorize]
        [HttpPost("linkCriteriAdvert")]
        public ActionResult createRelation(CriteriaAdvertPayload criteriaAdvert)
        {
            criteriaAdvertRepository.Add(criteriaAdvertRepository.iniCriteriaAdvert(criteriaAdvert));
            criteriaAdvertRepository.Save();
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public ActionResult DeleteCriteria(int id)
        {
            Criterias criteriaToDelete = criteriaRepository.FromId(id);
            if(criteriaToDelete == null)
            {
                return NotFound();
            }


            return Ok();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult clearRelation(int criteriaAdvertId)
        {
            CriteriaAdvert relationToDelete = criteriaAdvertRepository.FromId(criteriaAdvertId);
            if(relationToDelete == null)
            {
                return NotFound();
            }
            criteriaAdvertRepository.Delete(relationToDelete);
            criteriaAdvertRepository.Save();
            return Ok();
        }
    }
}
