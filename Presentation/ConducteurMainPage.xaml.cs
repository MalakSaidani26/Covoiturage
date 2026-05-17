using System.Windows;
using System.Windows.Controls;

namespace Presentation
{
    public partial class ConducteurMainPage : Page
    {
        public ConducteurMainPage()
        {
            InitializeComponent();

            // page par défaut
            MainFrame.Navigate(new DashboardPage());
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new DashboardPage());
        }

        private void Trajets_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ConducteurPage());
        }

        private void Reservations_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ReservationsConducteurPage());
        }

        private void Voitures_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new VoiturePage());
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginPage.CurrentUser = null;

            ((MainWindow)Application.Current.MainWindow)
                .MainFrame.Navigate(new HomePage());
        }
    }
}