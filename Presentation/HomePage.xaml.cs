using System.Windows;
using System.Windows.Controls;

namespace Presentation
{
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Navigate(new LoginPage());
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Navigate(new RegisterPage());
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Navigate(Page page)
        {
            var frame = ((MainWindow)Application.Current.MainWindow).MainFrame;

            frame.Opacity = 0;
            frame.Navigate(page);

            var anim = new System.Windows.Media.Animation.DoubleAnimation(0, 1,
                TimeSpan.FromMilliseconds(300));

            frame.BeginAnimation(Frame.OpacityProperty, anim);
        }
    }
}