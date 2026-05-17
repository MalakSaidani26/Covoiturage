using DataAccess.Data;

namespace Services
{
    public class StatistiquesService
    {
        private readonly CovoiturageContext _dbContext;

        public StatistiquesService(CovoiturageContext dbContext)
        {
            _dbContext = dbContext;
        }

        //UTILISATEURS 

        //Nombre total d'utilisateurs
        public int NombreTotalUtilisateurs()
        {
            var sql = from u in _dbContext.Utilisateurs
                      select u;
                     
                     return sql.Count();
        }

        // Nombre total de conducteurs
        public int NombreTotalConducteurs()
        {
            var sql = from u in _dbContext.Utilisateurs
                      where u.Role == "Conducteur"
                      select u;

            return sql.Count();
        }

        // Nombre total de passagers
        public int NombreTotalPassagers()
        {
            var sql = from u in _dbContext.Utilisateurs
                      where u.Role == "Passager"
                      select u;

            return sql.Count();
        }

        // TRAJETS

        // Nombre total de trajets
        public int NombreTotalTrajets()
        {
            var sql = from t in _dbContext.Trajets
                      select t;

            return sql.Count();
        }

        // Nombre de trajets à venir
        public int NombreTotalTrajetsAVenir()
        {
            var sql = from t in _dbContext.Trajets
                      where t.DateDepart > DateTime.Now
                      select t;

            return sql.Count();
        }

        // Nombre de trajets passés
        public int NombreTotalTrajetsPasses()
        {
            var sql =   from t in _dbContext.Trajets
                    where t.DateDepart < DateTime.Now
                    select t;

            return sql.Count();
        }

        //Top 5 des destinations les plus populaires
        public List<string> DestinationsPopulaires()
        {
            var sql = from t in _dbContext.Trajets
                      group t by t.AdresseArrivee into g
                      orderby g.Count() descending
                      select g.Key;

            return sql.Take(5).ToList();
        }

        // RÉSERVATIONS

        //Nombre total de réservations
        public int NombreTotalReservations()
        {
            var sql = from r in _dbContext.Reservations
                      select r;

            return sql.Count();
        }

        //Nombre de réservations confirmées
        public int NombreReservationsConfirmees()
        {
            var sql = from r in _dbContext.Reservations
                      where r.Statut == "Confirmée"
                      select r;

            return sql.Count();
        }

        //Nombre de réservations en attente
        public int NombreReservationsEnAttente()
        {
            var sql = from r in _dbContext.Reservations
                      where r.Statut == "En attente"
                      select r;

            return sql.Count();
        }

        //Nombre de réservations annulées
        public int NombreReservationsAnnulees()
        {
            var sql = from r in _dbContext.Reservations
                      where r.Statut == "Annulée"
                      select r;

            return sql.Count();
        }

        //  Top 5 des trajets les plus réservés
        public List<string> TrajetsPopulaires()
        {
            var sql = from r in _dbContext.Reservations
                      group r by r.TrajetId into g
                      orderby g.Count() descending
                      select g.Key.ToString();

            return sql.Take(5).ToList();
        }

        // FINANCE 

        //Chiffre d'affaires total (réservations confirmées)
        public decimal ChiffreAffairesTotal()
        {
            var sql = from r in _dbContext.Reservations
                      where r.Statut == "Confirmée"
                      select r.Prix;

            return sql.Sum();
        }

        // Revenu du mois en cours (réservations confirmées)
        public decimal RevenueMoisEnCours()
        {
            var sql = from r in _dbContext.Reservations
                      where r.Statut == "Confirmée"
                         && r.DateReservation.Month == DateTime.Now.Month
                       && r.DateReservation.Year == DateTime.Now.Year
                    select r.Prix;

            return sql.Sum();
        }

        //STATS PAR CONDUCTEUR 

        //Réservations d'un conducteur spécifique
        public List<DataAccess.Models.Reservation> ReservationsDuConducteur(int conducteurId)
        {
            var sql = from r in _dbContext.Reservations
                      where r.Trajet.ConducteurId == conducteurId
                      select r;

            return sql.ToList();
        }

        //Nombre de trajets d'un conducteur
        public int NombreTrajetsConducteur(int conducteurId)
        {
            var sql = from t in _dbContext.Trajets
                      where t.ConducteurId == conducteurId
                      select t;

            return sql.Count();
        }

        //CA d'un conducteur (confirmées uniquement)
        public decimal ChiffreAffairesConducteur(int conducteurId)
        {
            var sql = from r in _dbContext.Reservations
                      where r.Trajet.ConducteurId == conducteurId
                         && r.Statut == "Confirmée"
                      select r.Prix;

            return sql.Sum();
        }

        //Revenu du mois en cours pour un conducteur
        public decimal RevenueMoisConducteur(int conducteurId)
        {
            var sql = from r in _dbContext.Reservations
                      where r.Trajet.ConducteurId == conducteurId
                         && r.Statut == "Confirmée"
                         && r.DateReservation.Month == DateTime.Now.Month
                         && r.DateReservation.Year == DateTime.Now.Year
                      select r.Prix;

            return sql.Sum();
        }
    }
}