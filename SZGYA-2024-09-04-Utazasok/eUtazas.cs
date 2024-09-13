using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace SZGYA_2024_09_04_Utazasok
{
    internal class eUtazas
    {
        public byte Megallo { get; set; }
        public DateTime Felszallas { get; set; }
        public int Azonosito { get; set; }
        public string Tipus { get; set; }
        public DateTime Lejarat { get; set; }
        public int Felhasznalhato { get; set; }

        public eUtazas(string sor)
        {
            string[] adatok = sor.Split(' ');
            Megallo = byte.Parse(adatok[0]);
            try
            {
                Felszallas = DateTime.ParseExact(adatok[1], "yyyyMMdd-hhmm", CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                MessageBox.Show("A forrásfileban megadott egyik dátum helytelen formátummal rendelkezik!", "Hiba!", MessageBoxButton.OK);
            }
            Azonosito = int.Parse(adatok[2]);
            Tipus = adatok[3];
            if (adatok[4].Length > 3)
            {
                this.Felhasznalhato = -1;
                try
                {
                    Lejarat = DateTime.ParseExact(adatok[4], "yyyyMMdd", CultureInfo.InvariantCulture);
                }
                catch (FormatException)
                {
                    MessageBox.Show("A forrásfileban megadott egyik dátum helytelen formátummal rendelkezik!", "Hiba!", MessageBoxButton.OK);
                    throw new Exception("A fájl beolvasása sikertelen volt!");
                }
            }
            else
            {
                Felhasznalhato = int.Parse(adatok[4]);
            }
        }
        public eUtazas(byte megalloSzam, string felszallas, int azonosito, string tipus, string lejarat)
        {
            Megallo = megalloSzam;
            try
            {
                Felszallas = DateTime.ParseExact(felszallas, "yyyyMMdd-hhmm", CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                MessageBox.Show("A forrásfileban megadott egyik dátum helytelen formátummal rendelkezik!", "Hiba!", MessageBoxButton.OK);
            }
            Azonosito = azonosito;
            Tipus = tipus;
            if (lejarat.Length > 3)
            {
                this.Felhasznalhato = -1;
                try
                {
                    this.Lejarat = DateTime.ParseExact(lejarat, "yyyyMMdd", CultureInfo.InvariantCulture);
                }
                catch (FormatException)
                {
                    MessageBox.Show("A forrásfileban megadott egyik dátum helytelen formátummal rendelkezik!", "Hiba!", MessageBoxButton.OK);
                    throw new Exception("A fájl beolvasása sikertelen volt!");
                }
            }
            else
            {
                Felhasznalhato = int.Parse(lejarat);
            }
        }
    }
}
