using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Input;

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
            DisplayInfoDisk();
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
                MessageBox.Show("Диск не найден!", caption: "Нет диска", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
            }
        }

        private void DisplayInfoDisk()
        {
            string driveLetter = SelectionDisk.SelectedItem.ToString();
            DriveInfo driveInfo = new DriveInfo(driveLetter);

            DriveType.Text = driveInfo.DriveType.ToString();
            DriveName.Text = driveInfo.Name;
            RootDirectory.Text = driveInfo.RootDirectory.Name;

            try
            {
                TotalSize.Text = driveInfo.TotalSize.ToString();
                AvailableFreeSpace.Text = driveInfo.AvailableFreeSpace.ToString();
                DriveFormat.Text = driveInfo.DriveFormat;
                TagDisk.Text = driveInfo.VolumeLabel;
            }
            catch
            {
                TotalSize.Text = "";
                AvailableFreeSpace.Text = "";
                DriveFormat.Text = "";
                TagDisk.Text = "";
            }
        }
    }
}
