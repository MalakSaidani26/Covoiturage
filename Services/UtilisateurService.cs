using DataAccess.Data;
using DataAccess.Models;
namespace Services
{
    public class UtilisateurService
    {
        private readonly CovoiturageContext _dbContext;
        public UtilisateurService(CovoiturageContext dbContext)
        {
            _dbContext = dbContext;
        }
        // Récupérer un utilisateur par son ID
        public Utilisateur GetUtilisateurById(int id)
        {
            var sql = from u in _dbContext.Utilisateurs
                      where u.Id == id
                      select u;
            return sql.FirstOrDefault();
        }
        // Récupérer tous les utilisateurs 
        public List<Utilisateur> GetUtilisateurs()
        {

            var sql = from u in _dbContext.Utilisateurs
                      select u;
            return sql.ToList();

        }

        // Ajouter un nouvel utilisateur
        public void AjouterUtilisateur(Utilisateur utilisateur)
        {
            _dbContext.Utilisateurs.Add(utilisateur);
            _dbContext.SaveChanges();
        }

        // Mettre à jour les informations d'un utilisateur

        public void MettreAjout(Utilisateur utilisateur)
        {

            var recherche = _dbContext.Utilisateurs.Find(utilisateur.Id);
            if (recherche != null)
            {
                recherche.Nom = utilisateur.Nom;
                recherche.Prenom = utilisateur.Prenom;
                recherche.Telephone = utilisateur.Telephone;
                recherche.Role = utilisateur.Role;
                _dbContext.SaveChanges();

            }

        }
        // Supprimer un utilisateur
        public void SupprimerUtilisateur(int id)
        {
            var utilisateur = _dbContext.Utilisateurs.Find(id);
            if (utilisateur != null)
            {
                _dbContext.Utilisateurs.Remove(utilisateur);
                _dbContext.SaveChanges();
            }


        }
        // recuperer les utilisateurs qui sont des conducteurs
        public List<Utilisateur> GetConducteurs()
        {
            var sql = from u in _dbContext.Utilisateurs
                      where u.Role == "Conducteur"
                      select u;
            return sql.ToList();
        }
        // recuperer les utilisateurs qui sont des passagers
        public List<Utilisateur> GetPassagers()
        {
            var sql = from u in _dbContext.Utilisateurs
                      where u.Role == "Passager"
                      select u;
            return sql.ToList();
        }
    }
}

