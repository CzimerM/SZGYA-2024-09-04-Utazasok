using System;
using System.Collections.Generic;
using System.IO;
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
        public static readonly string[] Tipusok = { "FEB", "TAB", "NYB", "NYP", "RVS", "GYK" };

        public eUtazasGui()
        {
            InitializeComponent();

            //sorszám init
            cmbxSorszam.Items.Add("Válasszon megállót!");
            for (int i = 0; i <= 29; i++) cmbxSorszam.Items.Add($"{i}");
            cmbxSorszam.SelectedIndex = 0;

            //datepicker init
            dtpckrFelszallas.SelectedDate = DateTime.Now;

            //bérlettípus init
            cmbxBerletTipus.Items.Add("Válasszon típust!");
            foreach (var tipus in Tipusok) cmbxBerletTipus.Items.Add(tipus);
            cmbxBerletTipus.SelectedIndex = 0;

            txbKartyaAzon.MaxLength = 7;
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

        private void txbKartyaAzon_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblAzonDb.Content = $"{txbKartyaAzon.Text.Length}db";
        }

        private void sldrJegyDb_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblJegyDbDisp.Content = $"{sldrJegyDb.Value}db";
        }

        private void btnAdatokEll_Click(object sender, RoutedEventArgs e)
        {
            //megálló
            if (cmbxSorszam.SelectedIndex == 0)
            {
                MessageBox.Show("Nem választott megállót!", "Hiba!", MessageBoxButton.OK);
                return;
            }
            //felszállás idő
            int ora;
            int perc;
            try
            {
                if (txbFelszallasIdo.Text[2] != ':') throw new Exception();
                ora = int.Parse(txbFelszallasIdo.Text.Substring(0, 2));
                perc = int.Parse(txbFelszallasIdo.Text.Substring(3, 2));
                if (perc <= 0 || perc >= 60) throw new Exception();
                if (ora < 0 || ora >= 24) throw new Exception(); 
            }
            catch
            {
                MessageBox.Show("Nem megfelelő a bemeneti karakterlánc formátuma.", "Hiba!", MessageBoxButton.OK);
                return;
            }
            
            //kártyaazonosító
            int azon;
            if (txbKartyaAzon.Text.Length != 7)
            {
                MessageBox.Show("A kártya azonosítója nem hét karakter hosszú!", "Hiba!", MessageBoxButton.OK);
                return;
            }
            else if (!int.TryParse(txbKartyaAzon.Text, out azon))
            {
                MessageBox.Show("A kártya azonosítója nem pozitív egész szám", "Hiba!", MessageBoxButton.OK);
                return;
            }

            if ((bool)rbtnBerlet.IsChecked)
            {
                //bérlet típus
                if (cmbxBerletTipus.SelectedIndex == 0)
                {
                    MessageBox.Show("Nem adta meg a bérlet típusát!", "Hiba!", MessageBoxButton.OK);
                    return;
                }
                //bérlet érvényesség
                if (dtpckrBerletErvenyesseg.SelectedDate == null)
                {
                    MessageBox.Show("Nem adta meg a bérlet érvényességi idejét!", "Hiba!", MessageBoxButton.OK);
                    return;
                }
            }

            StreamWriter sw = new StreamWriter("../../../src/utasadat.txt", true, Encoding.UTF8);

            sw.WriteLine($"{cmbxSorszam.SelectedItem} {dtpckrFelszallas.SelectedDate.Value.ToString("yyyyMMdd")}-{ora.ToString("00")}{perc.ToString("00")} {txbKartyaAzon.Text} {(grpbxJegy.Visibility == Visibility.Visible ? "JGY" : cmbxBerletTipus.SelectedItem)} {(grpbxJegy.Visibility == Visibility.Visible ? sldrJegyDb.Value : dtpckrBerletErvenyesseg.SelectedDate.Value.ToString("yyyyMMdd"))}");

            sw.Close();
            MessageBox.Show("A felszállás tárolása sikeres volt!", "EUtazás 2020", MessageBoxButton.OK);

            cmbxSorszam.SelectedIndex = 0;
            dtpckrFelszallas.SelectedDate = DateTime.Now;
            txbFelszallasIdo.Text = "";
            txbKartyaAzon.Text = "";
            cmbxBerletTipus.SelectedIndex = 0;
            dtpckrBerletErvenyesseg.SelectedDate = null;
            sldrJegyDb.Value = 0;
        }
    }
}
