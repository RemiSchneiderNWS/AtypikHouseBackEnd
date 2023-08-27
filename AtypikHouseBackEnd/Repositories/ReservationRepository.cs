using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database;
using Payloads;

namespace Repositories;

public class ReservationRepository : Repository<Reservation>
{
    private readonly AdvertRepository advertRepository;
    private readonly UserRepository userRepository;
    public ReservationRepository(AppDbContext ctx, AdvertRepository advertRepository, UserRepository userRepository) : base(ctx)
    {
        Set = ctx.Reservations;
        this.advertRepository = advertRepository;
        this.userRepository = userRepository;
    }

    public IEnumerable<Reservation> reservationsByAdvert(int id)
    {
        return Set.Include(i => i.User).Where( reserve => reserve.Advert.Id == id);
    }
    public Reservation getReserveTovalid(ValidReserve validReserve)
    {
        return Set.Include(i => i.User).First(i => i.Advert.Id == validReserve.advertId && i.DateStart == validReserve.DateStart && i.DateEnd == validReserve.DateEnd) ;
    }

    public IEnumerable<Reservation> reservationForUser(int id)
    {
        return Set.Include(i => i.User).Include(i => i.Advert).Where(reserve => reserve.User.Id == id);
    }



    public Reservation iniReservation(ReservationPayload reservationPayload, int IdUser)
    {
        Reservation reservation = new Reservation();
        reservation.Advert = advertRepository.FromId(reservationPayload.advertId);
        reservation.User = userRepository.FromId(IdUser);
        reservation.DateStart = reservationPayload.DateStart;
        reservation.DateEnd = reservationPayload.DateEnd;
      
        reservation.Payment = false;
        DateTime currentDate = DateTime.Now;
        reservation.CreatedAt = currentDate;
        currentDate = currentDate.AddMinutes(15);
        string timer = $"{currentDate:yyyy-MM-dd}-{currentDate:HH:mm}";

        reservation.PaymentTimer = timer;
        reservation.AdvTenants = reservationPayload.AdvTenants;
        reservation.DelTenant = false;
        reservation.DelOwner = false; 
        
        return reservation;
    }

    
}