using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows;

namespace PassGen
{
    public partial class MainWindow
    {
        private static readonly RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();

        private static char GetNewChar()
        {
            var symbol = new byte[1];
            randomNumberGenerator.GetBytes(symbol);

            // for more info check ASCII table
            return (char) (symbol[0] % ('z' - '!' + 1) + '!');
        }

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private delegate bool CharCheckDelegate(char value);
        
        private void GenerateButtonClick(object sender, RoutedEventArgs e)
        {
            var resultPassword = string.Empty;

            var invalidChecks = new List<CharCheckDelegate>();

            if (UseDigitsCheckBox.IsChecked == false)
                invalidChecks.Add(char.IsDigit);
            if (UseSpecialSymbolsCheckBox.IsChecked == false)
                invalidChecks.Add(x => !char.IsLetterOrDigit(x));

            for (var i = 0; i < PasswordLengthSlider.Value; i++)
            {
                char charValue;

                do
                {
                    charValue = GetNewChar();
                } while (invalidChecks.Any(x => x(charValue)));

                resultPassword += charValue;
            }
            ResultTextBox.Text = resultPassword;
        }

        private void PasswordLengthSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            CurrentPasswordLengthLabel.Content = PasswordLengthSlider.Value;
        }

        private void CopyButtonClick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(ResultTextBox.Text);
        }
    }
}
