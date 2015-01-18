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
using Jisons;
using PlatformCommon.Plugin;
using PlatformCommon.Manager;
using PlatformCommon.Events;

namespace ControlLib
{
    /// <summary>
    /// CanvasButton.xaml 的交互逻辑
    /// </summary>
    public partial class CanvasButton : UserControl, IActionControl
    {
        //<Rectangle.Fill>
        //        <ImageBrush ImageSource="/ControlLib;component/Images/toolbar_pushed.png" />
        //    </Rectangle.Fill>
        public static readonly Brush SelectBrush = new ImageBrush() { ImageSource = new BitmapImage(new Uri("pack://application:,,,/ControlLib;component/Images/toolbar_pushed.png", UriKind.RelativeOrAbsolute)) };
        // new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5c4c4c4c"));

        public static readonly Brush UnSelectBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0000"));

        public IPluginObject PluginObject { get; private set; }

        //public bool IsSelected
        //{
        //    get { return (bool)GetValue(IsSelectedProperty); }
        //    set { SetValue(IsSelectedProperty, value); }
        //}
        //public static readonly DependencyProperty IsSelectedProperty =
        //    DependencyProperty.Register("IsSelected", typeof(bool), typeof(CanvasButton), new PropertyMetadata(false, IsSelectedChangedCallback));
        //static void IsSelectedChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var control = d as CanvasButton;
        //    control.SetSelectedView((bool)e.NewValue);
        //}

        public Button showButton = null;

        public Rectangle selectedview = null;

        public event EventHandler AddEvent;
        protected void AddingEvent()
        {
            GlobalEvent.Instance.EventAggregator.GetEvent<PluginsEvent>().Publish(new PluginsEventArgs() { Action = PluginAction.Show, PluginObject = PluginObject });
            PluginObject.IsShow = true;
        }

        public CanvasButton()
        {
            InitializeComponent();
        }

        public void InitCanvasButton(IPluginObject po)
        {
            IsSelected = true;
            this.Loaded += CanvasButton_Loaded;
            PluginObject = po;

            this.Tag = po.PluginName;

            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (showButton == null)
            {
                showButton = this.FindVisualChild<Button>();
                if (showButton != null)
                {
                    this.showButton.Background = new ImageBrush(PluginObject.PluginIcon) { Stretch = Stretch.Uniform };
                    this.showButton.Content = PluginObject.PluginName;
                }
                return;
            }

            if (selectedview == null)
            {
                selectedview = this.FindVisualChild<Rectangle>();
            }

            if (defaultView == null)
            {
                defaultView = this.showButton.GetImageSource();
                if (defaultView != null)
                {
                    CompositionTarget.Rendering -= CompositionTarget_Rendering;
                }
            }
        }

        void CanvasButton_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }


        protected override void OnPreviewMouseDoubleClick(MouseButtonEventArgs e)
        {
            AddingEvent();
            base.OnPreviewMouseDoubleClick(e);
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);
        }

        public void SetSelectedView(bool isselected)
        {
            if (this.selectedview != null)
            {
                this.selectedview.Fill = isselected ? SelectBrush : UnSelectBrush;
            }
        }

        #region IActionControl 成员

        bool isSelected = false;
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                SetSelectedView(isSelected);
            }
        }

        ImageSource defaultView = null;
        public ImageSource DefaultView
        {
            get
            {
                return defaultView;
            }
        }

        public FrameworkElement Element
        {
            get { return this; }
        }

        #endregion

        private void btn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
