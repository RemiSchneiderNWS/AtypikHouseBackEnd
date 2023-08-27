using Database.ModelDefinitions;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class PGSQLAppDbContext : AppDbContext
    {
        public PGSQLAppDbContext() { }

        public PGSQLAppDbContext(DbContextOptions<AppDbContext> options) : base(options)
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
    }
}
