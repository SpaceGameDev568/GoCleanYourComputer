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
        int processStartDelay = 200;
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void SysinfoButton_Click(object sender, RoutedEventArgs e)
        {
            SysinfoButton.Content = "Running...";
            
            Thread.Sleep(processStartDelay);
            
            // System cannot find dir yet
            var SysinfoProcess = System.Diagnostics.Process.Start("C:/Windows/SysWOW64/Msinfo32.exe", " /nfo sysinfo.nfo");
            //SysinfoProcess.WaitForExit();
            
            // Doesn't work rn
            SysinfoButton.Content = "Done!";
        }
        
        private void RMTempFiles_Click(object sender, RoutedEventArgs e)
        {
            RMTempFilesButton.Content = "Running...";
            
            Thread.Sleep(processStartDelay);
            
            var RMTempFilesProcess = System.Diagnostics.Process.Start("CMD.exe", "/K cd /d %LOCALAPPDATA%&echo y|rmdir Temp /S");
            //RMTempFilesProcess.WaitForExit();
            
            // Doesn't work rn
            RMTempFilesButton.Content = "Done!";
        }
        
        private void CleanDrive_Click(object sender, RoutedEventArgs e)
        {
            CleanDriveButton.Content = "Running...";
            
            Thread.Sleep(processStartDelay);
            
            var CleanDriveProcess = System.Diagnostics.Process.Start("cleanmgr.exe", "/sagerun:1");
            //CleanDriveProcess.WaitForExit();
            
            // Doesn't work rn
            CleanDriveButton.Content = "Done!";
        }
        
        private void Defrag_Click(object sender, RoutedEventArgs e)
        {
            DefragButton.Content = "Running...";
            
            Thread.Sleep(processStartDelay);
            
            // Info about cmd arguments:
            // https://learn.microsoft.com/en-us/windows-server/administration/windows-commands/defrag
            
            var DefragProcess = System.Diagnostics.Process.Start("defrag.exe", "/C /D /G /H");
            //DefragProcess.WaitForExit();
            
            // Doesn't work rn
            DefragButton.Content = "Done!";
        }
    }
}