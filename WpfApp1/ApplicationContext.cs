using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class ApplicationContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<UserProfile> Profiles { get; set; }
        public DbSet<UserCredential> Credentials { get; set; }

        public ApplicationContext()
        {

            Database.EnsureCreated();
        }
        public bool CheckCredentials(string log, string pass)
        {
            return Credentials.Any(u => u.Login == log && u.Password == pass);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-5BDQ3HL0;Initial Catalog=BookStore;" +
                "Integrated Security=True;Connect Timeout=30;Encrypt=False;" +
                "Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
}
