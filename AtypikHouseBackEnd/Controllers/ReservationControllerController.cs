using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using System.Security.Claims;

namespace AtypikHouseBackEnd.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ReservationController : Controller
    {
        public int UserGuid => int.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value);

        public readonly ReservationRepository reservationRepository;

        public ReservationController(ReservationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<Reservation> GetReserveById(int id)
        {
            Reservation reserve = reservationRepository.FromId(id);
            if (reserve == null)
            {
                return BadRequest("Reservation introuvable");
            }
            return (reserve);
        }
        [Authorize]
        [HttpGet("getReserveByAdvert")]
        public ActionResult<IEnumerable<Reservation>> GetReservesByAdvert(int id) {

            IEnumerable<Reservation> reservationsByAdvert = reservationRepository.reservationsByAdvert(id);
            if (reservationsByAdvert.Count() == 0)
            {
                return BadRequest("Aucune Réservation pour cette annonce");

            }

            return Ok();
        }

        [Authorize]
        [HttpPost]
        public ActionResult createReserve(Reservation reservation)
        {
            reservationRepository.Add(reservation);
            reservationRepository.Save();
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult deleteReserve(int id)
        {
            Reservation reserveToDelete = reservationRepository.FromId(id);
            if (reserveToDelete == null)
            {
                return BadRequest("Reservation introuvable");
            }

            if (reserveToDelete.User.Id != UserGuid)
            {
                return BadRequest("Vous n'avez pas l'autorisation de supprimé cette réservation");
            }
            reservationRepository.Delete(reserveToDelete);
            reservationRepository.Save();
            return Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult validReserve(int id) {
            Reservation reserveTovalidate = reservationRepository.FromId(id);
            if (reserveTovalidate == null)
            {
                return BadRequest("Reservation introuvable");

            }
            if(reserveTovalidate.User.Id != UserGuid)
            {
                return BadRequest("Non authorisé");
            }
            reserveTovalidate.Payment = true;
            reservationRepository.Update(reserveTovalidate);
            reservationRepository.Save();
            return Ok();
        }

    }
}
