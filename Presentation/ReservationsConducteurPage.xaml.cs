using System.Windows;
using System.Windows.Controls;
using DataAccess.Data;
using DataAccess.Models;
using Services;

namespace Presentation
{
    public partial class ReservationsConducteurPage : Page
    {
        private ReservationService reservationService;

        public ReservationsConducteurPage()
        {
            InitializeComponent();
            var context = new CovoiturageContext();
            reservationService = new ReservationService(context);
            LoadData();
        }

        private void LoadData()
        {
            int conducteurId = LoginPage.CurrentUser.Id;

            // Utilisation de ReservationService.GetReservations() filtrées par conducteur
            resGrid.ItemsSource = reservationService.GetReservations()
                .Where(r => r.Trajet?.ConducteurId == conducteurId)
                .ToList();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (resGrid.SelectedItem is Reservation r)
            {
                // Utilisation de ReservationService.MettreAJourStatut()
                reservationService.MettreAJourStatut(r.Id, "Confirmée");
                LoadData();
            }
            else
            {
                MessageBox.Show("Sélectionnez une réservation ❌");
            }
        }

        private void Refuse_Click(object sender, RoutedEventArgs e)
        {
            if (resGrid.SelectedItem is Reservation r)
            {
                // Utilisation de ReservationService.MettreAJourStatut()
                reservationService.MettreAJourStatut(r.Id, "Annulée");
                LoadData();
            }
            else
            {
                MessageBox.Show("Sélectionnez une réservation ❌");
            }
        }
    }
}