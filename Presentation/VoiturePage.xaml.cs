using System.Windows;
using System.Windows.Controls;
using DataAccess.Data;
using DataAccess.Models;
using Services;

namespace Presentation
{
    public partial class VoiturePage : Page
    {
        private VoitureService voitureService;

        public VoiturePage()
        {
            InitializeComponent();
            var context = new CovoiturageContext();
            voitureService = new VoitureService(context);
            LoadData();
        }

        private void LoadData()
        {
            int userId = LoginPage.CurrentUser.Id;

            // Utilisation de VoitureService.GetVoituresByUtilisateurId()
            voitureGrid.ItemsSource = voitureService.GetVoituresByUtilisateurId(userId);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateForm(out int places)) return;

            var v = new Voiture
            {
                Marque = txtMarque.Text,
                Modele = txtModele.Text,
                Couleur = txtCouleur.Text,
                Immatriculation = txtImmat.Text,
                NbPlacesTotal = places,
                UtilisateurId = LoginPage.CurrentUser.Id
            };

            // Utilisation de VoitureService.AjouterVoiture()
            voitureService.AjouterVoiture(v);

            MessageBox.Show("Voiture ajoutée ✔️");
            ClearForm();
            LoadData();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (voitureGrid.SelectedItem is not Voiture v)
            {
                MessageBox.Show("Sélectionnez une voiture ❌");
                return;
            }

            if (!ValidateForm(out int places)) return;

            v.Marque = txtMarque.Text;
            v.Modele = txtModele.Text;
            v.Couleur = txtCouleur.Text;
            v.Immatriculation = txtImmat.Text;
            v.NbPlacesTotal = places;

            // Utilisation de VoitureService.MettreAjout()
            voitureService.MettreAjout(v);

            MessageBox.Show("Voiture modifiée ✔️");
            LoadData();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (voitureGrid.SelectedItem is Voiture v)
            {
                // Utilisation de VoitureService.SupprimerVoiture()
                voitureService.SupprimerVoiture(v.Id);

                MessageBox.Show("Voiture supprimée ✔️");
                LoadData();
            }
            else
            {
                MessageBox.Show("Sélectionnez une voiture ❌");
            }
        }

        private void voitureGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (voitureGrid.SelectedItem is Voiture v)
            {
                txtMarque.Text = v.Marque;
                txtModele.Text = v.Modele;
                txtCouleur.Text = v.Couleur;
                txtPlaces.Text = v.NbPlacesTotal.ToString();
                txtImmat.Text = v.Immatriculation;
            }
        }

        private bool ValidateForm(out int places)
        {
            places = 0;

            if (string.IsNullOrWhiteSpace(txtMarque.Text) ||
                string.IsNullOrWhiteSpace(txtModele.Text) ||
                string.IsNullOrWhiteSpace(txtCouleur.Text) ||
                string.IsNullOrWhiteSpace(txtImmat.Text) ||
                !int.TryParse(txtPlaces.Text, out places))
            {
                MessageBox.Show("Remplissez correctement les champs ❌");
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            txtMarque.Text = "";
            txtModele.Text = "";
            txtCouleur.Text = "";
            txtPlaces.Text = "";
            txtImmat.Text = "";
        }
    }
}