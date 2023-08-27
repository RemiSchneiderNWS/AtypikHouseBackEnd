using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelDefinitions;

internal static class ActivityDefinition
{
    public static void Build(ModelBuilder modelBuilder)
    {
        EntityTypeBuilder<Activity> b = modelBuilder.Entity<Activity>();
            
        b.HasKey(x => x.Id);
        b.Property(x => x.Name);
        b.Property(x => x.Adress);
        b.Property(x => x.City);
        b.Property(x => x.Postal);
        b.Property(x => x.Describe);
        b.HasOne(x => x.User);
        b.HasOne(x => x.Advert);
    }
}