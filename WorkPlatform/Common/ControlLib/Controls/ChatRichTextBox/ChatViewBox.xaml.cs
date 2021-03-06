﻿using System;
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

namespace ControlLib
{
    /// <summary>
    /// ChatRichTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class ChatViewBox : UserControl
    {
        public FlowDocument Document
        {
            get
            {
                return this.viewbox.Document;
            }
        }

        public ChatViewBox()
        {
            InitializeComponent();

            //this.viewbox.IsReadOnly = true;
        }
        public void ScrollToEnd()
        {
            this.viewbox.ScrollToEnd();
        }

        public void AddControl(UIElement uiElement)
        {
            new InlineUIContainer(uiElement, this.viewbox.Selection.Start);
            //this.inputbox.Document.Blocks.Add(new BlockUIContainer(uiElement));
        }
    }
}
