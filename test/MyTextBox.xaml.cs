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
	/// MyTextBox.xaml 的交互逻辑
	/// </summary>
	public partial class MyTextBox : UserControl
	{
		public MyTextBox()
		{
			this.InitializeComponent();
            if (string.IsNullOrWhiteSpace(TipContent)) {
              _tipContent = "请输入帐号";
            }
            userName.DataContext = TipContent;
           
		}
        private string _tipContent=string.Empty;
        public string TipContent
        {
            get
            {
                return _tipContent;
            }
            set { if (value != _tipContent) 
            {
                _tipContent = value; 
                userName.DataContext = TipContent;
            } }
        }

        private string _Text = string.Empty;
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                if (value != _Text) 
            {
                _Text = value; 
                //userName.DataContext = TipContent;
            } }
        }

        private void userName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(userName.Text)) 
            {
                _Text = userName.Text;
            }
        }
	}
}
