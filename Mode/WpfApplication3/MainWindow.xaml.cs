using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfApplication3
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        List<string> list = new List<string>();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            var dialog= fbd.ShowDialog();
            if (dialog==System.Windows.Forms.DialogResult.OK)
            {
              var filepath=  fbd.SelectedPath;
              this.txt.Text = filepath;
            var paths=  Directory.GetFiles(filepath);
            foreach (var item in paths)
            {
                FileInfo FI = new FileInfo(item);
                if (FI.Extension.Contains("jpg")||FI.Extension.Contains("png"))
                {
                    list.Add(item);
                }
            }
            this.list1.ItemsSource = list;

            }
        }

        private void list1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          var paths=this.list1.SelectedItems;
          this.list2.ItemsSource = paths;
        }
    }
}
