using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using learning_pract.Models;

namespace learning_pract.pages.user_pages
{
    public partial class audit_fund_page : Page
    {
        List<Audit> audit = new List<Audit>();

        public audit_fund_page()
        {
            InitializeComponent();
            groups_lstV.ItemsSource = Group.getAll();
            UpdateTable(Schedule.getAll());
        }

        private void UpdateTable(List<Schedule> list)
        {
            foreach (Schedule schedule in list)
            {
                for (int i = 0; i < audit.Count; i++)
                {
                    audit[i].auditory = schedule.Auditory.num.ToString();
                    audit[i].group = schedule.Group.name;
                    audit[i].subject = schedule.Subject.Theme;
                    audit[i].Num = schedule.Num;
                    audit[i].Day = schedule.Day;
                }
            }

            AuditGrid.ItemsSource = audit;
        }


        private void Groups_lstV_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Group group = (Group) groups_lstV.SelectedItem;
            UpdateTable(Schedule.GetByGroupId(group.ID));
        }
        private void AllGroupsButton_OnClick(object sender, RoutedEventArgs e)
        {
            UpdateTable(Schedule.getAll());
        }

        private void EditButton_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("In development");
        }
    }
}