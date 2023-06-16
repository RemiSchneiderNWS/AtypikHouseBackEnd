using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelDefinitions;

internal static class AdvertDefinition
{
    public static void Build(ModelBuilder modelBuilder)
    {
        EntityTypeBuilder<Advert> b = modelBuilder.Entity<Advert>();
            
        b.HasKey(x => x.Id);
        b.Property(x => x.Name);
        b.Property(x => x.Type);
        b.Property(x => x.Tenants);
        b.Property(x => x.Status);
        b.Property(x => x.Up);
        b.Property(x => x.CriLimit);
        b.Property(x => x.Created_at);
        b.Property(x => x.Price);
        b.Property(x => x.City);
        b.Property(x => x.Postal);
        b.Property(x => x.Describe);
        b.HasOne(x => x.User);
        //public int UsrId { get; set; } 
    }
}