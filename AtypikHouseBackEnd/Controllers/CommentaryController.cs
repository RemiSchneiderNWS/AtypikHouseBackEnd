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
    public class CommentaryController : Controller
    {
        private readonly CommentaryRepository commmentaryRepository;
        private readonly UserRepository userRepository;
        public int UserGuid => int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
        public CommentaryController(CommentaryRepository commmentaryRepository, UserRepository userRepository)
        {
            this.commmentaryRepository = commmentaryRepository;
            this.userRepository = userRepository;
        }

        //[Authorize]
        [HttpGet("getCommentarybyAdvert/{idAdvert}")]
        public ActionResult<IEnumerable<Commentarys>> GetCommentarybyAdvert(int idAdvert) {

            IEnumerable<Commentarys> CommentarysOfAdvert = commmentaryRepository.GetByAdvert(idAdvert);
            if (CommentarysOfAdvert.Count() == 0)
            {
                return BadRequest("Aucun commentaires liés à cette annonce");
            }
            return Ok(CommentarysOfAdvert);
        }
        [Authorize]
        [HttpPost]
        public ActionResult CreateCommentary(CommentaryPayload commentaryPayload)
        {
            Commentarys commentarys = commmentaryRepository.iniCommentary(commentaryPayload);
            if(commentarys.Advert == null) {
                return NotFound("Annonce associée introuvable");
            }
            commentarys.User = userRepository.FromId(UserGuid);
            if(commentarys.User == null)
            {
                return NotFound("Utilisateur associée introuvable");
            }
            commmentaryRepository.Add(commentarys);
            commmentaryRepository.Save();
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult DeleteCommentary( int id)
        {
           Commentarys commentarysToDelete =  commmentaryRepository.FromId(id);
            
           if(UserGuid != commentarysToDelete.User.Id)
            {
                return BadRequest("Vous n'étes pas authorizé à effectuer cette action");

            }
           commmentaryRepository.Delete(commentarysToDelete);
           commmentaryRepository.Save();
            return Ok();
        }
    }
}
