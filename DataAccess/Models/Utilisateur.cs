namespace DataAccess.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Email { get; set; }
        public string MotDePasse { get; set; }
        public string Telephone { get; set; }
        public string Role { get; set; } = "Passager"; // Passager ou Conducteur
         // Relations 1-N
        public List<Voiture> Voitures { get; set; } = new();
        public List<Trajet> TrajetsConducteur { get; set; } = new();
        public List<Reservation> ReservationsPassager { get; set; } = new();
        
    }
}