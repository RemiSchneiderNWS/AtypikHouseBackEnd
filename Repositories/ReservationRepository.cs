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
    public ReservationRepository(AppDbContext ctx, AdvertRepository advertRepository) : base(ctx)
    {
        Set = ctx.Reservations;
        this.advertRepository = advertRepository;
    }

    public IEnumerable<Reservation> reservationsByAdvert(int id)
    {
        return Set.Where( reserve => reserve.Advert.Id == id);
    }
    public Reservation iniReservation(ReservationPayload reservationPayload)
    {
        Reservation reservation = new Reservation();
        reservation.Advert = advertRepository.FromId(reservationPayload.advertId);
        reservation.DateStart = reservationPayload.DateStart;
        reservation.DateEnd = reservationPayload.DateEnd;
        reservation.Payment = reservationPayload.Payment;
        reservation.PaymentTimer = reservationPayload.PaymentTimer;
        reservation.AdvTenants = reservationPayload.AdvTenants;
        reservation.DelTenant = reservationPayload.DelTenant;
        reservation.DelOwner = reservationPayload.DelOwner;
        //ATTENTION AUX VALEUR PAR DEFAUT
        return reservation;
    }
}