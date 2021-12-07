using eShop.DataAccess.Configurations;
using eShop.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Reflection;

namespace eShop.DataAccess
{
    public class EshopDbContext:DbContext
    {

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Credential> Credentials { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<eShop.Model.Image> Images { get; set; }      


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<User>(new UserConfiguration());
            modelBuilder.ApplyConfiguration<Category>(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration<City>(new CityConfiguration());
            modelBuilder.ApplyConfiguration<Country>(new CountryConfiguration());
            modelBuilder.ApplyConfiguration<Credential>(new CredentialConfiguration());
            modelBuilder.ApplyConfiguration<Order>(new OrderConfiguration());
            modelBuilder.ApplyConfiguration<OrderDetails>(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration<Product>(new ProductConfiguration());
            modelBuilder.ApplyConfiguration<State>(new StateConfiguration());
            modelBuilder.ApplyConfiguration<eShop.Model.Image>(new ImageConfiguration());
            modelBuilder.Ignore<AppSettings>();
            
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.LoadFrom(@"C:\Users\pa_na\source\repos\eShop\eShop.Model\bin\Debug\netcoreapp3.0\eShop.Model.dll"));
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.LoadFrom(@"E:\eShop\eShop.Model\bin\Debug\netcoreapp3.0\eShop.Model.dll"));
            //modelBuilder.ApplyConfiguration(new City());
            //if (Users.ToList().Count == 0)
            //{
            //    modelBuilder.Entity<User>().HasData(
            //        new User
            //        {
            //            AddressNo1 = "AdminAdrr",
            //            Country = new Country { Id = 1, Name = "Greece" },
            //            Credential = new Credential() { Password = "admin", Salt = PasswordGenerator.GetSalt(),}

            //        }); 
            //}
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appSettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("Conn");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
