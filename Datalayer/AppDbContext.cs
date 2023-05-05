using Entitylayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datalayer
{
    public class AppDbContext: DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            if (!optionBuilder.IsConfigured)
            {
                optionBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=WhoAmI;Trusted_Connection=True;");
                base.OnConfiguring(optionBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
        }
        public void seed()
        {
            Users.Add(new User("Jonathan", "Öberg", "sOlstrale", 1));
            Users.Add(new User("Robert", "Keränen", "SaSforLajf", 1));
            Users.Add(new User("Danny", "Mansour", "KingsbackHood", 1));
            Users.Add(new User("Daniel", "Yar", "HogreStudier", 2));

            SaveChanges();
        }
    }
}