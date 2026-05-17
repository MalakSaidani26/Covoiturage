using System.Windows;
using System.Windows.Controls;
using DataAccess.Data;
using DataAccess.Models;
using Services;

namespace Presentation
{
    public partial class MesReservationsPage : Page
    {
        private ReservationService _reservationService;

        public MesReservationsPage()
        {
            InitializeComponent();
            var context = new CovoiturageContext();
            _reservationService = new ReservationService(context);
            LoadData();
        }

        private void LoadData()
        {
            int userId = LoginPage.CurrentUser.Id;

            
            resGrid.ItemsSource = _reservationService.GetReservationsByPassagerId(userId);
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (resGrid.SelectedItem is Reservation r)
            {
                _reservationService.SupprimerReservation(r.Id);
                LoadData();
            }
            else
            {
                MessageBox.Show("Sélectionnez une réservation ❌");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow)
                .MainFrame.Navigate(new ReservationPage());
        }
    }
}