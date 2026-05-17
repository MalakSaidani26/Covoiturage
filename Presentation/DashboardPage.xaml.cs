using System.Windows.Controls;
using DataAccess.Data;
using Services;

namespace Presentation
{
    public partial class DashboardPage : Page
    {
        
        private StatistiquesService _statsService;

        public DashboardPage()
        {
            InitializeComponent();

            var context = new CovoiturageContext();
            _statsService = new StatistiquesService(context);

            LoadStats();
        }

        private void LoadStats()
        {
            int userId = LoginPage.CurrentUser.Id;

            // Réservations du conducteur via StatistiquesService
            var reservations = _statsService.ReservationsDuConducteur(userId);

            txtTotalRes.Text = reservations.Count.ToString();
            txtConfirme.Text = reservations.Count(r => r.Statut == "Confirmée").ToString();
            txtAttente.Text = reservations.Count(r => r.Statut == "En attente").ToString();
            txtRefuse.Text = reservations.Count(r => r.Statut == "Annulée").ToString();

            // Revenus via StatistiquesService
            txtCA.Text = _statsService.ChiffreAffairesConducteur(userId).ToString("0.00") + " TND";
            txtMois.Text = _statsService.RevenueMoisConducteur(userId).ToString("0.00") + " TND";

            // Trajets via StatistiquesService 
            txtTrajets.Text = _statsService.NombreTrajetsConducteur(userId).ToString();
        }
    }
}