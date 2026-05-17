using System.Windows;
using System.Windows.Controls;
using DataAccess.Data;
using DataAccess.Models;
using Services;

namespace Presentation
{
    public partial class ConducteurPage : Page
    {
        private TrajetService trajetService;
        private VoitureService voitureService;

        public ConducteurPage()
        {
            InitializeComponent();
            var context = new CovoiturageContext();
            trajetService = new TrajetService(context);
            voitureService = new VoitureService(context);

            LoadVoitures();
            LoadTrajets();
        }

        private void LoadVoitures()
        {
            int userId = LoginPage.CurrentUser.Id;

            // Utilisation de VoitureService.GetVoituresByUtilisateurId()
            cmbVoiture.ItemsSource = voitureService.GetVoituresByUtilisateurId(userId);
        }

        private void LoadTrajets()
        {
            int userId = LoginPage.CurrentUser.Id;

            // Utilisation de TrajetService.GetTrajetsByConducteurId()
            trajetGrid.ItemsSource = trajetService.GetTrajetsByConducteurId(userId);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtDuree.Text, out int duree))
            {
                MessageBox.Show("Durée invalide ❌");
                return;
            }

            if (cmbVoiture.SelectedItem is not Voiture v)
            {
                MessageBox.Show("Choisissez une voiture ❌");
                return;
            }

            DateTime date = dpDate.SelectedDate ?? DateTime.Now.Date;

            if (!TimeSpan.TryParse(txtHeure.Text, out TimeSpan heure))
            {
                MessageBox.Show("Heure invalide (format HH:mm) ❌");
                return;
            }

            var t = new Trajet
            {
                AdresseDepart = txtDepart.Text,
                AdresseArrivee = txtArrivee.Text,
                DateDepart = date.Add(heure),
                DureeMinutes = duree,
                ConducteurId = LoginPage.CurrentUser.Id,
                VoitureId = v.Id
            };

            // Utilisation de TrajetService.AjouterTrajet()
            trajetService.AjouterTrajet(t);

            MessageBox.Show("Trajet ajouté ✔️");
            ClearForm();
            LoadTrajets();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (trajetGrid.SelectedItem is not Trajet t)
            {
                MessageBox.Show("Sélectionnez un trajet ❌");
                return;
            }

            if (!int.TryParse(txtDuree.Text, out int duree))
            {
                MessageBox.Show("Durée invalide ❌");
                return;
            }

            if (cmbVoiture.SelectedItem is not Voiture v)
            {
                MessageBox.Show("Choisissez une voiture ❌");
                return;
            }

            DateTime date = dpDate.SelectedDate ?? t.DateDepart.Date;

            if (!TimeSpan.TryParse(txtHeure.Text, out TimeSpan heure))
            {
                MessageBox.Show("Heure invalide (format HH:mm) ❌");
                return;
            }

            t.AdresseDepart = txtDepart.Text;
            t.AdresseArrivee = txtArrivee.Text;
            t.DateDepart = date.Add(heure);
            t.DureeMinutes = duree;
            t.VoitureId = v.Id;

            // Utilisation de TrajetService.MettreAjout()
            trajetService.MettreAjout(t);

            MessageBox.Show("Trajet modifié ✔️");
            LoadTrajets();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (trajetGrid.SelectedItem is Trajet t)
            {
                // Utilisation de TrajetService.SupprimerTrajet()
                trajetService.SupprimerTrajet(t.Id);

                MessageBox.Show("Trajet supprimé ✔");
                LoadTrajets();
            }
        }

        private void trajetGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (trajetGrid.SelectedItem is Trajet t)
            {
                txtDepart.Text = t.AdresseDepart;
                txtArrivee.Text = t.AdresseArrivee;
                txtDuree.Text = t.DureeMinutes.ToString();
                dpDate.SelectedDate = t.DateDepart;
                txtHeure.Text = t.DateDepart.ToString("HH:mm");

                // Utilisation de VoitureService.GetVoitureById()
                cmbVoiture.SelectedItem = voitureService.GetVoitureById(t.VoitureId);
            }
        }

        private void ClearForm()
        {
            txtDepart.Text = "";
            txtArrivee.Text = "";
            txtDuree.Text = "";
            dpDate.SelectedDate = null;
            txtHeure.Text = "";
            cmbVoiture.SelectedItem = null;
        }
    }
}