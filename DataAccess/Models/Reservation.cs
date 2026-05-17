namespace DataAccess.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int NbPlaces { get; set; }
        public DateTime DateReservation { get; set; } = DateTime.Now;
        public string Statut { get; set; } = "En attente";
        public decimal Prix { get; set; }

        // Passager
        public int ?PassagerId { get; set; }
        public Utilisateur ?Passager { get; set; }

        // Trajet
        public int TrajetId { get; set; }
        public Trajet Trajet { get; set; }

    }
}

   
    
    
    
 