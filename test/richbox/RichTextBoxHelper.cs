﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Jisons
{
    public static class RichTextBoxHelper
    {

        public static void SetFontWeight(this RichTextBox richetextbox, bool isdefault = true)
        {
            var textRange = new TextRange(richetextbox.Document.ContentStart, richetextbox.Document.ContentEnd);
            if (isdefault)
            {
                textRange.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            }
            else
            {
                textRange.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            }
        }

        public static void SetFontTilt(this RichTextBox richetextbox, bool isdefault = true)
        {
            var textRange = new TextRange(richetextbox.Document.ContentStart, richetextbox.Document.ContentEnd);
            if (isdefault)
            {
                textRange.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
            }
            else
            {
                textRange.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
            }
        }

        public static void SetFontUnderLine(this RichTextBox richetextbox, bool isdefault = true)
        {
            var textRange = new TextRange(richetextbox.Document.ContentStart, richetextbox.Document.ContentEnd);
            if (!textRange.IsEmpty)
            {
                if (isdefault)
                {
                    textRange.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                }
                else
                {
                    textRange.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
                }
            }
        }

        public static void SetFontColor(this RichTextBox richetextbox, Color color)
        {
            var textRange = new TextRange(richetextbox.Document.ContentStart, richetextbox.Document.ContentEnd);
            textRange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(color));
        }

        public static void SetFontSize(this RichTextBox richetextbox, int size)
        {
            var textRange = new TextRange(richetextbox.Document.ContentStart, richetextbox.Document.ContentEnd);
            textRange.ApplyPropertyValue(TextElement.FontSizeProperty, size);
        }

        //获取或设置输入插入符号的位置。
        //public TextPointer CaretPosition { get; set; }

        //private static void SaveFile(string filename, RichTextBox richTextBox)
        //{
        //    if (string.IsNullOrEmpty(filename))
        //    {
        //        throw new ArgumentNullException();
        //    }
        //    using (FileStream stream = File.OpenWrite(filename))
        //    {
        //        TextRange documentTextRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
        //        string dataFormat = DataFormats.Text;
        //        string ext = System.IO.Path.GetExtension(filename);
        //        if (String.Compare(ext, ".xaml", true) == 0)
        //        {
        //            dataFormat = DataFormats.Xaml;
        //        }
        //        else if (String.Compare(ext, ".rtf", true) == 0)
        //        {
        //            dataFormat = DataFormats.Rtf;
        //        }
        //        documentTextRange.Save(stream, dataFormat);
        //    }
        //}

        //private static void LoadFile(string filename, RichTextBox richTextBox)
        //{
        //    if (string.IsNullOrEmpty(filename))
        //    {
        //        throw new ArgumentNullException();
        //    }
        //    if (!File.Exists(filename))
        //    {
        //        throw new FileNotFoundException();
        //    }
        //    using (FileStream stream = File.OpenRead(filename))
        //    {
        //        TextRange documentTextRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
        //        string dataFormat = DataFormats.Text;
        //        string ext = System.IO.Path.GetExtension(filename);
        //        if (String.Compare(ext, ".xaml", true) == 0)
        //        {
        //            dataFormat = DataFormats.Xaml;
        //        }
        //        else if (String.Compare(ext, ".rtf", true) == 0)
        //        {
        //            dataFormat = DataFormats.Rtf;
        //        }
        //        documentTextRange.Load(stream, dataFormat);
        //    }
        //}


    }
}
