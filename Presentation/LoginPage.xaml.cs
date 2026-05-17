using System.Windows;
using System.Windows.Controls;
using DataAccess.Data;
using DataAccess.Models;
using Services;

namespace Presentation
{
    public partial class LoginPage : Page
    {
        public static Utilisateur CurrentUser;

        private AuthentificationService authService;

        public LoginPage()
        {
            InitializeComponent();
            var context = new CovoiturageContext();
            authService = new AuthentificationService(context);
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text.Trim().ToLower();
            string password = txtPassword.Password.Trim();

            if (email == "" || password == "")
            {
                MessageBox.Show("Remplissez les champs");
                return;
            }

            // Utilisation de AuthentificationService.Connexion()
            var user = authService.Connexion(email, password);

            if (user != null)
            {
                CurrentUser = user;
                MessageBox.Show("Bienvenue " + user.Prenom + " ✔️");

                // Utilisation de AuthentificationService.EstConducteur() / EstPassager()
                if (authService.EstConducteur(user))
                    Navigate(new ConducteurMainPage());
                else
                    Navigate(new ReservationPage());
            }
            else
            {
                MessageBox.Show("Email ou mot de passe incorrect ❌");
            }
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