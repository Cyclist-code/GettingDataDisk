using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GettingDataDisk
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DragDropApp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseMethod(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MainWin_Loaded(object sender, RoutedEventArgs e)
        {
            string[] drives = Environment.GetLogicalDrives();
            foreach(var drive in drives)
            {
                SelectionDisk.Items.Add(drive);
            }
        }

        private void SelectionDisk_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayContentDisk();
        }

        private void DisplayContentDisk()
        {
            ContentDisk.Text = "";
            try
            {
                var drive = SelectionDisk.SelectedItem.ToString();
                string[] dirs = Directory.GetDirectories(drive);
                foreach(string s in dirs)
                {
                    ContentDisk.Text += s.Replace(drive, "") + Environment.NewLine;
                }

                string[] files = Directory.GetFiles(drive);
                foreach(string s in files)
                {
                    ContentDisk.Text += s.Replace(drive, "") + Environment.NewLine;
                }
            }
            catch
            {
                MessageBox.Show("Диск не найден!", "Нет диска", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
