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

            //Many to Many förhållande mellan PersonalData och Company!
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
                PersonalDataSet.Add(new PersonalData() { DataCategories = category, DataName = "FirstName", DataValue = "Jonathan", User = user });
                PersonalDataSet.Add(new PersonalData() { DataCategories = category, DataName = "LastName", DataValue = "Öberg", User = user });
                PersonalDataSet.Add(new PersonalData() { DataCategories = category, DataName = "Address", DataValue = "Vistvägen 15", User = user });
                PersonalDataSet.Add(new PersonalData() { DataCategories = category, DataName = "City", DataValue = "Ulricehamn", User = user });
                PersonalDataSet.Add(new PersonalData() { DataCategories = category, DataName = "PostalCode", DataValue = "523 30", User = user });
                PersonalDataSet.Add(new PersonalData() { DataCategories = category, DataName = "Personal identity number", DataValue = "19550505-8951", User = user });
                PersonalDataSet.Add(new PersonalData() { DataCategories = category, DataName = "E-mail", DataValue = "Jonathan.Öberg@gmail.com", User = user });
                PersonalDataSet.Add(new PersonalData() { DataCategories = category, DataName = "Phone", DataValue = "0708941399", User = user });
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