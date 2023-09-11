using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
using System.Xml.Serialization;

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

            LoadSetting();
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

        private Setting setting;
        private OpenSet set_DH;
        private OpenSet set_BE;
        private OpenSet set_JE;
        private void CreateSetting()
        {
            setting = new Setting();
            set_DH = new OpenSet();
            set_BE = new OpenSet();
            set_JE = new OpenSet();

            set_DH.name = "Hoi2 DH";
            set_DH.ports = new ObservableCollection<Port>();
            set_DH.ports.Add(new Port { port = "2300-2309", protocol = "TCP" });
            set_DH.ports.Add(new Port { port = "2310,47624", protocol = "TCP" });
            set_DH.ports.Add(new Port { port = "2300-2309", protocol = "UDP" });
            set_DH.ports.Add(new Port { port = "2310,47624", protocol = "UDP" });

            set_BE.name = "Minecraft BE";
            set_BE.ports = new ObservableCollection<Port>();
            set_BE.ports.Add(new Port { port = "19132", protocol = "UDP" });

            set_JE.name = "Minecraft JE";
            set_JE.ports = new ObservableCollection<Port>();
            set_JE.ports.Add(new Port { port = "25565", protocol = "TCP" });

            setting.opensets = new ObservableCollection<OpenSet>();
            setting.opensets.Add(set_DH);
            setting.opensets.Add(set_BE);
            setting.opensets.Add(set_JE);

            // XmlSerializerを使ってファイルに保存（オブジェクトの内容を書き込む）
            XmlSerializer serializer = new XmlSerializer(typeof(Setting));

            // カレントディレクトリに"settings.xml"というファイルで書き出す
            FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\\" + "settings.xml", FileMode.Create);

            // オブジェクトをシリアル化してXMLファイルに書き込む
            serializer.Serialize(fs, setting);
            fs.Close();
        }

        private void LoadSetting()
        {
            // XMLをTwitSettingsオブジェクトに読み込む
            Setting setting = new Setting();
            FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\\" + "settings.xml", FileMode.Open);

            // XMLファイルを読み込み、逆シリアル化（復元）する
            XmlSerializer serializer = new XmlSerializer(typeof(Setting));
            setting = (Setting)serializer.Deserialize(fs);
            fs.Close();



        }
    }

    public class Port
    {
        public string port;
        public string protocol;
    }

    public class OpenSet
    {
        public string name;
        public ObservableCollection<Port> ports;
    }

    public class Setting
    {
        public ObservableCollection<OpenSet> opensets;
    }
}
