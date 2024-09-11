using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public eUtazas(byte megalloSzam, string felszallas, int azonosito, string tipus, string lejarat)
        {
            Megallo = megalloSzam;
            //Felszallas = new DateTime(
            //  int.Parse(felszallas.Substring(0, 4)), //év
            //  int.Parse(felszallas.Substring(4, 2)), // hónap
            //  int.Parse(felszallas.Substring(6, 2)), // nap
            //  int.Parse(felszallas.Substring(9, 2)), // óra
            //  int.Parse(felszallas.Substring(11, 2)), //perc
            //  0);
            try
            {
                Felszallas = DateTime.ParseExact(felszallas, "yyyyMMdd-hhmm", CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {

            }
            Azonosito = azonosito;
            Tipus = tipus;
            if (lejarat.Length > 3)
            {
                this.Felhasznalhato = -1;
                this.Lejarat = new DateTime(
                  int.Parse(lejarat.Substring(0, 4)), //év
                  int.Parse(lejarat.Substring(4, 2)), // hónap
                  int.Parse(lejarat.Substring(6, 2))); // nap
            }
            else
            {
                Felhasznalhato = int.Parse(lejarat);
            }
        }
    }
}
