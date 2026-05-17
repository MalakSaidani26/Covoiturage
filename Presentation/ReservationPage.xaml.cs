using System.Windows;
using System.Windows.Controls;
using DataAccess.Data;
using DataAccess.Models;
using Services;

namespace Presentation
{
    public partial class ReservationPage : Page
    {
        private TrajetService trajetService;
        private ReservationService reservationService;

        private int currentUserId => LoginPage.CurrentUser?.Id ?? 0;

        public ReservationPage()
        {
            InitializeComponent();
            var context = new CovoiturageContext();
            trajetService = new TrajetService(context);
            reservationService = new ReservationService(context);

            LoadTrajets();
        }

        private void LoadTrajets()
        {
            // Utilisation de TrajetService.GetTrajets()
            trajetGrid.ItemsSource = trajetService.GetTrajets();
        }

        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
            if (LoginPage.CurrentUser == null)
            {
                MessageBox.Show("Veuillez vous connecter ❌");
                return;
            }

            if (trajetGrid.SelectedItem is not Trajet t)
            {
                MessageBox.Show("Choisissez un trajet ❌");
                return;
            }

            if (!int.TryParse(txtPlaces.Text, out int nbPlaces) || nbPlaces <= 0)
            {
                MessageBox.Show("Nombre de places invalide ❌");
                return;
            }

            if (!decimal.TryParse(txtPrix.Text, out decimal prix) || prix <= 0)
            {
                MessageBox.Show("Prix invalide ❌");
                return;
            }

            if (t.Voiture == null)
            {
                MessageBox.Show("Ce trajet n'a pas de voiture ❌");
                return;
            }

            // Calcul des places restantes via ReservationService.GetReservationsByTrajetId()
            var reservationsTrajet = reservationService.GetReservationsByTrajetId(t.Id);
            int placesReservees = reservationsTrajet.Sum(r => r.NbPlaces);
            int placesRestantes = t.Voiture.NbPlacesTotal - placesReservees;

            if (nbPlaces > placesRestantes)
            {
                MessageBox.Show($"Places insuffisantes ❌ (Restant: {placesRestantes})");
                return;
            }

            var reservation = new Reservation
            {
                TrajetId = t.Id,
                PassagerId = currentUserId,
                NbPlaces = nbPlaces,
                Prix = prix,
                DateReservation = DateTime.Now,
                Statut = "En attente"
            };

            // Utilisation de ReservationService.AjouterReservation()
            reservationService.AjouterReservation(reservation);

            MessageBox.Show("Réservation envoyée ✔️");

            txtPlaces.Text = "";
            txtPrix.Text = "";
            LoadTrajets();
        }

        private void MesReservations_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow)
                .MainFrame.Navigate(new MesReservationsPage());
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow)
                .MainFrame.Navigate(new HomePage());
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginPage.CurrentUser = null;
            MessageBox.Show("Déconnecté ✔️");
            ((MainWindow)Application.Current.MainWindow)
                .MainFrame.Navigate(new LoginPage());
        }
    }
}