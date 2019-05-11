using System;
using System.Security.Cryptography;
using System.Windows;

namespace PassGen
{
    public partial class MainWindow
    {
        private static readonly RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();

        private static int GetNextNumber(int maxValue)
        {
            var symbol = new byte[1];
            randomNumberGenerator.GetBytes(symbol);
            return symbol[0] % maxValue;
        }

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void GenerateButtonClick(object sender, RoutedEventArgs e)
        {
            // for more info check ASCII table

            var resultPassword = string.Empty;

            const int startShift = ' ' + 1;
            const int charRange = 'z' - '!' + 1;

            for (var i = 0; i < PasswordLengthSlider.Value; i++)
            {
                var charValue = startShift + GetNextNumber(charRange);

                if (UseDigitsCheckBox.IsChecked == true)
                {
                    if (UseSpecialSymbolsCheckBox.IsChecked == false)
                    {
                        while (charValue < '0' || charValue > '9' && charValue < 'a' || charValue > 'z' && charValue < 'A' || charValue > 'Z')
                            charValue = startShift + GetNextNumber(charRange);
                    }
                }
                else
                {
                    if (UseSpecialSymbolsCheckBox.IsChecked == false)
                    {
                        while (charValue < 'a' || charValue > 'z' && charValue < 'A' || charValue > 'Z')
                            charValue = startShift + GetNextNumber(charRange);
                    }
                    else
                    {
                        while (charValue > '0' && charValue < '9')
                            charValue = startShift + GetNextNumber(charRange);
                    }
                }

                resultPassword += Convert.ToChar(charValue);
            }
            ResultTextBox.Text = resultPassword;
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
