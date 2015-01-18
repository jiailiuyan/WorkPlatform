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
using Jisons;
using System.Windows.Shapes;
using System.IO;
using System.Reflection;

namespace ControlLib
{
    /// <summary>
    /// ChatInputBox.xaml 的交互逻辑
    /// </summary>
    public partial class ChatInputBox : UserControl
    {

        public FlowDocument Document
        {
            get
            {
                return this.inputbox.Document;
            }
        }

        public ChatInputBox()
        {
            InitializeComponent();
        }

        public void ScrollToEnd()
        {
            this.inputbox.ScrollToEnd();
        }

        bool isontWeight = false;
        bool isontTilt = false;
        bool isFontUnderLine = false;

        /// <summary>
        /// cuti
        /// </summary>
        public void SetFontWeight()
        {
            this.inputbox.SetFontWeight(isontWeight);
            isontWeight = !isontWeight;
        }

        /// <summary>
        /// xiahuaxian
        /// </summary>
        public void SetFontUnderLine()
        {
            this.inputbox.SetFontUnderLine(isFontUnderLine);
            isFontUnderLine = !isFontUnderLine;
        }

        /// <summary>
        /// xieti
        /// </summary>
        public void SetFontTilt()
        {
            this.inputbox.SetFontTilt(isontTilt);
            isontTilt = !isontTilt;
        }

        public void AddControl(UIElement uiElement)
        {
            new InlineUIContainer(uiElement, this.inputbox.Selection.Start);
            //this.inputbox.Document.Blocks.Add(new BlockUIContainer(uiElement));
        }

        public string GetString()
        {
            var bs = this.inputbox.Document.Blocks.FirstBlock as Paragraph;
            if (bs == null)
            {
                return string.Empty;
            }


            foreach (var item in bs.Inlines)
            {
                var rrr = item as InlineUIContainer;
                if (rrr != null)
                {
                    var rt = rrr.Child;

                }
            }


            MemoryStream s = new MemoryStream();
            TextRange documentTextRange = new TextRange(this.inputbox.Document.ContentStart, this.inputbox.Document.ContentEnd);
            documentTextRange.Save(s, DataFormats.XamlPackage);
            var t = documentTextRange.Text;

            var a = System.Windows.Markup.XamlWriter.Save(this.inputbox);

            return a;// Convert.ToBase64String(s.ToArray());
        }

    }
}
