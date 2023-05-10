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
        public DbSet<DataCategory> DataCategories { get; set; } = null!;
        public DbSet<PersonalData> PersonalDataSet { get; set; } = null!;
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
            Database.EnsureDeleted();
            if (Database.EnsureCreated())
            {
                DataCategory category = new DataCategory() { CategoryName = "Personal Information", CategoryDescription = "Data that are closely connected to the individual person" };
                DataCategories.Add(category);

                User user = new User("sOlstrale", 2);
                Users.Add(user);
                PersonalDataSet.Add(new PersonalData() { DataCategories = category, DataName = "First name", DataValue = "Jonathan", User = user });
                PersonalDataSet.Add(new PersonalData() { DataCategories = category, DataName = "Last name", DataValue = "Öberg", User = user });

                user = new User("SaSforLajf", 1);
                Users.Add(user);
                PersonalDataSet.Add(new PersonalData() { DataCategories = category, DataName = "First name", DataValue = "Robert", User = user });
                PersonalDataSet.Add(new PersonalData() { DataCategories = category, DataName = "Last name", DataValue = "Keränen", User = user });

                user = new User("KingsbackHood", 1);
                Users.Add(user);
                PersonalDataSet.Add(new PersonalData() { DataCategories = category, DataName = "First name", DataValue = "Danny", User = user });
                PersonalDataSet.Add(new PersonalData() { DataCategories = category, DataName = "Last name", DataValue = "Mansour", User = user });

                SaveChanges();
            }
        }
    }
}