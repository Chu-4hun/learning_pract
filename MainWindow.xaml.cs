using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using learning_pract.Models;
using learning_pract.pages;

namespace learning_pract
{
    /// <summary>
    ///     Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        { 
            InitializeComponent();
            MainFrame.Content = new Start_page();
        }
    }
}