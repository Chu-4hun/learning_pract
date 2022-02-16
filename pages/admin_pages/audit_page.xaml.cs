using System;
using System.Collections.Generic;
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
            List<String> output = new List<string>();
            foreach (var auditory in Auditory.GetAll())
            {
                output.Add(auditory.num.ToString());
            }

            audit_listView.ItemsSource = output;
        }

        private void Audit_listView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            audit_Number_txtBox.Text = Auditory.GetById(audit_listView.SelectedIndex+1).num.ToString();
            capacity_txtBox.Text = Auditory.GetById(audit_listView.SelectedIndex+1).capacity.ToString();
        }
    }
}