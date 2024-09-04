using System.IO;
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

namespace SZGYA_2024_09_04_Utazasok
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var utasadatok = new List<eUtazas>();
            var sr = new StreamReader("../../../src/utasadat.txt", encoding: Encoding.UTF8);
            while (!sr.EndOfStream)
            {
                var sor = sr.ReadLine();
                var adatok = sor.Split(" ");
                utasadatok.Add(new eUtazas(
                        byte.Parse(adatok[0]),
                        adatok[1],
                        int.Parse(adatok[2]),
                        adatok[3],
                        adatok[4]
                    ));
            }
            sr.Close();

        }
    }
}