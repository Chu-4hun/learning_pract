using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using learning_pract.Models;

namespace learning_pract.pages
{
    public partial class Register_page : Page
    {
        public Register_page()
        {
            InitializeComponent();
            position_comboBox.ItemsSource = Position.getAll();
            position_comboBox.DisplayMemberPath = "name";
            position_comboBox.SelectedIndex = 0;
        }

        private void Register_btn_OnClick(object sender, RoutedEventArgs e)
        {
            if (fam_txtBox.Text != String.Empty && name_txtBox.Text != String.Empty &&
                otch_txtBox.Text != String.Empty && login_txtBox.Text != String.Empty &&
                Passsword_txtBox.Password != String.Empty)
            {
                User user = new User(fam_txtBox.Text, name_txtBox.Text, otch_txtBox.Text, login_txtBox.Text,
                    Crypt.Encrypt(Passsword_txtBox.Password), (Position) position_comboBox.SelectedItem);
                user.save();
            }
            else
            {
                MessageBox.Show("Введите все данные");
            }
        }
    }
}