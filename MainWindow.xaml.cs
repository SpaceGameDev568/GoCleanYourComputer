using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
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
using Microsoft.Win32;

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
                //WindowStyle = ProcessWindowStyle.Hidden,
                //CreateNoWindow = true
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
                    SysinfoButton.IsHitTestVisible = false;
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
                //WindowStyle = ProcessWindowStyle.Hidden,
                //CreateNoWindow = true
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
                    RMTempFilesButton.IsHitTestVisible = false;
                });
            }
        }
        
        private void CleanDrive_Click(object sender, RoutedEventArgs e)
        {
            // Prepare the process to run
            var processArguments = new ProcessStartInfo
            {
                // NOTE: I SHOULD MAKE THIS ONLY SCAN WINDOWS DRIVE IN FUTURE
                
                // Enter in the command line arguments, everything you would enter after the executable name itself
                Arguments = "/sagerun:1&exit",
                // Enter the executable to run, including the complete path
                FileName = "cleanmgr.exe",
                // Do you want to show a console window?
                //WindowStyle = ProcessWindowStyle.Hidden,
                //CreateNoWindow = true
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
                    CleanDriveButton.IsHitTestVisible = false;
                });
            }
        }
        
        private void Defrag_Click(object sender, RoutedEventArgs e)
        {
            // Prepare the process to run
            var processArguments = new ProcessStartInfo
            {
                // Info about cmd arguments:
                // https://learn.microsoft.com/en-us/windows-server/administration/windows-commands/defrag
                
                // Enter in the command line arguments, everything you would enter after the executable name itself
                Arguments = "/C /D /G /H",
                // Enter the executable to run, including the complete path
                FileName = "defrag.exe",
                // Do you want to show a console window?
                //WindowStyle = ProcessWindowStyle.Hidden,
                //CreateNoWindow = true
            };

            InfoBox.Text = "Defragmenting your hard drives...";
            DefragButton.Content = "Running...";

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
                    DefragButton.Content = "Done!";
                    InfoBox.Text = "Successfully defragmented your hard drives.";
                    DefragButton.IsHitTestVisible = false;
                });
            }
        }

        private void RMOneDrive_Click(object sender, RoutedEventArgs e)
        {
            // Fails to uninstall for some reason
            
            // Prepare the process to run
            var killOneDriveArguments = new ProcessStartInfo
            {
                
                // Enter in the command line arguments, everything you would enter after the executable name itself
                Arguments = "/IM OneDrive.exe /F",

                // Enter the executable to run, including the complete path
                FileName = "taskkill",
                // Do you want to show a console window?
                //WindowStyle = ProcessWindowStyle.Hidden,
                //CreateNoWindow = true
            };
            
            // Prepare the process to run
            var processArguments = new ProcessStartInfo
            {
                
                // Enter in the command line arguments, everything you would enter after the executable name itself
                Arguments = "uninstall Microsoft.OneDriveSync_8wekyb3d8bbwe",

                // Enter the executable to run, including the complete path
                FileName = "winget.exe",
                // Do you want to show a console window?
                //WindowStyle = ProcessWindowStyle.Hidden,
                //CreateNoWindow = true
            };

            InfoBox.Text = "Removing Microsoft OneDrive...";
            RMOneDriveButton.Content = "Running...";
            
            RegistryKey? userDirKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders", true);

            // Run the external process & wait for it to finish
            new Thread(() => 
            {
                Thread.CurrentThread.IsBackground = true; 
                using (Process? proc = Process.Start(killOneDriveArguments))
                {
                    proc?.WaitForExit();
                }
            }).Start();
            
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
                    if (userDirKey != null)
                    {
                        userDirKey.SetValue("Desktop", "%USERPROFILE%/Desktop", RegistryValueKind.String);
                        userDirKey.SetValue("My Music", "%USERPROFILE%/Music", RegistryValueKind.String);
                        userDirKey.SetValue("My Pictures", "%USERPROFILE%/Pictures", RegistryValueKind.String);
                        userDirKey.SetValue("My Video", "%USERPROFILE%/Videos", RegistryValueKind.String);
                        userDirKey.SetValue("Personal", "%USERPROFILE%/Documents", RegistryValueKind.String);
                        userDirKey.Close();
                    }
                    
                    RMOneDriveButton.Content = "Done!";
                    InfoBox.Text = "Successfully removed Microsoft OneDrive.";
                    RMOneDriveButton.IsHitTestVisible = false;
                });
            }
        }
        
        private void RMWidgets_Click(object sender, RoutedEventArgs e)
        {
            // Prepare the process to run
            var processArguments = new ProcessStartInfo
            {
                // Enter in the command line arguments, everything you would enter after the executable name itself
                Arguments = "uninstall MicrosoftWindows.Client.WebExperience_cw5n1h2txyewy",
                // Enter the executable to run, including the complete path
                FileName = "winget",
                // Do you want to show a console window?
                //WindowStyle = ProcessWindowStyle.Hidden,
                //CreateNoWindow = true
            };

            InfoBox.Text = "Removing Widgets app...";
            RMWidgetsButton.Content = "Running...";

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
                    RMWidgetsButton.Content = "Done!";
                    InfoBox.Text = "Successfully removed Widgets app.";
                    RMWidgetsButton.IsHitTestVisible = false;
                });
            }
        }

        private void NoBingSearch_Click(object sender, RoutedEventArgs e)
        {
            RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Search", true);

            InfoBox.Text = "Disabling Bing search results in Windows search...";
            NoBingSearchButton.Content = "Running...";
            
            if (key != null)
            {
                key?.CreateSubKey("BingSearchEnabled");
                key?.SetValue("BingSearchEnabled", "0", RegistryValueKind.DWord);
                key?.Close();
            }

            
            NoBingSearchButton.Content = "Done!";
            InfoBox.Text = "Successfully disabled Bing search.";
            NoBingSearchButton.IsHitTestVisible = false;
        }

        private void FixBootloaderRes_Click(object sender, RoutedEventArgs e)
        {
            // Prepare the process to run
            var processArguments = new ProcessStartInfo
            {
                // Enter in the command line arguments, everything you would enter after the executable name itself
                Arguments = "-set {globalsettings} highestmode on",
                // Enter the executable to run, including the complete path
                FileName = "bcdedit.exe",
                // Do you want to show a console window?
                //WindowStyle = ProcessWindowStyle.Hidden,
                //CreateNoWindow = true
            };

            InfoBox.Text = "Applying bootloader settings...";
            FixBootloaderResButton.Content = "Running...";

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
                    FixBootloaderResButton.Content = "Done!";
                    InfoBox.Text = "Successfully fixed bootloader visual distortion.";
                    FixBootloaderResButton.IsHitTestVisible = false;
                });
            }
        }
        
        private void DisableTelemetry_Click(object sender, RoutedEventArgs e)
        {
            RegistryKey? key = Registry.LocalMachine.OpenSubKey(@"\SOFTWARE\Policies\Microsoft\Windows\DataCollection", true);

            // Prepare the process to run
            var diagTrackArguments = new ProcessStartInfo
            {
                // Enter in the command line arguments, everything you would enter after the executable name itself
                Arguments = "config DiagTrack start= disabled",
                // Enter the executable to run, including the complete path
                FileName = "sc",
                // Do you want to show a console window?
                //WindowStyle = ProcessWindowStyle.Hidden,
                //CreateNoWindow = true
            };

            InfoBox.Text = "Disabling invasive telemetry...";
            DisableTelemetryButton.Content = "Running...";
            
            if (key != null)
            {
                key?.SetValue("AllowTelemetry", "0", RegistryValueKind.DWord);
                key?.Close();
            }

            // Run the external process & wait for it to finish
            new Thread(() => 
            {
                Thread.CurrentThread.IsBackground = true; 
                using (Process? proc = Process.Start(diagTrackArguments))
                {
                    proc?.WaitForExit();

                    CompletedTask();
                }
            }).Start();
            
            void CompletedTask()
            {
                this.Dispatcher.Invoke(() =>
                {
                    DisableTelemetryButton.Content = "Done!";
                    InfoBox.Text = "Successfully disabled Windows telemetry.";
                    DisableTelemetryButton.IsHitTestVisible = false;
                });
            }
        }
        

        private void RestoreSystemImage_Click(object sender, RoutedEventArgs e)
        {
            // Prepare the process to run
            var dismArguments = new ProcessStartInfo
            {
                // Enter in the command line arguments, everything you would enter after the executable name itself
                Arguments = "/online /cleanup-image /restorehealth",
                // Enter the executable to run, including the complete path
                FileName = "DISM",
                // Do you want to show a console window?
                //WindowStyle = ProcessWindowStyle.Hidden,
                //CreateNoWindow = true
            };
            
            // Prepare the process to run
            var sfcArguments = new ProcessStartInfo
            {
                // Enter in the command line arguments, everything you would enter after the executable name itself
                Arguments = "/scannow",
                // Enter the executable to run, including the complete path
                FileName = "SFC",
                // Do you want to show a console window?
                //WindowStyle = ProcessWindowStyle.Hidden,
                //CreateNoWindow = true
            };

            InfoBox.Text = "Checking system image (This could take a while)...";
            RestoreSystemImageButton.Content = "Running...";

            // Run the external process & wait for it to finish
            new Thread(() => 
            {
                Thread.CurrentThread.IsBackground = true; 
                using (Process? proc = Process.Start(dismArguments))
                {
                    proc?.WaitForExit();
                }
            }).Start();
            
            // Run the external process & wait for it to finish
            new Thread(() => 
            {
                Thread.CurrentThread.IsBackground = true; 
                using (Process? proc = Process.Start(sfcArguments))
                {
                    proc?.WaitForExit();

                    CompletedTask();
                }
            }).Start();

            void CompletedTask()
            {
                this.Dispatcher.Invoke(() =>
                {
                    RestoreSystemImageButton.Content = "Done!";
                    InfoBox.Text = "Successfully repaired system image.";
                    RestoreSystemImageButton.IsHitTestVisible = false;
                });
            }
        }
    }
}