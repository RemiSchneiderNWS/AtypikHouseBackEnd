using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelDefinitions;

internal static class CriteriaAdvertDefinition
{
    public static void Build(ModelBuilder modelBuilder)
    {
        EntityTypeBuilder<CriteriaAdvert> b = modelBuilder.Entity<CriteriaAdvert>();

        b.HasKey(x => x.Id);
        b.HasOne( x => x.Criteria);
        b.HasOne(x => x.Advert);

    }
}