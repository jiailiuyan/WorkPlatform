using ClientManager;
using ClientManager.Controls;
using CommonHelper;
using Project.Entities;
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
using PlatformCommon.Message;
using PlatformCommon.Plugin;
using Jisons;
using ControlLib;
using PlatformCommon.Manager;
using PlatformCommon.Events;
using System.IO;
using System.Xml;

namespace Modules.ChatModule
{
    /// <summary>
    /// ChatControl.xaml 的交互逻辑
    /// </summary>
    public partial class ChatControl : UserControl, IPluginObject
    {
        #region IPluginObject 成员

        public string PluginName
        {
            get
            {
                return Person != null ? Person.Name : "";
            }
        }

        public ImageSource PluginIcon
        {
            get
            {
                return null;
            }
        }

        private FrameworkElement pluginobject;
        public FrameworkElement Plugin
        {
            get
            {
                return pluginobject;
            }
        }

        public PluginType Type
        {
            get { return PluginType.Window; }
        }

        public event EventHandler Closing;
        protected void OnClosingChanged()
        {
            if (this.Closing != null)
            {
                this.Closing(this, EventArgs.Empty);
            }
        }

        public event EventHandler Opening;
        protected void OpeningChanged()
        {
            if (this.Opening != null)
            {
                this.Opening(this, EventArgs.Empty);
            }
        }

        public event EventHandler Hiding;
        protected void HidingChanged()
        {
            if (this.Hiding != null)
            {
                this.Hiding(this, EventArgs.Empty);
            }
        }
        public bool IsShow
        {
            get
            {
                return this.Visibility == System.Windows.Visibility.Visible;
            }
            set
            {
                if (value)
                {
                    OpeningChanged();
                }
                else
                {
                    HidingChanged();
                }
            }
        }

        public bool IsTool
        {
            get { return true; }
        }

        #endregion

        public ClientInfo Person;

        public ChatControl(ClientInfo person)
        {
            InitializeComponent();

            Person = person;

            this.pluginobject = this;
            this.Loaded += ChatControl_Loaded;
        }

        void ChatControl_Loaded(object sender, RoutedEventArgs e)
        {
            WorkClient.Instance.OnFileAction += client_OnFileAction;

            foreach (var plugin in PluginManager.Instance.PluginObjects)
            {
                MenuItem item = new MenuItem();
                item.Header = plugin.PluginName;

                item.Click += (itemsender, iteme) =>
                {
                    CanvasButton cb = new CanvasButton();
                    cb.InitCanvasButton(plugin);
                    cb.Width = 50;
                    cb.Height = 50;
                    //cb.AddEvent += (cbs, cbe) =>
                    //{
                    //    GlobalEvent.Instance.EventAggregator.GetEvent<PluginsEvent>().Publish(new PluginsEventArgs() { Action = PluginAction.Show, PluginObject = plugin });
                    //    plugin.IsShow = true;
                    //};
                    this.inputrichbox.AddControl(cb);
                };
                this.pluginmenu.Items.Add(item);
            }
        }

        void client_OnFileAction(object sender, FileActionArgs e)
        {

            switch (e.Type)
            {
                case ControlType.SendStart:
                    {
                        if (!this.filescontrol.Children.Contains(e.Control))
                        {
                            this.filescontrol.Children.Add(e.Control);
                        }
                        break;
                    }

                case ControlType.SendCancel:
                case ControlType.SendRefuse:
                    {

                        this.filescontrol.Children.Remove(e.Control);
                        break;
                    }
                case ControlType.SendComplete:
                    {
                        var path = "";
                        var control = e.Control as SendFileControl;

                        if (control != null)
                        {
                            path = control.FileName + " 发送已经完成"; ;
                        }

                        //this.receivemsg.Text += Environment.NewLine;
                        //this.receivemsg.Text += path;
                        var title = path + "  " + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine;
                        var p = new Paragraph();
                        var r = new Run(title);
                        p.Inlines.Add(r);
                        p.Foreground = Brushes.Red;
                        this.viewrichbox.Document.Blocks.Add(p);
                        this.viewrichbox.ScrollToEnd();

                        this.filescontrol.Children.Remove(e.Control);
                        break;
                    }



                case ControlType.ReceiveStart:
                    {
                        if (!this.filescontrol.Children.Contains(e.Control))
                        {
                            this.filescontrol.Children.Add(e.Control);
                        }
                        break;
                    }

                case ControlType.ReceiveCancel:
                case ControlType.ReceiveRefuse:
                    {
                        this.filescontrol.Children.Remove(e.Control);
                        break;
                    }
                case ControlType.ReceiveComplete:
                    {
                        var path = "";
                        var control = e.Control as ReceiveFloderControl;
                        if (control != null)
                        {

                            path = control.FloderPath + " 接收已经完成"; ;

                        }
                        else
                        {
                            var filecontrol = e.Control as ReceiveFileControl;
                            path = filecontrol.FileName + " 接收已经完成"; ;
                        }


                        var title = path + "  " + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine;
                        var p = new Paragraph();
                        var r = new Run(title);
                        p.Inlines.Add(r);
                        p.Foreground = Brushes.Red;
                        this.viewrichbox.Document.Blocks.Add(p);
                        this.viewrichbox.ScrollToEnd();
                        //this.receivemsg.Text += Environment.NewLine;
                        //this.receivemsg.Text += path;

                        this.filescontrol.Children.Remove(e.Control);
                        break;
                    }


                default: break;
            }

        }

