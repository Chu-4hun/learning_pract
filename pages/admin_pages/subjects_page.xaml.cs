using System;
using System.Windows;
using System.Windows.Controls;
using learning_pract.Models;

namespace learning_pract.pages.admin_pages
{
    public partial class subjects_page : Page
    {
        public subjects_page()
        {
            InitializeComponent();
            subject_lstV.ItemsSource = Lecture.getAll();
            subject_lstV.SelectedIndex = 0;
        }

        private void Add_btn_OnClick(object sender, RoutedEventArgs e)
        {
            if (subject_name_txtBox.Text != String.Empty)
            {
                Lecture inputGroup = new Lecture(subject_name_txtBox.Text);
                inputGroup.Save();
                UpdateUI();
            }
            else
            {
                MessageBox.Show("Впишите название предмета");
            }
        }

        private void Change_btn_OnClick(object sender, RoutedEventArgs e)
        {
            if (subject_lstV.SelectedIndex >= 0 && subject_name_txtBox.Text != String.Empty)
            {
                Lecture inputGroup = (Lecture) subject_lstV.SelectedItem;
                inputGroup.Theme = subject_name_txtBox.Text;
                inputGroup.Save();
                UpdateUI();
            }
            else
            {
                MessageBox.Show("Выберите выберите отделение и/или впишите название предмета");
            }
        }

        private void Delete_btn_OnClick(object sender, RoutedEventArgs e)
        {
            if (subject_lstV.SelectedIndex >= 0)
            {
                Lecture inputGroup = (Lecture) subject_lstV.SelectedItem;
                inputGroup.delete();
                UpdateUI();
            }
            else
            {
                MessageBox.Show("Выберите предмет");
            }
        }

        private void UpdateUI()
        {
            if (subject_lstV.SelectedIndex >=0)
            {
                Lecture lecture = (Lecture) subject_lstV.SelectedItem;
                int buf = subject_lstV.SelectedIndex;
                subject_name_txtBox.Text = lecture.Theme;
                subject_lstV.ItemsSource = Lecture.getAll();
                subject_lstV.SelectedIndex = buf;
            }
        }

        private void Subject_lstV_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (subject_lstV.SelectedIndex >= 0)
            {
                Lecture lecture = (Lecture) subject_lstV.SelectedItem;
                subject_name_txtBox.Text = lecture.Theme;
            }
        }
    }
}