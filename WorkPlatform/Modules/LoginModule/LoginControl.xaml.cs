using ClientManager;
using Jisons;
using System;
using System.Collections.Generic;
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
using System.Linq;
using PlatformCommon.ViewMode;

namespace Modules.LoginModule
{
    /// <summary>
    /// LoginControl.xaml 的交互逻辑
    /// </summary>
    public partial class LoginControl : UserControl
    {
        public LoginControl()
        {
            this.InitializeComponent();

            this.name.PreviewMouseLeftButtonDown += txt1_PreviewMouseLeftButtonDown;

            this.password.PreviewMouseLeftButtonDown += txt3_PreviewMouseLeftButtonDown;

            this.password.FocusableChanged += password_FocusableChanged;

            this.Loaded += LoginControl_Loaded;

            this.loginbutton.IsEnabled = false;

            WorkClient.Instance.OnMessageReceive += Instance_OnMessageReceive;
        }

        void password_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.txt4.Visibility = Visibility.Hidden;
        }

        void LoginControl_Loaded(object sender, RoutedEventArgs e)
        {

            var ip = ServerData.Instance.IP;
            if (!string.IsNullOrWhiteSpace(ip))
            {
                serverip.Text = ServerData.Instance.IP;
                port.Text = ServerData.Instance.Port.ToString();
                WorkClient.Instance.RegisterServerNode(ServerData.Instance.IP, ServerData.Instance.Port);
            }
            else
            {
                var ips = NetHelper.GetNetInfo().FirstOrDefault().IP;
                if (ips != null)
                {
                    serverip.Text = ips.FirstOrDefault();
                }
            }

            this.name.Text = "lisi";
            this.password.Text = "123456";

            JudgeName();
            JudgePassword();

        }

        void Instance_OnMessageReceive(object sender, PlatformCommon.Message.MessageDataArgs e)
        {
            switch (e.Data.Type)
            {
                case PlatformCommon.Message.MessageType.Login:
                    {
                        if (WorkClient.Instance.Person != null && WorkClient.Instance.Person.KeyId != 0)
                        {
                            this.msgText.Text = "登录成功";
                            this.Visibility = System.Windows.Visibility.Collapsed;
                        }
                        else
                        {
                            this.msgText.Text = "登录失败，请检查用户名密码";
                        }

                        break;
                    }

                case PlatformCommon.Message.MessageType.ConnetServer:
                    {

                        this.loginbutton.IsEnabled = true;
                        break;
                    }
            }
        }

        void txt3_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.txt4.Visibility = Visibility.Hidden;
            this.JudgeName();
        }

        void txt1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.txt2.Visibility = Visibility.Hidden;
            this.JudgePassword();
        }

        public void JudgeName()
        {
            if (string.IsNullOrWhiteSpace(this.name.Text))
            {
                this.txt2.Visibility = Visibility.Visible;
            }
            else
            {
                this.txt2.Visibility = Visibility.Hidden;
            }
        }

        public void JudgePassword()
        {
            if (string.IsNullOrWhiteSpace(this.password.Text))
            {
                this.txt4.Visibility = Visibility.Visible;
            }
            else
            {
                this.txt4.Visibility = Visibility.Hidden;
            }
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            var name = this.name.Text;
            var pw = this.password.Text;
            WorkClient.Instance.Login(name, pw);
        }

        private void saveServer_Click(object sender, RoutedEventArgs e)
        {
            ServerData.Instance.SaveData(serverip.Text, port.Text);
            WorkClient.Instance.RegisterServerNode(ServerData.Instance.IP, ServerData.Instance.Port);
        }
    }
}