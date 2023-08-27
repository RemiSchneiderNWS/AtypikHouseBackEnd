using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelDefinitions;

internal static class ActivityImageDefinition
{
    public static void Build(ModelBuilder modelBuilder)
    {
        EntityTypeBuilder<ActivityImage> b = modelBuilder.Entity<ActivityImage>();
            
        b.HasKey(x => x.Id);
        b.Property(x => x.Name);
        b.Property(x => x.Order);
        b.Property(x => x.Activity);

    }
}