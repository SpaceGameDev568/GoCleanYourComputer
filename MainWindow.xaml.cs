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
using Path = System.IO.Path;

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
                Arguments = "/nfo sysinfo.nfo",
                FileName = "C:/Windows/SysWOW64/Msinfo32.exe",
                // Hide console window
                //WindowStyle = ProcessWindowStyle.Hidden,
                //CreateNoWindow = true
            };

            InfoBox.Text = "Retrieving System Info...";
            SysinfoButton.Content = "Running...";

            // Run command line with args
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
                Arguments = "/K cd /d %LOCALAPPDATA%&echo y|rmdir Temp /S&exit",
                FileName = "CMD.exe",
                // Hide console window
                //WindowStyle = ProcessWindowStyle.Hidden,
                //CreateNoWindow = true
            };

            InfoBox.Text = "Removing temporary files...";
            RmTempFilesButton.Content = "Running...";

            // Run command line with args
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
                    RmTempFilesButton.Content = "Done!";
                    InfoBox.Text = "Successfully removed temporary files.";
                    RmTempFilesButton.IsHitTestVisible = false;
                });
            }
        }
        
        private void CleanDrive_Click(object sender, RoutedEventArgs e)
        {
            // Prepare the process to run
            var processArguments = new ProcessStartInfo
            {
                Arguments = "/sagerun:1&exit",
                FileName = "cleanmgr.exe",
                // Hide console window
                //WindowStyle = ProcessWindowStyle.Hidden,
                //CreateNoWindow = true
            };

            InfoBox.Text = "Cleaning up Windows files on your drives";
            CleanDriveButton.Content = "Running...";

            // Run command line with args
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
                
                Arguments = "/C /D /G /H",
                FileName = "defrag.exe",
                // Hide console window
                //WindowStyle = ProcessWindowStyle.Hidden,
                //CreateNoWindow = true
            };

            InfoBox.Text = "Defragmenting your hard drives...";
            DefragButton.Content = "Running...";

            // Run command line with args
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
            // Prepare the process to run
            var killOneDriveArguments = new ProcessStartInfo
            {
                Arguments = "/IM OneDrive.exe /F",
                FileName = "taskkill",
                // Hide console window
                //WindowStyle = ProcessWindowStyle.Hidden,
                //CreateNoWindow = true
            };
            
            // Prepare the process to run
            var oneDriveSyncArguments = new ProcessStartInfo
            {
                Arguments = "uninstall Microsoft.OneDriveSync_8wekyb3d8bbwe",
                FileName = "winget.exe",
                // Hide console window
                //WindowStyle = ProcessWindowStyle.Hidden,
                //CreateNoWindow = true
            };
            
            var oneDriveClientArguments = new ProcessStartInfo
            {
                Arguments = "uninstall Microsoft.OneDrive",
                FileName = "winget.exe",
                // Hide console window
                //WindowStyle = ProcessWindowStyle.Hidden,
                //CreateNoWindow = true
            };

            InfoBox.Text = "Removing Microsoft OneDrive...";
            RmOneDriveButton.Content = "Running...";
            
            RegistryKey? userDirKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders", true);

            // Run command line with args
            new Thread(() => 
            {
                Thread.CurrentThread.IsBackground = true; 
                using (Process? proc = Process.Start(killOneDriveArguments))
                {
                    proc?.WaitForExit();
                }
                
                Thread.CurrentThread.IsBackground = true; 
                using (Process? proc = Process.Start(oneDriveSyncArguments))
                {
                    proc?.WaitForExit();
                }
                
                Thread.CurrentThread.IsBackground = true; 
                using (Process? proc = Process.Start(oneDriveClientArguments))
                {
                    proc?.WaitForExit();
                }
                
                // FUTURE FEATURE: Copy files to new directory
                
                // Note: This only copies files from the OneDrive directory, but does not delete the old ones to avoid potential data loss. I might add this as a warning toggle later, but for now it may cause more unintentional bloat.

                CompletedTask();
                
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
                    
                    RmOneDriveButton.Content = "Done!";
                    InfoBox.Text = "Successfully removed Microsoft OneDrive.";
                    RmOneDriveButton.IsHitTestVisible = false;
                });
            }
        }
        
        private void RMWidgets_Click(object sender, RoutedEventArgs e)
        {
            // Prepare the process to run
            var processArguments = new ProcessStartInfo
            {
                Arguments = "uninstall MicrosoftWindows.Client.WebExperience_cw5n1h2txyewy",
                FileName = "winget",
                // Hide console window
                //WindowStyle = ProcessWindowStyle.Hidden,
                //CreateNoWindow = true
            };

            InfoBox.Text = "Removing Widgets app...";
            RmWidgetsButton.Content = "Running...";

            // Run command line with args
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
                    RmWidgetsButton.Content = "Done!";
                    InfoBox.Text = "Successfully removed Widgets app.";
                    RmWidgetsButton.IsHitTestVisible = false;
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
                Arguments = "-set {globalsettings} highestmode on",
                FileName = "bcdedit.exe",
                // Hide console window
                //WindowStyle = ProcessWindowStyle.Hidden,
                //CreateNoWindow = true
            };

            InfoBox.Text = "Applying bootloader settings...";
            FixBootloaderResButton.Content = "Running...";

            // Run command line with args
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
                Arguments = "config DiagTrack start= disabled",
                FileName = "sc",
                // Hide console window
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

            // Run command line with args
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
                Arguments = "/online /cleanup-image /restorehealth",
                FileName = "DISM",
                // Hide console window
                //WindowStyle = ProcessWindowStyle.Hidden,
                //CreateNoWindow = true
            };
            
            // Prepare the process to run
            var sfcArguments = new ProcessStartInfo
            {
                Arguments = "/scannow",
                FileName = "SFC",
                // Hide console window
                //WindowStyle = ProcessWindowStyle.Hidden,
                //CreateNoWindow = true
            };

            InfoBox.Text = "Checking system image... (This could take a while)";
            RestoreSystemImageButton.Content = "Running...";

            // Run command line with args
            new Thread(() => 
            {
                Thread.CurrentThread.IsBackground = true; 
                using (Process? proc = Process.Start(dismArguments))
                {
                    proc?.WaitForExit();
                }
            }).Start();
            
            // Run command line with args
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