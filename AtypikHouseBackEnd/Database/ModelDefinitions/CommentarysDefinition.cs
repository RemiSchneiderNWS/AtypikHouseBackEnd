using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelDefinitions;

internal static class CommentarysDefinition
{
    public static void Build(ModelBuilder modelBuilder)
    {
        EntityTypeBuilder<Commentarys> b = modelBuilder.Entity<Commentarys>();

        b.HasKey(x => x.Id);
        b.Property(x => x.Text);
        b.HasOne( x => x.Advert);
        b.HasOne(x => x.User);

    }
}