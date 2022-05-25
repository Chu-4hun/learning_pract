using System.Windows;
using learning_pract.pages.user_pages;

namespace learning_pract
{
    public partial class user_panel : Window
    {
        public user_panel()
        {
            InitializeComponent();
        }


        private void ExitButton_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ScheduleButton_OnClick(object sender, RoutedEventArgs e)
        {
            User_frame.Content = new schedule_page();
        }

        private void AuditoryButton_OnClick(object sender, RoutedEventArgs e)
        {
            User_frame.Content = new audit_fund_page();
        }
    }
}