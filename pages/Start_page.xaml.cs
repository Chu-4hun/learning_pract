using System.Windows;
using System.Windows.Controls;

namespace learning_pract.pages
{
    public partial class Start_page : Page
    {
        public Start_page()
        {
            InitializeComponent();
        }

        private void AuthButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Auth_page());
        }

        private void RegisterButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Register_page());
        }
    }
}