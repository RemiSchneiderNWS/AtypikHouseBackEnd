using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using System.Security.Claims;

namespace AtypikHouseBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentaryController : Controller
    {
        private readonly CommentaryRepository commmentaryRepository;
        public int UserGuid => int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
        public CommentaryController(CommentaryRepository commmentaryRepository)
        {
            this.commmentaryRepository = commmentaryRepository;
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
        public ActionResult CreateCommentary(Commentarys comment)
        {
            commmentaryRepository.Add(comment);
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
