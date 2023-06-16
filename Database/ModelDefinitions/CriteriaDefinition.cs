using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelDefinitions;

internal static class CriteriaDefinition
{
    public static void Build(ModelBuilder modelBuilder)
    {
        EntityTypeBuilder<Criterias> b = modelBuilder.Entity<Criterias>();

        b.HasKey(x => x.Id);
        b.Property( x => x.Name);
      


    }
}