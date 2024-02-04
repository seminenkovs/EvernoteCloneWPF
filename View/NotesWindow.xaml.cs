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
using System.Windows.Shapes;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace EvernoteCloneWPF.View
{
    /// <summary>
    /// Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        public NotesWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_OnClick_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void SpeechButton_OnClick(object sender, RoutedEventArgs e)
        {
            // TODO: get region and key for evernote speech to text
            string region = "";
            string key = "";

            var speechConfig = SpeechConfig.FromSubscription(key, region);
            using (var audioConfig = AudioConfig.FromDefaultMicrophoneInput())
            {
                using (var recognizer = new SpeechRecognizer(speechConfig, audioConfig))
                {
                    var result = await recognizer.RecognizeOnceAsync();
                    contentRichTextBox.Document.Blocks.Add(new Paragraph(
                        new Run(result.Text)));
                }
            }
        }

        private void ContentRichTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            int amountOfCharacters = (new TextRange(contentRichTextBox.Document.ContentStart,
                contentRichTextBox.Document.ContentEnd)).Text.Length;

            statusTextBlock.Text = $"Document length: {amountOfCharacters} characters";
        }

        private void BoldButton_OnClick(object sender, RoutedEventArgs e)
        {
            contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty,
                FontWeights.Bold);
        }
    }
}
