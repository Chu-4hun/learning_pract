using System.Windows;
using InventoryOfEquipment.Lib;

namespace learning_pract
{
    /// <summary>
    ///     Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static DB db;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            db = new DB("194.87.110.238", "chu_learn_db", "chu", "CYV-6tm-5dE-Z9C");

            var window = new MainWindow()
            {
                Height = 800,
                Width = 800,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            // window.DataContext =  new MainViewModel();
            // window.ShowDialog();
        }
    }
}