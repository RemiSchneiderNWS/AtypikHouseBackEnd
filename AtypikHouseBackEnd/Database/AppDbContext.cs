using Database.ModelDefinitions;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class AppDbContext : DbContext
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            string connectionString = "Host=localhost;Database=AtypikHouse;Username=postgres;Password=866e72d0;Port=49174";

            optionsBuilder.UseNpgsql(connectionString, options =>
            {
                options.MigrationsAssembly("AtypikHouseBackEnd"); 
                options.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
            });

          
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ActivityDefinition.Build(modelBuilder);
            UserDefinition.Build(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
