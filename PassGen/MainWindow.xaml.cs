using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows;

namespace PassGen
{
    public partial class MainWindow : Window
    {
        private RandomNumberGenerator rnd = RandomNumberGenerator.Create();
        public MainWindow()
        {
            InitializeComponent();
            PLength.Minimum = 1;
            PLength.Maximum = 40;
            PLength.Value = 10;
        }

        private int getNewNumber(int mx)
        {
            byte[] symbol = new byte[1];
            rnd.GetBytes(symbol);
            return symbol[0] % mx;
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string s = String.Empty;
            for (int i = 0; i < PLength.Value; i++)
            {
                int xchar = 33 + getNewNumber(94);
                if (Digits.IsChecked == true)
                {
                    if (Symbols.IsChecked == false)
                    {
                        while (xchar < 48 || xchar > 57 && xchar < 65 || xchar > 90 && xchar < 97 || xchar > 122)
                        {
                            xchar = 33 + getNewNumber(94);
                        }
                    }
                }
                else
                {
                    if (Symbols.IsChecked == false)
                    {
                        while (xchar < 65 || xchar > 90 && xchar < 97 || xchar > 122)
                        {
                            xchar = 33 + getNewNumber(94);
                        }
                    }
                    else
                    {
                        while (xchar > 47 && xchar < 58)
                        {
                            xchar = 33 + getNewNumber(94);
                        }
                    }
                }
                s += Convert.ToChar(xchar);
            };
            Result.Text = s;
        }

        private void PLength_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Current.Content = PLength.Value;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(Result.Text);
        }
    }
}
