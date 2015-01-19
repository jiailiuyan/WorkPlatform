using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Project.BusinessFacade;
using ServerManage;
using PlatformCommon.Message;
using Jisons;

namespace PrismServer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public List<PersonData> Persons
        {
            get { return (List<PersonData>)GetValue(PersonsProperty); }
            set { SetValue(PersonsProperty, value); }
        }
        public static readonly DependencyProperty PersonsProperty =
            DependencyProperty.Register("Persons", typeof(List<PersonData>), typeof(MainWindow), new PropertyMetadata(null));

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> ip = new List<string>();
            var net = NetHelper.GetNetInfo();

            net.ForEach(i => { ip.Add(i.Name + " " + i.IP[0]); });
            this.serverip.ItemsSource = ip;
            this.serverip.SelectedIndex = 0;
        }

        public void InitPersons()
        {
            Persons = WorkService.Instance.Connections;
            this.dataGrid.ItemsSource = Persons;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var ip = this.serverip.SelectedValue.ToString().Split(' ')[1];
            WorkService.Start(ip, int.Parse(this.serverport.Text));
            InitPersons();

            var btn = sender as Button;
            btn.IsEnabled = false;
        }
    }
}

