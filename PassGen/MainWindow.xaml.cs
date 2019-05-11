using System;
using System.Security.Cryptography;
using System.Windows;
using PassGenLib;

namespace PassGen
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void GenerateButtonClick(object sender, RoutedEventArgs e)
        {
            var range = CharacterRange.LowercaseEnglishLetters + CharacterRange.UppercaseEnglishLetters;
            if (UseDigitsCheckBox.IsChecked)
                range += CharacterRange.Digits;
            if (UseSpecialSymbolsCheckBox)
                range += CharacterRange.SpecialCharacters;
            
            var password = new PasswordGenerator(range).MakePassword(PasswordLengthSlider.Value);

            ResultTextBox.Text = password;
        }

        private void PasswordLengthSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (CurrentPasswordLengthLabel != null)
                CurrentPasswordLengthLabel.Content = PasswordLengthSlider.Value;
        }

        private void CopyButtonClick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(ResultTextBox.Text);
        }
    }
}
