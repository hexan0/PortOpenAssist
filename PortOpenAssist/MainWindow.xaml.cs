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
        public string settingFileName = "settings.xml";
        public MainWindow()
        {
            InitializeComponent();

            if(!System.IO.File.Exists(settingFileName)) CreateSetting();
            LoadSetting();
        }

        public Setting setting;
        private void SaveLanIP_Click(object sender, RoutedEventArgs e)
        {
            setting.lanip = LanIP.Text;

            // XmlSerializerを使ってファイルに保存（オブジェクトの内容を書き込む）
            XmlSerializer serializer = new XmlSerializer(typeof(Setting));

            // カレントディレクトリに"settings.xml"というファイルで書き出す
            FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\\" + settingFileName, FileMode.Create);

            // オブジェクトをシリアル化してXMLファイルに書き込む
            serializer.Serialize(fs, setting);
            fs.Close();
        }

        /*
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
        */

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

        
        private OpenSet set_DH;
        private OpenSet set_BE;
        private OpenSet set_JE;
        private void CreateSetting()
        {
            setting = new Setting();
            set_DH = new OpenSet();
            set_BE = new OpenSet();
            set_JE = new OpenSet();

            setting.lanip = "";

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
            FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\\" + settingFileName, FileMode.Create);

            // オブジェクトをシリアル化してXMLファイルに書き込む
            serializer.Serialize(fs, setting);
            fs.Close();
        }

        //public ObservableCollection<Port>[] portSetting;
        public List<ObservableCollection<Port>> portSetting = new List<ObservableCollection<Port>>();
        private void LoadSetting()
        {
            // XMLをSettingオブジェクトに読み込む
            setting = new Setting();
            FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\\" + settingFileName, FileMode.Open);

            // XMLファイルを読み込み、逆シリアル化（復元）する
            XmlSerializer serializer = new XmlSerializer(typeof(Setting));
            setting = (Setting)serializer.Deserialize(fs);
            fs.Close();

            /* stackpanel使ったやり方
            foreach(OpenSet item in setting.opensets)
            {
                StackPanel sp = new StackPanel{ Orientation = Orientation.Horizontal, Width = 471 };
                sp.Children.Add(new Button { Content = " Open ", HorizontalAlignment = HorizontalAlignment.Right, HorizontalContentAlignment = HorizontalAlignment.Right });
                sp.Children.Add(new Button { Content = " Close ", HorizontalAlignment = HorizontalAlignment.Right, HorizontalContentAlignment = HorizontalAlignment.Right });
                sp.Children.Add(new Label { Content = item.name });
                String portText = "";
                foreach(Port portOb in item.ports)
                {
                    portText = portText + portOb.port + portOb.protocol + "\n";
                }
                sp.Children.Add(new Label { Content = portText });
                stackPanel.Children.Add(sp);
            }*/

            LanIP.Text = setting.lanip;

            //Grid使ったやり方
            int i = 0;
            foreach (OpenSet item in setting.opensets)
            {
                //name
                Label name = new Label { Content = item.name };
                Grid.SetRow(name, i) ;
                Grid.SetColumn(name, 0);
                grid.Children.Add(name);

                //ポート番号
                String portText = "";
                foreach (Port portOb in item.ports)
                {
                    portText = portText + portOb.port + " " + portOb.protocol + "\n";
                }
                Label port = new Label { Content = portText };
                Grid.SetRow(port, i);
                Grid.SetColumn(port, 1);
                grid.Children.Add(port);
                //portSetting[i] = item.ports;
                portSetting.Add(item.ports);

                //Openボタン
                Button buttonOpen = new Button();
                buttonOpen.Content = " Open ";
                buttonOpen.Name = "o" + i.ToString();
                buttonOpen.Click += ButtonOpen_Click;
                Grid.SetRow(buttonOpen, i);
                Grid.SetColumn(buttonOpen, 2);
                grid.Children.Add(buttonOpen);

                //Closeボタン
                Button buttonClose = new Button { Content = " Close " };
                buttonClose.Name = "c" + i.ToString();
                buttonClose.Click += ButtonClose_Click;
                Grid.SetRow(buttonClose, i);
                Grid.SetColumn(buttonClose, 3);
                grid.Children.Add(buttonClose);

                //設定ボタン
                Button buttonSetting = new Button { Content = "S" };
                buttonSetting.Name = "s" + i.ToString();
                buttonSetting.Click += ButtonSetting_Click;
                Grid.SetRow(buttonSetting, i);
                Grid.SetColumn(buttonSetting, 4);
                grid.Children.Add(buttonSetting);

                //grid.RowDefinitions.Add(new RowDefinition()); //行幅設定の追加
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); //行幅設定の追加
                // TODO : あまりに設定が多いと表示がはみでてしまい見えなくなる問題
                i++;
            }
            //追加ボタン
            Button buttonNew = new Button();
            buttonNew.Content = " + ";
            buttonNew.Name = "plus";
            buttonNew.Click += ButtonNew_Click;
            Grid.SetRow(buttonNew, i);
            Grid.SetColumn(buttonNew, 0);
            grid.Children.Add(buttonNew);
        }

        

        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
            //MessageBox.Show("open");

            //ProcessStartInfoのオブジェクトを生成
            ProcessStartInfo psInfo = new ProcessStartInfo();
            psInfo.FileName = "UPnPCJ.exe"; //コマンド
            psInfo.CreateNoWindow = true; // コンソール・ウィンドウを開かない
            psInfo.UseShellExecute = false; // シェル機能を使用しない
            //psInfo.RedirectStandardOutput = true; // 標準出力をリダイレクト

            Button b = (Button)sender;
            
            foreach (Port portOb in portSetting[int.Parse(b.Name.Substring(1))])
            {
                psInfo.Arguments = @"/open " + portOb.port + " " + portOb.protocol + " " + portOb.port + " " + LanIP.Text; //引数
                Process.Start(psInfo); // コマンドの実行開始
            }

        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
            //ProcessStartInfoのオブジェクトを生成
            ProcessStartInfo psInfo = new ProcessStartInfo();
            psInfo.FileName = "UPnPCJ.exe"; //コマンド
            psInfo.CreateNoWindow = true; // コンソール・ウィンドウを開かない
            psInfo.UseShellExecute = false; // シェル機能を使用しない
            //psInfo.RedirectStandardOutput = true; // 標準出力をリダイレクト

            Button b = (Button)sender;

            foreach (Port portOb in portSetting[int.Parse(b.Name.Substring(1))])
            {
                psInfo.Arguments = @"/close " + portOb.port + " " + portOb.protocol + " " + portOb.port + " " + LanIP.Text; //引数
                Process.Start(psInfo); // コマンドの実行開始
            }
        }

        private void ButtonSetting_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            var win = new SettingWindow(setting, int.Parse(b.Name.Substring(1)));
            win.ShowDialog();
            //LoadSetting();
            this.Close();
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
            var win = new SettingWindow(setting, -1);
            win.ShowDialog();
            //LoadSetting();
            this.Close();
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
        public string lanip;
        public ObservableCollection<OpenSet> opensets;
    }
}
