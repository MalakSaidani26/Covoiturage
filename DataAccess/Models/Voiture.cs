
namespace DataAccess.Models
{
    public class Voiture
    {
        public int Id { get; set; }
        public string Marque { get; set; }
        public string Modele { get; set; }
        public string Couleur { get; set; }
        public string Immatriculation { get; set; }
        public int NbPlacesTotal { get; set; }
        public string? Description { get; set; }
        public int UtilisateurId { get; set; }
        public Utilisateur Utilisateur { get; set; }

        public List<Trajet> Trajets { get; set; } = new();

    }
}