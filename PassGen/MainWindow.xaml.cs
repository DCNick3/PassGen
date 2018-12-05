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

namespace PassGen
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rnd = new Random();
        int c = 0;
        public MainWindow()
        {
            InitializeComponent();
            PLength.Minimum = 1;
            PLength.Maximum = 25;
            PLength.Value = 1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string s = String.Empty;
            for (int i = 0; i < PLength.Value; i++)
            {
                int xchar = 33 + rnd.Next(94);
                if (Digits.IsChecked == true)
                {
                    if (Symbols.IsChecked == false)
                    {
                        while (xchar < 48 || xchar > 57 && xchar < 65 || xchar > 90 && xchar < 97 || xchar > 122)
                        {
                            xchar = 33 + rnd.Next(94);
                        }
                    }
                }
                else
                {
                    if (Symbols.IsChecked == false)
                    {
                        while (xchar < 65 || xchar > 90 && xchar < 97 || xchar > 122)
                        {
                            xchar = 33 + rnd.Next(94);
                        }
                    }
                    else
                    {
                        while (xchar > 47 && xchar < 58)
                        {
                            xchar = 33 + rnd.Next(94);
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
    }
}
