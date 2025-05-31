
using ApiAspNet.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace ApiAspNet.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // in memory database used for simplicity, change to a real db for 
            //production applications;
            options.UseNpgsql(Configuration.GetConnectionString("WebApiDB"));
        }

        public DbSet<User> Users { get; set; }    
        public DbSet<Flotte> Flottes { get; set; }
        /*public DbSet<Voyage> Voyages { get; set; }*/
        public DbSet<Gestionnaire> Gestionnaires { get; set; }
        public DbSet<Chauffeur> Chauffeurs { get; set; }
        public DbSet<Client> Clients { get; set; }
        /*public DbSet<Offre> Offres { get; set; }*/
        public DbSet<Agence> Agences { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
