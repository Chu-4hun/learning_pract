using System;
using System.Windows;
using System.Windows.Controls;
using learning_pract.Models;

namespace learning_pract
{
    public partial class Auth_page : Page
    {
        private String _login;
        private String _password;
        public Auth_page()
        {
            InitializeComponent();
        }

        private void AuthBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text != String.Empty && PasswordBox.Password != String.Empty)
            {
                _login= LoginBox.Text;
                _password = Crypt.Encrypt(PasswordBox.Password);
                User inputUser = User.getByLoginPassword(_login, _password);
                if (inputUser.exists())
                {
                    switch (inputUser.Position.name)
                    {
                        case "Администратор":
                        {
                            admin_panel adminPanel = new admin_panel();
                            adminPanel.Show();
                            Window.GetWindow(this)?.Close();
                            break;
                        }
                        case "Сотрудник Учебной Части":
                        {
                            user_panel userPanel = new user_panel();
                            userPanel.Show();
                            Window.GetWindow(this)?.Close();
                            break;
                        }
                    }
                    
                }
            }
            else
            {
                MessageBox.Show("Введите пароль и логин");
            }
        }
    }
}