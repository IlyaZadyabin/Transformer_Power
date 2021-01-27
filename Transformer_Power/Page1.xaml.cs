using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Transformer_Power
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }
         private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void SendInfoButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("H2 concentration is " + H2_Box.Text + Environment.NewLine +
                            "CO concentration is " + CO_Box.Text + Environment.NewLine +
                            "C2H4 concentration is " + C2H4_Box.Text + Environment.NewLine +
                            "C2H2 concentration is " + C2H2_Box.Text + Environment.NewLine );
            MainWindow objMainWindows = (MainWindow)Window.GetWindow(this);
            objMainWindows.frame.Navigate(new Page2());

        }
    }
}
