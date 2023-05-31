using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GoCleanYourComputer
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

        private void SysinfoButton_Click(object sender, RoutedEventArgs e)
        {

            // Prepare the process to run
            var processArguments = new ProcessStartInfo
            {
                // Enter in the command line arguments, everything you would enter after the executable name itself
                Arguments = "/nfo sysinfo.nfo",
                // Enter the executable to run, including the complete path
                FileName = "C:/Windows/SysWOW64/Msinfo32.exe",
                // Do you want to show a console window?
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true
            };

            InfoBox.Text = "Retrieving System Info...";
            SysinfoButton.Content = "Running...";

            // Run the external process & wait for it to finish
            new Thread(() => 
            {
                Thread.CurrentThread.IsBackground = true; 
                using (Process? proc = Process.Start(processArguments))
                {
                    proc?.WaitForExit();

                    CompletedTask();
                }
            }).Start();

            void CompletedTask()
            {
                this.Dispatcher.Invoke(() =>
                {
                    SysinfoButton.Content = "Done!";
                    InfoBox.Text = "System info exported to sysinfo.nfo in the app directory.";
                });
            }

            // WIP: COPY SYSINFO TO DOCUMENTS DIR IN THE FUTURE
        }
        
        private void RMTempFiles_Click(object sender, RoutedEventArgs e)
        {
            // Prepare the process to run
            var processArguments = new ProcessStartInfo
            {
                // Enter in the command line arguments, everything you would enter after the executable name itself
                Arguments = "/K cd /d %LOCALAPPDATA%&echo y|rmdir Temp /S&exit",
                // Enter the executable to run, including the complete path
                FileName = "CMD.exe",
                // Do you want to show a console window?
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true
            };

            InfoBox.Text = "Removing temporary files...";
            RMTempFilesButton.Content = "Running...";

            // Run the external process & wait for it to finish
            new Thread(() => 
            {
                Thread.CurrentThread.IsBackground = true; 
                using (Process? proc = Process.Start(processArguments))
                {
                    proc?.WaitForExit();

                    CompletedTask();
                }
            }).Start();

            void CompletedTask()
            {
                this.Dispatcher.Invoke(() =>
                {
                    RMTempFilesButton.Content = "Done!";
                    InfoBox.Text = "Successfully removed temporary files.";
                });
            }
        }
        
        private void CleanDrive_Click(object sender, RoutedEventArgs e)
        {
            // Prepare the process to run
            var processArguments = new ProcessStartInfo
            {
                // Enter in the command line arguments, everything you would enter after the executable name itself
                Arguments = "/sagerun:1&exit",
                // Enter the executable to run, including the complete path
                FileName = "cleanmgr.exe",
                // Do you want to show a console window?
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true
            };

            InfoBox.Text = "Cleaning up Windows files on your drives";
            CleanDriveButton.Content = "Running...";

            // Run the external process & wait for it to finish
            new Thread(() => 
            {
                Thread.CurrentThread.IsBackground = true; 
                using (Process? proc = Process.Start(processArguments))
                {
                    proc?.WaitForExit();

                    CompletedTask();
                }
            }).Start();

            void CompletedTask()
            {
                this.Dispatcher.Invoke(() =>
                {
                    CleanDriveButton.Content = "Done!";
                    InfoBox.Text = "Successfully cleaned Windows files.";
                });
            }
        }
        
        private void Defrag_Click(object sender, RoutedEventArgs e)
        {
            DefragButton.Content = "Running...";
            
            
            
            // Info about cmd arguments:
            // https://learn.microsoft.com/en-us/windows-server/administration/windows-commands/defrag
            
            var DefragProcess = System.Diagnostics.Process.Start("defrag.exe", "/C /D /G /H");
            //DefragProcess.WaitForExit();
            
            // Doesn't work rn
            DefragButton.Content = "Done!";
        }
        
        private void RMEdge_Click(object sender, RoutedEventArgs e)
        {
            DefragButton.Content = "Running...";
        }
        
        private void RMOneDrive_Click(object sender, RoutedEventArgs e)
        {
            RMOneDriveButton.Content = "Running...";
            
            
            
            // Info about cmd arguments:
            // https://learn.microsoft.com/en-us/windows-server/administration/windows-commands/defrag
            
            var DefragProcess = System.Diagnostics.Process.Start("defrag.exe", "/C /D /G /H");
            //DefragProcess.WaitForExit();
            
            // Doesn't work rn
            RMOneDriveButton.Content = "Done!";
        }
        
        private void RMWidgets_Click(object sender, RoutedEventArgs e)
        {
            RMWidgetsButton.Content = "Running...";
            
            
            
            // Info about cmd arguments:
            // https://learn.microsoft.com/en-us/windows-server/administration/windows-commands/defrag
            
            var DefragProcess = System.Diagnostics.Process.Start("winget", "uninstall MicrosoftWindows.Client.WebExperience_cw5n1h2txyewy");
            //DefragProcess.WaitForExit();
            
            // Doesn't work rn
            RMWidgetsButton.Content = "Done!";
        }
        
        private void RestoreSystemImage_Click(object sender, RoutedEventArgs e)
        {
            RestoreSystemImageButton.Content = "Running...";
            
            
            
            // Info about cmd arguments:
            // https://learn.microsoft.com/en-us/windows-server/administration/windows-commands/defrag
            
            System.Diagnostics.Process.Start("DISM", "/online /cleanup-image /restorehealth");
            System.Diagnostics.Process.Start("sfc", "/scannow");
            //DefragProcess.WaitForExit();
            
            // Doesn't work rn
            RestoreSystemImageButton.Content = "Done!";
        }
        
        // Needs Admin
        private void FixBootloaderRes_Click(object sender, RoutedEventArgs e)
        {
            FixBootloaderResButton.Content = "Running...";
            
            

            System.Diagnostics.Process.Start("bcdedit.exe", "-set {globalsettings} highestmode on");

            // Doesn't work rn
            FixBootloaderResButton.Content = "Done!";
        }
    }
}