﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReadAndWriteStreams
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void File_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem? menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                if (menuItem.Name == "ReadMenuItem")
                {
                    //neues Fenster geht auf und Datei wird gesucht
                    DateiPfadWindow DateiPfadWindow = new DateiPfadWindow ();
                    DateiPfadWindow.Show();
                }
                else if(menuItem.Name == "WriteMenuItem")
                {
                    MessageBox.Show("Write ausgeführt.");
                }
            }
        }
    }
}