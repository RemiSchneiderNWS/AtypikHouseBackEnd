using Database;
using Microsoft.EntityFrameworkCore;

namespace UnitTests;

public class InMemoryAppDbContext : AppDbContext
{
    public InMemoryAppDbContext() { }

    public InMemoryAppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseInMemoryDatabase("TestsDatabase");
    }
}