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

namespace SZGYA_2024_09_04_Utazasok
{
    /// <summary>
    /// Interaction logic for eUtazasGui.xaml
    /// </summary>
    public partial class eUtazasGui : Window
    {
        public eUtazasGui()
        {
            InitializeComponent();
        }

        private void rbtnJegyShow(object sender, RoutedEventArgs e)
        {
            grpbxBerlet.Visibility = Visibility.Collapsed;
            grpbxJegy.Visibility = Visibility.Visible;
        }
        private void rbtnBerletShow(object sender, RoutedEventArgs e)
        {
            grpbxJegy.Visibility = Visibility.Collapsed;
            grpbxBerlet.Visibility = Visibility.Visible;
        }
    }
}
