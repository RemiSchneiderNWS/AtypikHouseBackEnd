using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.ModelDefinitions;

internal static class UserDefinition
{
    /*
     * public string Mail { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public Role Role { get; set; }

        public string FirstName {get;set;}
        
        public string LastName { get; set;}

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
        public string Expires { get; set; }
     */
    public static void Build(ModelBuilder modelBuilder)
    {
        EntityTypeBuilder<User> b = modelBuilder.Entity<User>();
            
        b.HasKey(x => x.Id);
        b.Property(x => x.Mail);
        b.Property(x => x.Password);
        b.Property(x => x.Phone);
        b.Property(x => x.Role);
        b.Property(x => x.FirstName);
        b.Property(x => x.LastName);
        b.Property(x => x.AccessToken);
        b.Property(x => x.RefreshToken);
        b.Property(x => x.ExpiresIn);
        //public int UsrId { get; set; } 
    }
}