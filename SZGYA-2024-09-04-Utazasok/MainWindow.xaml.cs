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

            var eUtazasWin = new eUtazasGui();
            eUtazasWin.Show();

            var utasadatok = new List<eUtazas>();
            var sr = new StreamReader("../../../src/utasadat.txt", encoding: Encoding.UTF8);
            while (!sr.EndOfStream)
            {
                utasadatok.Add(new eUtazas(sr.ReadLine()));
            }
            sr.Close();

            lblHarmadik.Content = $"A buszra {utasadatok.Count} utas akart felszállni.";
            var nemSzallhatfel = new List<eUtazas>();
            foreach (var u in utasadatok)
            {
                if (u.Felhasznalhato == 0)
                    nemSzallhatfel.Add(u);
                else if (u.Felhasznalhato == -1 && u.Felszallas.Date > u.Lejarat.Date)
                    nemSzallhatfel.Add(u);
            }
            lblNegyedik.Content = $"A buszra {nemSzallhatfel.Count} utas nem szállhatott fel.";
            var legtobbFelszallo = utasadatok.GroupBy(u => u.Megallo).OrderByDescending(g => g.Count()).FirstOrDefault().ToList();
            txtBlkOtodik.Text = $"A legtöbb utas ({legtobbFelszallo.Count} fő) a {legtobbFelszallo.First().Megallo}. megállóban próbált felszállni";

            lblHatodikEgy.Content = $"Ingyenesen utazók száma: {utasadatok.Count(u => (u.Tipus == "NYP" || u.Tipus == "GYK" || u.Tipus == "RVS") && u.Felhasznalhato == -1 && u.Felszallas.Date <= u.Lejarat.Date)} fő";
            lblHatodikKetto.Content = $"A kedvezményesen utazók száma: {utasadatok.Count(u => (u.Tipus == "TAB" || u.Tipus == "NYB") && u.Felhasznalhato == -1 && u.Felszallas.Date <= u.Lejarat.Date)} fő";

            // 7.
            var lejarok = utasadatok.Where(u => ((u.Lejarat.Date - u.Felszallas.Date).Days <= 3) && u.Felhasznalhato == -1);
            var sw = new StreamWriter("../../../src/figyelmeztetes.txt", false, Encoding.UTF8);
            foreach (var lejaro in lejarok)
            {
                sw.WriteLine($"{lejaro.Azonosito} {lejaro.Lejarat.Year}-{lejaro.Lejarat.Month.ToString("00")}-{lejaro.Lejarat.Day.ToString("00")}");
            }
            sw.Close();
        }
    }
}