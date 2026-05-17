using System.Windows;
using System.Windows.Controls;
using DataAccess.Data;
using DataAccess.Models;
using Services;

namespace Presentation
{
    public partial class RegisterPage : Page
    {
        private AuthentificationService authService;

        public RegisterPage()
        {
            InitializeComponent();
            var context = new CovoiturageContext();
            authService = new AuthentificationService(context);
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string prenom = txtPrenom.Text.Trim();
            string nom = txtNom.Text.Trim();
            string email = txtEmail.Text.Trim().ToLower();
            string password = txtPassword.Password.Trim();
            string tel = txtTel.Text.Trim();

            if (prenom == "" || nom == "" || email == "" || password == "" || tel == "" || cmbRole.SelectedItem == null)
            {
                MessageBox.Show("Remplissez tous les champs ❌");
                return;
            }

            var user = new Utilisateur
            {
                Prenom = prenom,
                Nom = nom,
                Email = email,
                MotDePasse = password,
                Telephone = tel,
                Role = (cmbRole.SelectedItem as ComboBoxItem).Content.ToString()
            };

            // Utilisation de AuthentificationService.Inscription()
            bool succes = authService.Inscription(user);

            if (!succes)
            {
                MessageBox.Show("Email déjà utilisé ❌");
                return;
            }

            MessageBox.Show("Compte créé ✔️");
            Navigate(new LoginPage());
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Navigate(new HomePage());
        }

        private void Navigate(Page page)
        {
            var frame = ((MainWindow)Application.Current.MainWindow).MainFrame;
            frame.Navigate(page);
        }
    }
}