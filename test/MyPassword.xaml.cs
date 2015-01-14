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

namespace WpfApplication1
{
	/// <summary>
	/// MyPassword.xaml 的交互逻辑
	/// </summary>
	public partial class MyPassword : UserControl
	{
		public MyPassword()
		{
			this.InitializeComponent();
            if (string.IsNullOrWhiteSpace(TipContent))
            {
                _tipContent = "请输入密码";
            }
            txtPasswordTip.Text = TipContent;
		}

        string tip = string.Empty;
        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {

            if (password.Password.Length != 0)
            {
               // this.Content = password.Password;
                this._Password = password.Password;
                if (!string.IsNullOrWhiteSpace(txtPasswordTip.Text))
                    tip = txtPasswordTip.Text;
                txtPasswordTip.Text = string.Empty;
            }
            else { txtPasswordTip.Text = tip; }
        }

        private string _tipContent = string.Empty;
        public string TipContent
        {
            get
            {
                return _tipContent;
            }
            set
            {
                if (value != _tipContent)
                {
                    _tipContent = value;
                    txtPasswordTip.Text = TipContent;
                }
            }
        }

        private string _Password = string.Empty;
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                if (value != _Password)
                {
                    _Password = value;
                  
                }
            }
        }
      
	}
}