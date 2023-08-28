using Database.ModelDefinitions;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class PGSQLAppDbContext : AppDbContext
    {
        private readonly string ConnectionString = "Host=localhost;Database=AtypikHouse;Username=postgres;Password=866e72d0;Port=49174";

        public PGSQLAppDbContext() { }

        public PGSQLAppDbContext(string? connectionString)
        {
            if (connectionString != null)
                ConnectionString = connectionString;
        }

        public PGSQLAppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseNpgsql(ConnectionString, options =>
            {
                options.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
            });


            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
    }
}
