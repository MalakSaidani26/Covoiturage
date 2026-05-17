namespace DataAccess.Models
{
    public class Trajet
    {
        public int Id { get; set; }
        public string AdresseDepart { get; set; }
        public string AdresseArrivee { get; set; }
        public DateTime DateDepart { get; set; }
        public int DureeMinutes { get; set; }
        // Conducteur
        public int ConducteurId { get; set; }
        public Utilisateur Conducteur { get; set; }

        // Voiture
        public int VoitureId { get; set; }
        public Voiture Voiture { get; set; }

        public List<Reservation> Reservations { get; set; } = new();
        public string NomCompletConducteur
        {
            get
            {
                return Conducteur != null
                    ? Conducteur.Prenom + " " + Conducteur.Nom
                    : "";
            }
        }
    }


}
