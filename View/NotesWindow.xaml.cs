using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace EvernoteCloneWPF.View
{
    /// <summary>
    /// Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        //private SpeechRecognitionEngine _recognizer;

        public NotesWindow()
        {
            InitializeComponent();

            #region System.Speech speech recognizer
            //var currentCulture = (from r in SpeechRecognitionEngine.InstalledRecognizers()
            //    where r.Culture.Equals(Thread.CurrentThread.CurrentCulture)
            //    select r).FirstOrDefault();

            //_recognizer = new SpeechRecognitionEngine(currentCulture);

            //GrammarBuilder builder = new GrammarBuilder();
            //builder.AppendDictation();
            //Grammar grammar = new Grammar(builder);

            //_recognizer.LoadGrammar(grammar);
            //_recognizer.SetInputToDefaultAudioDevice();
            //_recognizer.SpeechRecognized += Recognizer_SpeechRecognized;
            #endregion

            var fontFamilies = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            fontFamilyComboBox.ItemsSource = fontFamilies;

            List<double> fontSizes =new List<double>() {8, 9, 10, 11, 12, 14, 16, 28, 46};
            fontSizeComboBox.ItemsSource = fontSizes;
        }

        private void Recognizer_SpeechRecognized(object? sender, SpeechRecognizedEventArgs e)
        {
            string recognizedText = e.Result.Text;

            contentRichTextBox.Document.Blocks.Add(new Paragraph(
                new Run(recognizedText)));
        }

        private void MenuItem_OnClick_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //private bool isRecognizing = false;
        private async void SpeechButton_OnClick(object sender, RoutedEventArgs e)
        {
            #region Microsoft.CognitiveServices.Speech

            //get region and key for evernote speech to text
            //string region = "";
            //string key = "";

            //var speechConfig = SpeechConfig.FromSubscription(key, region);
            //using (var audioConfig = AudioConfig.FromDefaultMicrophoneInput())
            //{
            //    using (var recognizer = new SpeechRecognizer(speechConfig, audioConfig))
            //    {
            //        var result = await recognizer.RecognizeOnceAsync();
            //        contentRichTextBox.Document.Blocks.Add(new Paragraph(
            //            new Run(result.Text)));
            //    }
            //}

            #endregion

            #region System.Speech

            //if (!isRecognizing)
            //{
            //    _recognizer.RecognizeAsync(RecognizeMode.Multiple);
            //}
            //else
            //{
            //    _recognizer.RecognizeAsyncStop();
            //    isRecognizing = false;
            //}
            
            #endregion
        }

        private void ContentRichTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            int amountOfCharacters = (new TextRange(contentRichTextBox.Document.ContentStart,
                contentRichTextBox.Document.ContentEnd)).Text.Length;

            statusTextBlock.Text = $"Document length: {amountOfCharacters} characters";
        }

        private void BoldButton_OnClick(object sender, RoutedEventArgs e)
        {
            bool isButtonChecked = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonChecked)
            {
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty,
                    FontWeights.Bold);
            }
            else
            {
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty,
                    FontWeights.Normal);
            }
            
        }

        private void ContentRichTextBox_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedWeight = contentRichTextBox.Selection
                .GetPropertyValue(FontWeightProperty);
            boldButton.IsChecked = (selectedWeight != DependencyProperty.UnsetValue) &&
                                   (selectedWeight.Equals(FontWeights.Bold));

            var selectedStyle = contentRichTextBox.Selection.GetPropertyValue(
                Inline.FontStyleProperty);
            italicButton.IsChecked = (selectedStyle != DependencyProperty.UnsetValue) &&
                                     (selectedStyle.Equals(FontStyles.Italic));

            var selectedDecoration = contentRichTextBox.Selection.GetPropertyValue(
                Inline.TextDecorationsProperty);
            underlineButton.IsChecked = (selectedDecoration != DependencyProperty.UnsetValue) &&
                                        (selectedDecoration.Equals(TextDecorations.Underline));

            fontFamilyComboBox.SelectedItem = contentRichTextBox.Selection.GetPropertyValue(
                Inline.FontFamilyProperty);

            fontSizeComboBox.Text = (contentRichTextBox.Selection.GetPropertyValue(
                Inline.FontSizeProperty)).ToString();
        }

        private void ItalicButton_OnClick(object sender, RoutedEventArgs e)
        {
            bool isButtonEnabled = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonEnabled)
            {
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty,
                    FontStyles.Italic);
            }
            else
            {
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty,
                    FontStyles.Normal);
            }
        }

        private void UnderlineButton_OnClick(object sender, RoutedEventArgs e)
        {
            bool isButtonEnabled = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonEnabled)
            {
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty,
                    TextDecorations.Underline);
            }
            else
            {
                TextDecorationCollection textDecorations;
                (contentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty)
                    as TextDecorationCollection).TryRemove(TextDecorations.Underline,
                    out textDecorations);
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty,
                    textDecorations);
            }
        }

        private void FontFamilyCombox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fontFamilyComboBox.SelectedItem != null)
            {
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty,
                    fontFamilyComboBox.SelectedItem);
            }
        }

        private void FontSizeComboBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty,
                fontSizeComboBox.Text);
        }
    }
}
