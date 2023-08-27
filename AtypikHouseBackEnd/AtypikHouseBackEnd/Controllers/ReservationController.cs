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
                return NotFound("Aucune Réservation pour cette annonce");

            }

            return Ok(reservationsByAdvert);
        }


        [Authorize]
        [HttpGet("canComment/{id}")]
        public bool userCanComment(int id)
        {

            IEnumerable<Reservation> reservationsByAdvert = reservationRepository.reservationsByAdvert(id);
            if (reservationsByAdvert.Count() == 0)
            {
                return false;

            }
            IEnumerable<Reservation> reservationIfUser = reservationsByAdvert.Where(x => x.User.Id == UserGuid);
            if (reservationIfUser.Count() == 0)
            {
                return false;
            }

            return true;
        }

        [Authorize]
        [HttpGet("getDatebyAdvRes/{id}")]
        public ActionResult<List<DateForReserve>> getDatebyAdvRes(int id)
        {

            IEnumerable<Reservation> reservationsByAdvert = reservationRepository.reservationsByAdvert(id);
            if (reservationsByAdvert.Count() == 0)
            {
                return NotFound("Aucune réservation pour cette annonce");

            }

            List<DateForReserve> reservationIfUser = new List<DateForReserve>();

            for (int i = 0; i < reservationsByAdvert.Count(); i++)
            {
                DateForReserve dateReserve = new DateForReserve();

                dateReserve.DateStart = reservationsByAdvert.ElementAt(i).DateStart;
                dateReserve.DateEnd = reservationsByAdvert.ElementAt(i).DateEnd;

                reservationIfUser.Add(dateReserve);
            }

            return Ok(reservationIfUser);
        }

        [Authorize]
        [HttpGet("getUserReserve")]
        public ActionResult getUserReserve()
        {

            IEnumerable<Reservation> reservationsByUser = reservationRepository.reservationForUser(UserGuid);
            if (reservationsByUser.Count() == 0)
            {
                return NotFound("Aucune réservation pour cet utilisateur");

            }
            
            return Ok(reservationsByUser);
        }

        [Authorize]
        [HttpPost("validReserve")]
        public ActionResult validReserve([FromBody] ValidReserve validReserve)
        {
           Reservation reserve =  reservationRepository.getReserveTovalid(validReserve);
            if(reserve == null)
            {
                return NotFound("Reservation Introuvable");
            }
            if (reserve.User.Id != UserGuid)
            {
                return BadRequest("Vous n'avez pas l'autorisation de d'accepter cette réservation");
            }
            reserve.Payment = true;
            reservationRepository.Update(reserve);
            reservationRepository.Save();
            return Ok();
        }


        [Authorize]
        [HttpPost("cancelReserve")]
        public ActionResult cancelReserve([FromBody] ValidReserve validReserve)
        {
            Reservation reserve = reservationRepository.getReserveTovalid(validReserve);
            if (reserve == null)
            {
                return NotFound("Reservation Introuvable");
            }
            if (reserve.User.Id != UserGuid)
            {
                return BadRequest("Vous n'avez pas l'autorisation de supprimé cette réservation");
            }
            reservationRepository.Delete(reserve);
            reservationRepository.Save();
            return Ok();
        }


        [Authorize]
        [HttpPost]
        public ActionResult createReserve(ReservationPayload reservationPayload)
        {
            Reservation reservation = reservationRepository.iniReservation(reservationPayload, UserGuid);
            reservationRepository.Add(reservation);
            reservationRepository.Save();
            return Ok(reservation);
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
        /*
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
        */
    }
}
