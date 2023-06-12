using AtypikHouseBackEnd.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AtypikHouseBackEnd.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(local)\\sqlexpress;Database=AtypikHouse;Integrated Security=True;TrustServerCertificate=true");
        }
        public DbSet<Activity> activity { get; set; }
      

    }
}
