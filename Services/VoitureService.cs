using DataAccess.Data;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
namespace Services
{
    public class VoitureService
    {
        
        private readonly CovoiturageContext _dbContext;
        public VoitureService(CovoiturageContext dbContext)
        {
            _dbContext = dbContext;
        }

        //recuperer une voiture par son id
        public Voiture GetVoitureById(int id)
        {
            var sql = from v in _dbContext.Voitures
                        .Include(v => v.Utilisateur)
                      where v.Id == id
                      select v;
            return sql.FirstOrDefault();
        }
        //recuperer toutes les voitures avec les informations de leurs propriétaires
        public List<Voiture> GetVoitures()
        {
            var sql = from v in _dbContext.Voitures
                        .Include(v => v.Utilisateur)
                      select v;
            return sql.ToList();
        }
        // recuperer les voitures d'un utilisateur
        public List<Voiture> GetVoituresByUtilisateurId(int utilisateurId)
        {
            var sql = from v in _dbContext.Voitures
                        .Include(v => v.Utilisateur)
                      where v.UtilisateurId == utilisateurId
                      select v;
            return sql.ToList();
        }
        //ajouter une voiture
        public void AjouterVoiture(Voiture voiture)
        {
            _dbContext.Voitures.Add(voiture);
            _dbContext.SaveChanges();
        }
        //mettre à jour les informations d'une voiture
        public void MettreAjout(Voiture voiture)
        {
            var recherche = _dbContext.Voitures.Find(voiture.Id);
            if (recherche != null)
            {
                recherche.Marque = voiture.Marque;
                recherche.Modele = voiture.Modele;
                recherche.Couleur = voiture.Couleur;
                recherche.Immatriculation = voiture.Immatriculation;
                recherche.NbPlacesTotal = voiture.NbPlacesTotal;
                recherche.Description = voiture.Description;
                _dbContext.SaveChanges();
            }
        }

        //supprimer une voiture
        public void SupprimerVoiture(int id)
        {
            var voiture = _dbContext.Voitures.Find(id);
            if (voiture != null)
            {
                _dbContext.Voitures.Remove(voiture);
                _dbContext.SaveChanges();
            }
        }
    }
}
