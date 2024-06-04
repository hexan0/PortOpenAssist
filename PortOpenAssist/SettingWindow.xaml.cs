using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace PortOpenAssist
{
    /// <summary>
    /// SettingWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingWindow : Window
    {
        public int i = 0; //セットの使っている行
        public int k = 0; //ポートの使っている行+1（=追加ボタンのある行）
        public Setting setting;
        public Button newButton;
        public string settingFileName = "settings.xml";

        public SettingWindow(Setting setting_, int i_ = -1)
        {
            InitializeComponent();

            setting = setting_;
            i = i_;

            if (i == -1)
            {
                //新規
                k = 0;
                
                TextBox portBox = new TextBox();
                //portBox.Text = item.port;
                portBox.Name = "n" + k;
                Grid.SetRow(portBox, k);
                Grid.SetColumn(portBox, 1);
                setting_grid.Children.Add(portBox);
                setting_grid.RegisterName(portBox.Name, portBox);

                ComboBox protocolBox = new ComboBox();
                protocolBox.Items.Add("TCP");
                protocolBox.Items.Add("UDP");
                //protocolBox.SelectedItem = item.protocol;
                protocolBox.Name = "p" + k;
                Grid.SetRow(protocolBox, k);
                Grid.SetColumn(protocolBox, 2);
                setting_grid.Children.Add(protocolBox);
                setting_grid.RegisterName(protocolBox.Name, protocolBox);

                k++;

                newButton = new Button();
                newButton.Content = "+";
                newButton.Click += NewButton_Click;
                Grid.SetRow(newButton, k);
                Grid.SetColumn(newButton, 1);
                setting_grid.Children.Add(newButton);

                setting_grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); //行幅設定の追加
            }
            else
            {
                //更新
                name.Text = setting.opensets[i].name;
                k = 0; //初期化
                foreach ( Port item in setting.opensets[i].ports)
                {
                    TextBox portBox = new TextBox();
                    portBox.Text = item.port;
                    portBox.Name = "n" + k;
                    Grid.SetRow(portBox, k);
                    Grid.SetColumn(portBox, 1);
                    setting_grid.Children.Add(portBox);
                    setting_grid.RegisterName(portBox.Name, portBox);

                    ComboBox protocolBox = new ComboBox();
                    protocolBox.Items.Add("TCP");
                    protocolBox.Items.Add("UDP");
                    protocolBox.SelectedItem = item.protocol;
                    protocolBox.Name = "p" + k;
                    Grid.SetRow(protocolBox, k);
                    Grid.SetColumn(protocolBox, 2);
                    setting_grid.Children.Add(protocolBox);
                    setting_grid.RegisterName(protocolBox.Name, protocolBox);

                    /*
                    Button deleteButton = new Button();
                    deleteButton.Content = "x";
                    deleteButton.Click += DeleteButton_Click;
                    Grid.SetRow(deleteButton, k);
                    Grid.SetColumn(deleteButton, 3);
                    setting_grid.Children.Add(deleteButton);
                    */

                    // setting_grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength { Value = 23 } }); //行幅設定の追加
                    // ↑この設定方法はValueが読み取り専用なので無理
                    //setting_grid.RowDefinitions.Add(new RowDefinition()); //行幅設定の追加
                    setting_grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); //行幅設定の追加

                    k++;
                }
                newButton = new Button();
                newButton.Content = "+";
                newButton.Click += NewButton_Click;
                Grid.SetRow(newButton, k);
                Grid.SetColumn(newButton, 1);
                setting_grid.Children.Add(newButton);

                //setting_grid.RowDefinitions.Add(new RowDefinition()); //行幅設定の追加
                setting_grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); //行幅設定の追加
            }
        }

        

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            //ポート番号入力ボックス追加
            TextBox portBox = new TextBox();
            //portBox.Text = item.port;
            portBox.Name = "n" + k;
            Grid.SetRow(portBox, k);
            Grid.SetColumn(portBox, 1);
            setting_grid.Children.Add(portBox);
            setting_grid.RegisterName(portBox.Name, portBox);

            //プロトコル選択追加
            ComboBox protocolBox = new ComboBox();
            protocolBox.Items.Add("TCP");
            protocolBox.Items.Add("UDP");
            //protocolBox.SelectedItem = item.protocol;
            protocolBox.Name = "p" + k;
            Grid.SetRow(protocolBox, k);
            Grid.SetColumn(protocolBox, 2);
            setting_grid.Children.Add(protocolBox);
            setting_grid.RegisterName(protocolBox.Name, protocolBox);

            k++;
            Grid.SetRow(newButton, k); //行追加ボタンを一つ下の行に移動
            setting_grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); //行幅設定の追加
        }

        /*
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
            Control[] controls = Controls.Find("textbox1", true);
            foreach (Control control in controls)
            {
                this.Controls.Remove(control);
                control.Dispose();
            }
        }
        */
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); //このウィンドウを閉じる

            var mainWin = new MainWindow();
            mainWin.Show(); //メインウィンドウを開く
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if(i == -1)
            {
                //新規
                OpenSet newOpenSet = new OpenSet();
                newOpenSet.name = name.Text;
                newOpenSet.ports = new ObservableCollection<Port>();
                Port altPort;
                for (int n = 0; n < k; n++)
                {
                    /*
                    if (FindChild<TextBox>(Application.Current.MainWindow, "n" + n).Text == "") break;
                    altPort.port = FindChild<TextBox>(Application.Current.MainWindow, "n" + n).Text; //ここエラー
                    altPort.protocol = FindChild<TextBox>(Application.Current.MainWindow, "p" + n).Text;
                    setting.opensets[i].ports.Add(altPort);
                    */
                    /*
                    altPort.port = FindChild<TextBox>(Application.Current.MainWindow, "n" + n).Text; //ここエラー
                    altPort.protocol = FindChild<TextBox>(Application.Current.MainWindow, "p" + n).Text;
                    if (altPort.port == "") break;
                    newOpenSet.ports.Add(altPort);
                    */
                    altPort = new Port();

                    TextBox portTextBox = (TextBox)this.setting_grid.FindName("n" + n);
                    if (portTextBox.Text == "") break;
                    altPort.port = portTextBox.Text;
                    ComboBox protocolComboBox = (ComboBox)this.setting_grid.FindName("p" + n);
                    if (protocolComboBox.Text == "") break;
                    altPort.protocol = protocolComboBox.Text;
                    //if (altPort.port == "") break;
                    newOpenSet.ports.Add(altPort);

                }
                setting.opensets.Add(newOpenSet);
            }
            else
            {
                //更新
                setting.opensets[i].name = name.Text;
                setting.opensets[i].ports.Clear();
                Port altPort;
                for (int n = 0; n < k; n++)
                {
                    /*
                    if (FindChild<TextBox>(Application.Current.MainWindow, "n" + n).Text == "") break;
                    altPort.port = FindChild<TextBox>(Application.Current.MainWindow, "n" + n).Text; //ここエラー
                    altPort.protocol = FindChild<TextBox>(Application.Current.MainWindow, "p" + n).Text;
                    setting.opensets[i].ports.Add(altPort);
                    */
                    /*
                    altPort.port = FindChild<TextBox>(Application.Current.MainWindow, "n" + n).Text; //ここエラー
                    altPort.protocol = FindChild<TextBox>(Application.Current.MainWindow, "p" + n).Text;
                    if (altPort.port == "") break;
                    setting.opensets[i].ports.Add(altPort);
                    */
                    altPort = new Port();

                    TextBox portTextBox = (TextBox)this.setting_grid.FindName("n" + n);
                    if (portTextBox.Text == "") break;
                    altPort.port = portTextBox.Text;
                    ComboBox protocolComboBox = (ComboBox)this.setting_grid.FindName("p" + n);
                    if (protocolComboBox.Text == "") break;
                    altPort.protocol = protocolComboBox.Text;
                    //if (altPort.port == "") break;
                    setting.opensets[i].ports.Add(altPort);

                }
            }

            // XmlSerializerを使ってファイルに保存（オブジェクトの内容を書き込む）
            XmlSerializer serializer = new XmlSerializer(typeof(Setting));

            // カレントディレクトリに"settings.xml"というファイルで書き出す
            FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\\" + settingFileName, FileMode.Create);

            // オブジェクトをシリアル化してXMLファイルに書き込む
            serializer.Serialize(fs, setting);
            fs.Close();

            this.Close(); //このウィンドウを閉じる

            var mainWin = new MainWindow();
            mainWin.Show(); //メインウィンドウを開く
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            //setting.opensets[i] = null; //TODO: データは消えるけど、opensetタグが消えないから完全削除にならない
            setting.opensets.RemoveAt(i); //これでいけた

            // XmlSerializerを使ってファイルに保存（オブジェクトの内容を書き込む）
            XmlSerializer serializer = new XmlSerializer(typeof(Setting));

            // カレントディレクトリに"settings.xml"というファイルで書き出す
            FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\\" + settingFileName, FileMode.Create);

            // オブジェクトをシリアル化してXMLファイルに書き込む
            serializer.Serialize(fs, setting);
            fs.Close();

            this.Close(); //このウィンドウを閉じる

            var mainWin = new MainWindow();
            mainWin.Show(); //メインウィンドウを開く
        }

        /*
        /// <summary>
        /// 指定したName属性を持つ子要素を探すメソッド
        /// </summary>
        /// <param name="parent">探したい要素の親要素</param>
        /// <typeparam name="T">探したいオブジェクトの型</typeparam>
        /// <param name="childName">探したい子要素のx:Name または Name</param>
        /// <returns>条件にマッチする最初の子要素(無ければnull)</returns>
        public static T FindChild<T>(DependencyObject parent, string childName)
           where T : DependencyObject
        {
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                T childType = child as T;
                if (childType == null)
                {
                    foundChild = FindChild<T>(child, childName);

                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }
        */
    }
}
