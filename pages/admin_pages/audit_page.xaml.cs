using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using learning_pract.Models;

namespace learning_pract.pages.admin_pages
{
    public partial class audit_page : Page
    {
        public audit_page()
        {
            InitializeComponent();
            // List<String> output = new List<string>();
            // foreach (var auditory in Auditory.GetAll())
            // {
            //     output.Add(auditory.num.ToString());
            // }

            audit_listView.ItemsSource = Auditory.GetAll();
        }

        private void Audit_listView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            audit_Number_txtBox.Text = ((Auditory) audit_listView.SelectedItem).num.ToString();
            capacity_txtBox.Text = ((Auditory) audit_listView.SelectedItem).capacity.ToString();
        }

        private void Add_btn_OnClick(object sender, RoutedEventArgs e)
        {
            if (audit_Number_txtBox.Text != String.Empty && capacity_txtBox.Text != String.Empty)
            {
                Auditory newAuditory = new Auditory(audit_Number_txtBox.Text, capacity_txtBox.Text);
                newAuditory.Save();
            }
            else
            {
                MessageBox.Show("Введите все данные");
            }
        }

        private void Change_btn_OnClick(object sender, RoutedEventArgs e)
        {
            if (audit_listView.SelectedIndex >= 0 && audit_Number_txtBox.Text != String.Empty &&
                capacity_txtBox.Text != String.Empty)
            {
                Auditory editAudit = (Auditory) audit_listView.SelectedItem ;
                Int32.TryParse(capacity_txtBox.Text, out editAudit.capacity);
                int vrem;
                Int32.TryParse(audit_Number_txtBox.Text, out vrem);
                editAudit.num = vrem;
                editAudit.Save();
            }
            else
            {
                MessageBox.Show("Введите все данные и выберите аудиторю");
            }
        }

        private void Delete_btn_OnClick(object sender, RoutedEventArgs e)
        {
            if (audit_listView.SelectedIndex >= 0)
            {
                Auditory inputAudit = (Auditory) audit_listView.SelectedItem;
                inputAudit.delete();
            }
            else
            {
                MessageBox.Show("Выберите аудиторю");
            }
        }
    }
}