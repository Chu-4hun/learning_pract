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

            db = new DB("194.169.163.29", "ffff", "Chu", "4hun");

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