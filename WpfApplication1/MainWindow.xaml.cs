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

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            GetGrid(this.grid1);
        }

        Grid OldGrid;
        //Grid NewGrid;
        public void GetGrid(Grid panel)
        {
          
            if (OldGrid != null)
            {
                this.OldGrid.Visibility = Visibility.Collapsed;
            }
            this.OldGrid = panel;
            //NewGrid = panel;
            if (panel != null)
            {
                panel.Visibility = Visibility.Visible;
            }


        }
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            this.GetGrid(this.grid1);
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            this.GetGrid(this.grid2);

        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            this.GetGrid(this.grid3);
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            this.GetGrid(this.grid4);
        }
    }
}
