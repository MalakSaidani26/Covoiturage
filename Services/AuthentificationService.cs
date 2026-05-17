using DataAccess.Data;
using DataAccess.Models;

namespace Services
{
    public class AuthentificationService
    {
        private readonly CovoiturageContext _dbContext;
        public AuthentificationService(CovoiturageContext dbContext)
        {
            _dbContext = dbContext;
        }
        //vérification de l'email et du mot de passe pour l'authentification
        public Utilisateur Connexion(string email, string motdepasse)
        {
            var utilisateur = from u in _dbContext.Utilisateurs
                              where u.Email == email && u.MotDePasse == motdepasse
                              select u;

            return utilisateur.FirstOrDefault();
        }
        //vérification de l'email pour l'inscription
        public bool EmailExiste(string email)
        {
            var utlisateur = from u in _dbContext.Utilisateurs
                             where u.Email == email
                             select u;
            return utlisateur.Any();
        }
        //inscription et ajout  d'un nouvel utilisateur
        public bool Inscription(Utilisateur utilisateur)
        {
            if (EmailExiste(utilisateur.Email))
                return false; // L'email existe déjà
            _dbContext.Utilisateurs.Add(utilisateur);
            
            _dbContext.SaveChanges();
            return true;
        }
        //vérification le role de l'utilisateur passager
        public bool EstPassager(Utilisateur utilisateur)
        {
            if (utilisateur.Role == "Passager")
            {
                return true;
            }
            return false;
        }
        //vérification le role de l'utilisateur conducteur
        public bool EstConducteur(Utilisateur utilisateur)
        {
            if (utilisateur.Role == "Conducteur")
            {
                return true;
            }
            return false;
        }

    }

}