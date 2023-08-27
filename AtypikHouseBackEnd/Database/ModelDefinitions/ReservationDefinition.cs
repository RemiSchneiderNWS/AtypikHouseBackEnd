using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelDefinitions;

internal static class ReservationDefinition
{
    public static void Build(ModelBuilder modelBuilder)
    {
        EntityTypeBuilder<Reservation> b = modelBuilder.Entity<Reservation>();

        b.HasKey(x => x.Id);
        b.Property( x => x.DateStart);
        b.Property(x => x.DateEnd);
        b.Property(x => x.CreatedAt);
        b.Property(x => x.Payment);
        b.Property(x => x.PaymentTimer);
        b.Property(x => x.AdvTenants);
        b.Property(x => x.DelTenant);
        b.Property(x => x.DelOwner);
        b.HasOne(x => x.User);
        b.HasOne(x => x.Advert);



    }
}