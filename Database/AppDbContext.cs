using Database.ModelDefinitions;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Activity> Activity { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseNpgsql("Host=localhost;Database=AtypikHouse;Username=postgres;Password=root");
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
