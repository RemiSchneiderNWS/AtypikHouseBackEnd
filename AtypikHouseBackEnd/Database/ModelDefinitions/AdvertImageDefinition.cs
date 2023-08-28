using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelDefinitions;

internal static class AdvertImageDefinition
{
    public static void Build(ModelBuilder modelBuilder)
    {
        EntityTypeBuilder<AdvertImage> b = modelBuilder.Entity<AdvertImage>();

        b.HasKey(x => x.Id);
        b.Property(x => x.Name);
        b.Property(x => x.Order);
        b.HasOne(x => x.Advert);

    }
}