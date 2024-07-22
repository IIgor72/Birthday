using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Birthday.Model;
using Microsoft.EntityFrameworkCore;

namespace Birthday
{
    internal class BirthdayDbContext : DbContext
    {
        public DbSet<BirthdayEntry> BirthdayEntries { get; set; }
        public BirthdayDbContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BirthdayDb;Trusted_Connection=True;");
        }
    }
}
