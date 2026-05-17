using DataAccess.Data;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class ReservationService
    {
        private readonly CovoiturageContext _dbContext;

        public ReservationService(CovoiturageContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Récupérer toutes les réservations avec passager, trajet et conducteur du trajet
        public List<Reservation> GetReservations()
        {
            var sql = from r in _dbContext.Reservations
                        .Include(r => r.Passager)
                        .Include(r => r.Trajet)
                            .ThenInclude(t => t.Conducteur)   
                        .Include(r => r.Trajet)
                            .ThenInclude(t => t.Voiture)
                      select r;
            return sql.ToList();
        }

        // Récupérer les réservations d'un trajet spécifique
        public List<Reservation> GetReservationsByTrajetId(int trajetId)
        {
            var sql = from r in _dbContext.Reservations
                        .Include(r => r.Passager)
                        .Include(r => r.Trajet)
                            .ThenInclude(t => t.Conducteur)   
                        .Include(r => r.Trajet)
                            .ThenInclude(t => t.Voiture)
                      where r.TrajetId == trajetId
                      select r;
            return sql.ToList();
        }

        // Récupérer les réservations d'un passager — avec le conducteur du trajet inclus
        public List<Reservation> GetReservationsByPassagerId(int passagerId)
        {
            var sql = from r in _dbContext.Reservations
                        .Include(r => r.Passager)
                        .Include(r => r.Trajet)
                            .ThenInclude(t => t.Conducteur)   
                        .Include(r => r.Trajet)
                            .ThenInclude(t => t.Voiture)
                      where r.PassagerId == passagerId
                      select r;
            return sql.ToList();
        }

        // Récupérer une réservation par son id
        public Reservation GetReservationById(int id)
        {
            var sql = from r in _dbContext.Reservations
                        .Include(r => r.Passager)
                        .Include(r => r.Trajet)
                            .ThenInclude(t => t.Conducteur)   
                        .Include(r => r.Trajet)
                            .ThenInclude(t => t.Voiture)
                      where r.Id == id
                      select r;
            return sql.FirstOrDefault();
        }

        // Ajouter une réservation
        public void AjouterReservation(Reservation reservation)
        {
            _dbContext.Reservations.Add(reservation);
            _dbContext.SaveChanges();
        }

        // Supprimer une réservation
        public void SupprimerReservation(int id)
        {
            var reservation = _dbContext.Reservations.Find(id);
            if (reservation != null)
            {
                _dbContext.Reservations.Remove(reservation);
                _dbContext.SaveChanges();
            }
        }

        // Mettre à jour une réservation 
        public void MettreAJour(Reservation reservation)
        {
            var recherche = _dbContext.Reservations.Find(reservation.Id);
            if (recherche != null)
            {
                recherche.NbPlaces = reservation.NbPlaces;
                recherche.DateReservation = reservation.DateReservation;
                recherche.Statut = reservation.Statut;
                recherche.Prix = reservation.Prix;
                _dbContext.SaveChanges();
            }
        }

        // Mettre à jour le statut d'une réservation uniquement
        public void MettreAJourStatut(int reservationId, string statut)
        {
            var recherche = _dbContext.Reservations.Find(reservationId);
            if (recherche != null)
            {
                recherche.Statut = statut;
                _dbContext.SaveChanges();
            }
        }
    }
}