using System;
using System.Windows;
using System.Windows.Controls;
using learning_pract.Models;

namespace learning_pract.pages.admin_pages
{
    public partial class otdel_page : Page
    {
        public otdel_page()
        {
            InitializeComponent();
            otdel_lstV.ItemsSource = Department.getAll();
            otdel_lstV.SelectedIndex = 0;
        }

        private void Add_btn_OnClick(object sender, RoutedEventArgs e)
        {
            if (otdel_name_txtBox.Text != String.Empty)
            {
                Department inputDep = new Department(otdel_name_txtBox.Text);
                inputDep.Save();
                UpdateUI();
            }
            else
            {
                MessageBox.Show("Впишите название отдела");
            }
        }

        private void Change_btn_OnClick(object sender, RoutedEventArgs e)
        {
            if (otdel_lstV.SelectedIndex >= 0 && otdel_name_txtBox.Text != String.Empty)
            {
                Department inputDep = (Department) otdel_lstV.SelectedItem;
                inputDep.name = otdel_name_txtBox.Text;
                inputDep.Save();
                UpdateUI();
            }
            else
            {
                MessageBox.Show("Выберите выберите отделение и/или впишите название отдела");
            }
        }

        private void Delete_btn_OnClick(object sender, RoutedEventArgs e)
        {
            if (otdel_lstV.SelectedIndex >= 0)
            {
                Department inputDep = (Department) otdel_lstV.SelectedItem;
                inputDep.delete();
                UpdateUI();
            }
            else
            {
                MessageBox.Show("Выберите отдел");
            }
        }

        private void UpdateUI()
        {
            Department department = (Department) otdel_lstV.SelectedItem;
            int buf = otdel_lstV.SelectedIndex;
            otdel_name_txtBox.Text = department.name;
            otdel_lstV.ItemsSource = Department.getAll();
            otdel_lstV.SelectedIndex = buf;
        }

        private void Otdel_lstV_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (otdel_lstV.SelectedIndex >= 0)
            {
                Department department = (Department) otdel_lstV.SelectedItem;
                otdel_name_txtBox.Text = department.name;
            }
        }
    }
}