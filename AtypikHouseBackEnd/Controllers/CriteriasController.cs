using Microsoft.AspNetCore.Mvc;
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

        public CriteriasController(CriteriaRepository criteriaRepository)
        {
            this.criteriaRepository = criteriaRepository;

        }

        [HttpGet]
        public ActionResult Get()
        {

        }
    }
}
