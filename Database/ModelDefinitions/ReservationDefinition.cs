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
    /*
          public int Id { get; set; }
        public User User { get; set; } = null!;
        public Advert Advert { get; set; } = null! ;      
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime CreatedAt { get;set; }      
        public Boolean Payment { get; set; }
        public string PaymentTimer {get; set; } = string.Empty;
        public int AdvTenants { get; set; }
        public Boolean DelTenant { get; set; }
        public Boolean DelOwner { get; set; }
     */
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