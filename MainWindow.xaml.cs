using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        private void CleanButton_Click(object sender, RoutedEventArgs e)
        {
            CleanButton.Content = "Clicked!";
            
            // System cannot find dir yet
            string strCmdText;
            strCmdText= "/nfo %USERPROFILE%/Documents/sysinfo.nfo";
            System.Diagnostics.Process.Start("C:/Windows/SysWOW64/Msinfo32.exe",strCmdText);
        }
        
        private void RMTempFiles_Click(object sender, RoutedEventArgs e)
        {
            CleanButton.Content = "Clicked!";
            
            System.Diagnostics.Process.Start("CMD.exe", "/K cd /d %LOCALAPPDATA%&echo y|rmdir Temp /S");
            
        }
    }
}