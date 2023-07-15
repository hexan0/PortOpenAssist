using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace PortOpenAssist
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenDH_Click(object sender, RoutedEventArgs e)
        {
            //ProcessStartInfoのオブジェクトを生成
            ProcessStartInfo psInfo = new ProcessStartInfo();
            psInfo.FileName = "UPnPCJ.exe"; //コマンド
            psInfo.Arguments = @"/open 2300-2309 TCP 2300-2309 " + LanIP.Text; //引数
            psInfo.CreateNoWindow = true; // コンソール・ウィンドウを開かない
            psInfo.UseShellExecute = false; // シェル機能を使用しない
            //psInfo.RedirectStandardOutput = true; // 標準出力をリダイレクト
            Process.Start(psInfo); // コマンドの実行開始
            psInfo.Arguments = @"/open 2310,47624 TCP 2310,47624 " + LanIP.Text; //引数
            Process.Start(psInfo); // コマンドの実行開始
            psInfo.Arguments = @"/open 2300-2309 UDP 2300-2309 " + LanIP.Text; //引数
            Process.Start(psInfo); // コマンドの実行開始
            psInfo.Arguments = @"/open 2310,47624 UDP 2310,47624 " + LanIP.Text; //引数
            Process.Start(psInfo); // コマンドの実行開始
        }

        private void OpenMCBE_Click(object sender, RoutedEventArgs e)
        {
            //ProcessStartInfoのオブジェクトを生成
            ProcessStartInfo psInfo = new ProcessStartInfo();
            psInfo.FileName = "UPnPCJ.exe"; //コマンド
            psInfo.Arguments = @"/open 19132 UDP 19132 " + LanIP.Text; //引数
            psInfo.CreateNoWindow = true; // コンソール・ウィンドウを開かない
            psInfo.UseShellExecute = false; // シェル機能を使用しない
            //psInfo.RedirectStandardOutput = true; // 標準出力をリダイレクト
            Process.Start(psInfo); // コマンドの実行開始
        }

        private void OpenMCJE_Click(object sender, RoutedEventArgs e)
        {
            //ProcessStartInfoのオブジェクトを生成
            ProcessStartInfo psInfo = new ProcessStartInfo();
            psInfo.FileName = "UPnPCJ.exe"; //コマンド
            psInfo.Arguments = @"/open 25565 TCP 25565 " + LanIP.Text; //引数
            psInfo.CreateNoWindow = true; // コンソール・ウィンドウを開かない
            psInfo.UseShellExecute = false; // シェル機能を使用しない
            //psInfo.RedirectStandardOutput = true; // 標準出力をリダイレクト
            Process.Start(psInfo); // コマンドの実行開始
        }

        private void CloseDH_Click(object sender, RoutedEventArgs e)
        {
            //ProcessStartInfoのオブジェクトを生成
            ProcessStartInfo psInfo = new ProcessStartInfo();
            psInfo.FileName = "UPnPCJ.exe"; //コマンド
            psInfo.Arguments = @"/close 2300-2309 TCP 2300-2309 " + LanIP.Text; //引数
            psInfo.CreateNoWindow = true; // コンソール・ウィンドウを開かない
            psInfo.UseShellExecute = false; // シェル機能を使用しない
            //psInfo.RedirectStandardOutput = true; // 標準出力をリダイレクト
            Process.Start(psInfo); // コマンドの実行開始
            psInfo.Arguments = @"/close 2310,47624 TCP 2310,47624 " + LanIP.Text; //引数
            Process.Start(psInfo); // コマンドの実行開始
            psInfo.Arguments = @"/close 2300-2309 UDP 2300-2309 " + LanIP.Text; //引数
            Process.Start(psInfo); // コマンドの実行開始
            psInfo.Arguments = @"/close 2310,47624 UDP 2310,47624 " + LanIP.Text; //引数
            Process.Start(psInfo); // コマンドの実行開始
        }

        private void CloseMCBE_Click(object sender, RoutedEventArgs e)
        {
            //ProcessStartInfoのオブジェクトを生成
            ProcessStartInfo psInfo = new ProcessStartInfo();
            psInfo.FileName = "UPnPCJ.exe"; //コマンド
            psInfo.Arguments = @"/close 19132 UDP 19132 " + LanIP.Text; //引数
            psInfo.CreateNoWindow = true; // コンソール・ウィンドウを開かない
            psInfo.UseShellExecute = false; // シェル機能を使用しない
            //psInfo.RedirectStandardOutput = true; // 標準出力をリダイレクト
            Process.Start(psInfo); // コマンドの実行開始
        }

        private void CloseMCJE_Click(object sender, RoutedEventArgs e)
        {
            //ProcessStartInfoのオブジェクトを生成
            ProcessStartInfo psInfo = new ProcessStartInfo();
            psInfo.FileName = "UPnPCJ.exe"; //コマンド
            psInfo.Arguments = @"/close 25565 TCP 25565 " + LanIP.Text; //引数
            psInfo.CreateNoWindow = true; // コンソール・ウィンドウを開かない
            psInfo.UseShellExecute = false; // シェル機能を使用しない
            //psInfo.RedirectStandardOutput = true; // 標準出力をリダイレクト
            Process.Start(psInfo); // コマンドの実行開始
        }

        //英数字限定用
        private void EnOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // 
            e.Handled = !new Regex("[-A-Za-z_./:0-9]").IsMatch(e.Text);
        }
        //限定用
        private void Only_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            // 貼り付けを許可しない
            if (e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }
    }
}
