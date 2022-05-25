using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using learning_pract.Models;

namespace learning_pract.pages.user_pages
{
    public partial class add_schedule_page : Page
    {
        public add_schedule_page()
        {
            InitializeComponent();
            List<int> nums = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9};
            Group_cmBox.ItemsSource = Group.getAll();
            Group_cmBox.DisplayMemberPath = "name";
            Subject_cmBox.ItemsSource = Lecture.getAll();
            Subject_cmBox.DisplayMemberPath = "Theme";
            User_cmBox.ItemsSource = User.getAll();
            User_cmBox.DisplayMemberPath = "FIO";
            Audit_cmBox.ItemsSource = Auditory.GetAll();
            Audit_cmBox.DisplayMemberPath = "num";
            Num_cmBox.ItemsSource = nums;
        }

        private void Add_btn_OnClick(object sender, RoutedEventArgs e)
        {
            if (Group_cmBox.SelectedItem != null && Subject_cmBox.SelectedItem != null &&
                User_cmBox.SelectedItem != null && Audit_cmBox.SelectedItem != null && Num_cmBox.SelectedItem != null &&
                DatePicker.SelectedDate != null)
            {
                Schedule schedule = new Schedule(DatePicker.SelectedDate.Value.Date.ToShortDateString(),
                    (Lecture) Subject_cmBox.SelectedItem, (Auditory) Audit_cmBox.SelectedItem,
                    (int) Num_cmBox.SelectedItem, (Group) Group_cmBox.SelectedItem);
                schedule.save();
                // schedule.DeleteDublicates(); // not done Npgsql.PostgresException (0x80004005): 42601: syntax error at or near "("
                MessageBox.Show("Готово!");
            }
            else
            {
                MessageBox.Show("Вы ввели не все данные - проверьте еще раз!", "Внимание!");
            }
        }

        private void Change_btn_OnClick(object sender, RoutedEventArgs e)
        {
            // throw new System.NotImplementedException();
        }

        private void Delete_btn_OnClick(object sender, RoutedEventArgs e)
        {
            // throw new System.NotImplementedException();
        }
    }
}