        private void send_Click(object sender, RoutedEventArgs e)
        {
            var data = new MessageData();
            data.Type = MessageType.Message;

            data.SentUser = WorkClient.Instance.Person.ClientInfo;
            data.ResiveUser = Person;

            var xaml = this.inputrichbox.GetString();
            if (string.IsNullOrWhiteSpace(xaml))
            {
                return;
            }

            data.Value = xaml;

            var a = System.Windows.Markup.XamlReader.Parse(xaml);
            var rich = a as RichTextBox;
            var bs = rich.Document.Blocks.FirstBlock as Paragraph;
            foreach (var item in bs.Inlines)
            {
                var rrr = item as InlineUIContainer;
                if (rrr != null)
                {
                    var rt = rrr.Child as CanvasButton;
                    if (rt.Tag != null && !string.IsNullOrWhiteSpace(rt.Tag.ToString()))
                    {
                        var plugin = PluginManager.Instance.PluginObjects.FirstOrDefault(i => i.PluginName == rt.Tag.ToString());
                        if (plugin != null)
                        {
                            rt.InitCanvasButton(plugin);
                            rt.IsSelected = false;
                        }
                    }
                }
            }

            var title = WorkClient.Instance.Person.ClientInfo.Name + "  " + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine;
            var p = new Paragraph();
            var r = new Run(title);
            p.Inlines.Add(r);
            p.Foreground = Brushes.Blue;//设置字体颜色  
            this.viewrichbox.Document.Blocks.Add(p);

            this.viewrichbox.Document.Blocks.Add(bs);
            this.viewrichbox.ScrollToEnd();

            WorkClient.Instance.SendMessage(data);

            this.inputrichbox.Document.Blocks.Clear();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            OnClosingChanged();
        }

        private void sendfile_Click(object sender, RoutedEventArgs e)
        {
            var files = DialogHelper.SearchFiles(); ;
            if (files != null && files.Count() > 0)
            {
                foreach (var item in files)
                {
                    var data = new MessageData();
                    data.Type = MessageType.StartSendFile;

                    data.Value = new SendFileData() { IsFloder = false, Path = item };
                    data.SentUser = WorkClient.Instance.Person.ClientInfo;
                    data.ResiveUser = Person;
                    WorkClient.Instance.SendMessage(data);
                }
            }
        }

        private void sendfloder_Click(object sender, RoutedEventArgs e)
        {
            var floder = DialogHelper.SearchFolder();
            if (!string.IsNullOrWhiteSpace(floder))
            {
                var data = new MessageData();
                data.Type = MessageType.StartSendFile;
                data.Value = new SendFileData() { IsFloder = true, Path = floder };
                data.SentUser = WorkClient.Instance.Person.ClientInfo; ;
                data.ResiveUser = Person;
                WorkClient.Instance.SendMessage(data);
            }
        }

        public void AddMessage(MessageData message)
        {
            if (message.ResiveUser.KeyId == WorkClient.Instance.Person.ClientInfo.KeyId)
            {

                var a = System.Windows.Markup.XamlReader.Parse(message.Value.ToString());

                var rich = a as RichTextBox;

                var bs = rich.Document.Blocks.FirstBlock as Paragraph;

                foreach (var item in bs.Inlines)
                {
                    var rrr = item as InlineUIContainer;
                    if (rrr != null)
                    {
                        var rt = rrr.Child as CanvasButton;
                        if (rt.Tag != null && !string.IsNullOrWhiteSpace(rt.Tag.ToString()))
                        {
                            var plugin = PluginManager.Instance.PluginObjects.FirstOrDefault(i => i.PluginName == rt.Tag.ToString());
                            if (plugin != null)
                            {
                                rt.InitCanvasButton(plugin);
                                rt.IsSelected = false;
                            }
                        }
                    }

                }

                var title = message.SentUser.Name + "  " + DateTime.Now.ToString("HH:mm:ss") + Environment.NewLine;
                var p = new Paragraph();
                var r = new Run(title);
                p.Inlines.Add(r);
                p.Foreground = Brushes.Blue;//设置字体颜色  
                this.viewrichbox.Document.Blocks.Add(p);

                this.viewrichbox.Document.Blocks.Add(bs);
                this.viewrichbox.ScrollToEnd();

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.inputrichbox.SetFontWeight();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.inputrichbox.SetFontTilt();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.inputrichbox.SetFontUnderLine();
        }

    }
}
