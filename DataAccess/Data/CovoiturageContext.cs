using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
namespace DataAccess.Data
{
    public class CovoiturageContext: DbContext
    {
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Voiture> Voitures { get; set; }
        public DbSet<Trajet> Trajets { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=CovoiturageDB;Trusted_Connection=True;");
        }
    }
}
