using System.Windows.Controls;
using learning_pract.Models;

namespace learning_pract.pages.admin_pages
{
    public partial class audit_page : Page
    {
        public audit_page()
        {
            InitializeComponent();
            audit_listView.ItemsSource = Auditory.GetAll();
        }
    }
}