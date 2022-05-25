using System;
using System.Windows;
using System.Windows.Controls;
using learning_pract.Models;

namespace learning_pract.pages.admin_pages
{
    public partial class teacher_page : Page
    {
        public teacher_page()
        {
            InitializeComponent();
            role_cmBox.ItemsSource = Position.getAll();
            role_cmBox.DisplayMemberPath = "name";
            role_cmBox.SelectedIndex = 0;
            techers_lstView.ItemsSource = User.getAll();
        }

        private void Register_btn_OnClick(object sender, RoutedEventArgs e)
        {
            if (sername_txtBox.Text != String.Empty && name_txtBox.Text != String.Empty &&
                otch_txtBox.Text != String.Empty && Login_txtBox.Text != String.Empty &&
                password_box.Password != String.Empty)
            {
                User user = new User(surname: sername_txtBox.Text, firstName: name_txtBox.Text,
                    patronymic: otch_txtBox.Text, login: Login_txtBox.Text, password:
                    Crypt.Encrypt(password_box.Password), position: (Position) role_cmBox.SelectedItem);

                user.save();
                UpdateUI();
            }
            else
            {
                MessageBox.Show("Введите все данные");
            }
        }

        private void Delete_btn_OnClick(object sender, RoutedEventArgs e)
        {
            if (techers_lstView.SelectedIndex >= 0)
            {
                User inputAudit = (User) techers_lstView.SelectedItem;
                inputAudit.delete();
                UpdateUI();
            }
            else
            {
                MessageBox.Show("Выберите аудиторю");
            }
        }

        private void Techers_lstView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (techers_lstView.SelectedIndex >= 0)
            {
                User user = (User) techers_lstView.SelectedItem;
                role_cmBox.SelectedIndex = user.Position.ID - 1;
                sername_txtBox.Text = user.surname;
                name_txtBox.Text = user.firstName;
                otch_txtBox.Text = user.patronymic;
                Login_txtBox.Text = user.login;
                password_box.Password = Crypt.Decrypt(user.password);
            }
        }

        private void UpdateUI()
        {
            if (techers_lstView.SelectedIndex >= 0)
            {
                User user = (User) techers_lstView.SelectedItem;
                int buf = techers_lstView.SelectedIndex;
                role_cmBox.SelectedIndex = user.Position.ID - 1;
                sername_txtBox.Text = user.surname;
                name_txtBox.Text = user.firstName;
                otch_txtBox.Text = user.patronymic;
                Login_txtBox.Text = user.login;
                password_box.Password = Crypt.Decrypt(user.password);
                techers_lstView.ItemsSource = User.getAll();
                techers_lstView.SelectedIndex = buf;
            }
        }

        private void Change_btn_OnClick(object sender, RoutedEventArgs e)
        {
            if (sername_txtBox.Text != String.Empty && name_txtBox.Text != String.Empty &&
                otch_txtBox.Text != String.Empty && Login_txtBox.Text != String.Empty &&
                password_box.Password != String.Empty)
            {
                User user = (User) techers_lstView.SelectedItem;
                // user._idPosition = role_cmBox.SelectedIndex + 1;
                user.surname = sername_txtBox.Text;
                user.firstName = name_txtBox.Text;
                user.patronymic = otch_txtBox.Text;
                user.login = Login_txtBox.Text;
                user.password = Crypt.Encrypt(password_box.Password);
                user.save();
                UpdateUI();
            }
            else
            {
                MessageBox.Show("Введите все данные");
            }
        }
    }
}