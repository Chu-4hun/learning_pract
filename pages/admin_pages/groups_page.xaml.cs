using System;
using System.Windows;
using System.Windows.Controls;
using learning_pract.Models;

namespace learning_pract.pages.admin_pages
{
    public partial class groups_page : Page
    {
        public groups_page()
        {
            InitializeComponent();
            groups_lstV.ItemsSource = Group.getAll();
            groups_lstV.SelectedIndex = 0;
        }

        private void Add_btn_OnClick(object sender, RoutedEventArgs e)
        {
            if (group_name_txtBox.Text != String.Empty)
            {
                Group inputGroup = new Group(group_name_txtBox.Text);
                inputGroup.Save();
                UpdateUI();
            }
            else
            {
                MessageBox.Show("Впишите название группы");
            }
        }

        private void Change_btn_OnClick(object sender, RoutedEventArgs e)
        {
            if (groups_lstV.SelectedIndex >= 0 && group_name_txtBox.Text != String.Empty)
            {
                Group inputGroup = (Group) groups_lstV.SelectedItem;
                inputGroup.name = group_name_txtBox.Text;
                inputGroup.Save();
                UpdateUI();
            }
            else
            {
                MessageBox.Show("Выберите выберите отделение и/или впишите название группы");
            }
        }

        private void Delete_btn_OnClick(object sender, RoutedEventArgs e)
        {
            if (groups_lstV.SelectedIndex >= 0)
            {
                Group inputGroup = (Group) groups_lstV.SelectedItem;
                inputGroup.delete();
                UpdateUI();
            }
            else
            {
                MessageBox.Show("Выберите группу");
            }
        }

        private void UpdateUI()
        {
            Group group = (Group) groups_lstV.SelectedItem;
            int buf = groups_lstV.SelectedIndex;
            group_name_txtBox.Text = group.name;
            groups_lstV.ItemsSource = Group.getAll();
            groups_lstV.SelectedIndex = buf;
        }

        private void Groups_lstV_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (groups_lstV.SelectedIndex >= 0)
            {
                Group group = (Group) groups_lstV.SelectedItem;
                group_name_txtBox.Text = group.name;
            }
        }
    }
}