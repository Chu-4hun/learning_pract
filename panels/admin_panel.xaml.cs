using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using learning_pract.pages.admin_pages;

namespace learning_pract
{
    public partial class admin_panel : Window
    {
        public admin_panel()
        {
            InitializeComponent();
            
        }

        private void AuditoryButton_OnClick(object sender, RoutedEventArgs e)
        {
            Admin_frame.Content = new audit_page();
        }

        private void TeachersButton_OnClick(object sender, RoutedEventArgs e)
        {
            Admin_frame.Content = new teacher_page();
        }

        private void OtdelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Admin_frame.Content = new otdel_page();
        }

        private void GroupsButton_OnClick(object sender, RoutedEventArgs e)
        {
            Admin_frame.Content = new groups_page();
        }

        private void SubjectButton_OnClick(object sender, RoutedEventArgs e)
        {
            Admin_frame.Content = new subjects_page();
        }

        private void ExitButton_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}