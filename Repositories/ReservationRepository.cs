using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database;

namespace Repositories;

public class ReservationRepository : Repository<Reservation>
{
    public ReservationRepository(AppDbContext ctx) : base(ctx)
    {
        Set = ctx.Reservations;
    }

    public IEnumerable<Reservation> reservationsByAdvert(int id)
    {
        return Set.Where( reserve => reserve.Advert.Id == id);
    }
}