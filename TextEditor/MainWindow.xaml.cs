using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextEditor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        int startPos=0;
        int endPos=1;
        private TextPointer textPointerStart;
        private TextPointer textPointerEnd;
        public MainWindow()
        {
            InitializeComponent();
            textPointerStart = richTextBox.GetTextPointerAtOffset(startPos); 
            textPointerEnd = richTextBox.GetTextPointerAtOffset(endPos);

        }
       
        private int GetLenght()
        {
            TextRange content = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            return content.Text.Length;
        }
        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
    
         
           GetSelectionText().ApplyPropertyValue(Run.FontWeightProperty,FontWeights.Bold);
        }
        public void MakeInactive()
        {
            boldButton.IsEnabled = false;
            italicButton.IsEnabled = false;
            underlineButton.IsEnabled = false;
            clearButton.IsEnabled = false;
            comboBoxSize.IsEnabled = false;
            comboBoxColor.IsEnabled = false;
        }
        public void MakeActive()
        {
            boldButton.IsEnabled = true;
            italicButton.IsEnabled = true;
            underlineButton.IsEnabled = true;
            clearButton.IsEnabled = true;
            comboBoxSize.IsEnabled = true;
            comboBoxColor.IsEnabled = true;
        }

        private void TextBoxStart_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (paragraph == null)
                return;
            
            string line = textBoxStart.Text;

            if (int.TryParse(line, out int startPos) && startPos>=0 && startPos<=endPos-1)
            {
                this.startPos = startPos;
                MakeActive();
            }
            else if(startPos>endPos-1)
            {
                this.startPos = startPos;
                MakeInactive();                            
            }
            else
            {
                MakeInactive();
            }

        }

        private void TextBoxEnd_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (paragraph == null)
                return;

            string line = textBoxEnd.Text.Trim();
            if ( int.TryParse(line, out int endPos) && endPos >= startPos &&  endPos <GetLenght()-1)
            {
                this.endPos = endPos+1;
                MakeActive();
            }
            else if(endPos < startPos)
            {
                this.endPos = endPos + 1;
                MakeInactive();              
            }
            else
            {
                MakeInactive();
            }
        }


        private TextSelection GetSelectionText()
        {
            textPointerStart = richTextBox.GetTextPointerAtOffset(startPos);
            textPointerEnd = richTextBox.GetTextPointerAtOffset(endPos);  
            richTextBox.Selection.Select(textPointerStart, textPointerEnd);         
            return richTextBox.Selection;
        }
        private void italicButton_Click(object sender, RoutedEventArgs e)
        {
         
            GetSelectionText().ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Italic);
        }

        private void underlineButton_Click(object sender, RoutedEventArgs e)
        {
         
            GetSelectionText().ApplyPropertyValue(Run.TextDecorationsProperty, TextDecorations.Underline);
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            TextSelection text = GetSelectionText();
            text.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Normal);
            text.ApplyPropertyValue(Run.TextDecorationsProperty, null);
            text.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Normal);
            text.ApplyPropertyValue(RichTextBox.FontSizeProperty, 16.0);
            text.ApplyPropertyValue(Run.ForegroundProperty, new SolidColorBrush( Colors.Black));
       
        }

        private void comboBoxSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (paragraph != null && comboBoxSize.SelectedItem != null)
            {
             
                double value = double.Parse(((ComboBoxItem)comboBoxSize.SelectedItem).Content.ToString());
                GetSelectionText().ApplyPropertyValue(RichTextBox.FontSizeProperty,value);
            }
        }

        private void comboBoxColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxColor.SelectedItem != null && paragraph!=null)
            {
               Color color=(Color)( ColorConverter.ConvertFromString(((ComboBoxItem)comboBoxColor.SelectedItem).Tag.ToString()));
                 GetSelectionText().ApplyPropertyValue(Run.ForegroundProperty,new SolidColorBrush(color));
            }
        }
    }
    }

