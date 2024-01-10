using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LibraryWPF
{
    internal class LibraryContext : DbContext
    {
        public DbSet<Book> books { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Reservation> reservations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=dbtest;Username=postgres;Password=123;");
        }
    }
}
