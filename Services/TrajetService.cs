using DataAccess.Data;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
namespace Services
{
    public class TrajetService
    {
        private readonly CovoiturageContext _dbContext;
        public TrajetService(CovoiturageContext dbContext)
        {
            _dbContext = dbContext;
        }
        //recupérer tous les trajets avec les informations de leurs conducteurs et voitures
            public List<Trajet> GetTrajets()
            {
                var sql = from t in _dbContext.Trajets
                            .Include(t => t.Conducteur)
                            .Include(t => t.Voiture)
                        select t;
                return sql.ToList();
        }
        //recuperer les trajets avec d'un conducteur spécifique
        public List<Trajet> GetTrajetsByConducteurId(int conducteurId)
        {
            var sql = from t in _dbContext.Trajets
                        .Include(t => t.Conducteur)
                        .Include(t => t.Voiture)
                      where t.ConducteurId == conducteurId
                      select t;
            return sql.ToList();
        }
        //recuperer les trajets avec d'une voiture spécifique
        public List<Trajet> GetTrajetsByVoitureId(int voitureId)
        {
            var sql = from t in _dbContext.Trajets
                        .Include(t => t.Conducteur)
                        .Include(t => t.Voiture)
                      where t.VoitureId == voitureId
                      select t;
            return sql.ToList();
        }
        
        //ajouter un trajet
        public void AjouterTrajet(Trajet trajet)
        {
            _dbContext.Trajets.Add(trajet);
            _dbContext.SaveChanges();
        }
        //mettre à jour les informations d'un trajet
        public void MettreAjout(Trajet trajet)
        {
            var recherche = _dbContext.Trajets.Find(trajet.Id);
            if (recherche != null)
            {
                recherche.AdresseDepart = trajet.AdresseDepart;
                recherche.AdresseArrivee = trajet.AdresseArrivee;
                recherche.DateDepart = trajet.DateDepart;
                recherche.DureeMinutes = trajet.DureeMinutes;
                recherche.ConducteurId = trajet.ConducteurId;
                recherche.VoitureId = trajet.VoitureId;
                _dbContext.SaveChanges();
            }
        }
        //supprimer un trajet
         public void SupprimerTrajet(int id) { 
            var trajet = _dbContext.Trajets.Find(id);
            if (trajet != null)
            {
                _dbContext.Trajets.Remove(trajet);
                _dbContext.SaveChanges();
            }
        }

    }
}
