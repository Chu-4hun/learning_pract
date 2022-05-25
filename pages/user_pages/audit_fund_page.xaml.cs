using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using learning_pract.Models;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;


namespace learning_pract.pages.user_pages
{
    public partial class audit_fund_page : Excel.Page
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
                audit.Add(new Audit(schedule));
            }

            
            AuditGrid.ItemsSource = audit;
            AuditGrid.Items.Refresh();
        }

        private void Clear()
        {
            audit = new List<Audit>();
            AuditGrid.ItemsSource = null;
            AuditGrid.Columns.Clear();
            AuditGrid.Items.Clear();
            AuditGrid.Items.Refresh();
        }


        private void Groups_lstV_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Group group = (Group) groups_lstV.SelectedItem;
            Clear();
            UpdateTable(Schedule.GetByGroupId(group.ID));
        }

        private void AllGroupsButton_OnClick(object sender, RoutedEventArgs e)
        {
            Clear();
            UpdateTable(Schedule.getAll());
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            add_schedule_page addSchedulePage = new add_schedule_page();
            if (NavigationService != null) NavigationService.Navigate(addSchedulePage);
        }

        private void ExportButton_OnClick(object sender, RoutedEventArgs e)
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet) workbook.Sheets[1];

            for (int j = 0; j < AuditGrid.Columns.Count; j++)
            {
                Range myRange = (Range) sheet1.Cells[1, j + 1];
                myRange.Value2 = AuditGrid.Columns[j].Header;
            }

            for (int i = 0; i < AuditGrid.Columns.Count; i++)
            {
                for (int j = 0; j < AuditGrid.Items.Count; j++)
                {
                    TextBlock b = AuditGrid.Columns[i].GetCellContent(AuditGrid.Items[j]) as TextBlock;
                    Microsoft.Office.Interop.Excel.Range myRange =
                        (Microsoft.Office.Interop.Excel.Range) sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;
                }
            }
        }

        public HeaderFooter LeftHeader { get; }
        public HeaderFooter CenterHeader { get; }
        public HeaderFooter RightHeader { get; }
        public HeaderFooter LeftFooter { get; }
        public HeaderFooter CenterFooter { get; }
        public HeaderFooter RightFooter { get; }
    }
}