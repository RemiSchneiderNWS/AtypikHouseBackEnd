using Database.ModelDefinitions;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public abstract class AppDbContext : DbContext
    {
        public DbSet<Activity> Activity { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Advert> Adverts { get; set; }  
        public DbSet<Commentarys> Commentarys { get; set; }

        public DbSet<Criterias> Criterias { get; set; }

        public DbSet<Reservation> Reservations { get; set; }    

        public DbSet<ActivityImage> ActivityImages { get; set; }
        public DbSet<AdvertImage> AdvertImages { get; set; }
        public DbSet<CriteriaAdvert> CriteriaAdverts { get; set; }  
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ActivityDefinition.Build(modelBuilder);
            UserDefinition.Build(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
