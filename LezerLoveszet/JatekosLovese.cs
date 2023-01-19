using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LezerLoveszet
{
    internal class JatekosLovese
    {
        public string? Nev { get; set; }
        public float XKoordinata { get; set; }
        public float YKoordinata { get;set; }
        public int LovesSorszam { get; set; }
        
        public JatekosLovese(string Nev, float XKoordinata, float YKoordinata, int LovesSorszam) { 
            this.Nev = Nev;
            this.XKoordinata= XKoordinata;
            this.YKoordinata= YKoordinata;
            this.LovesSorszam= LovesSorszam;
        }

        public double Tavolsag(float CeltablaX, float CeltablaY)
        {
            return Math.Sqrt(Math.Pow(CeltablaX - XKoordinata, 2) + Math.Pow(CeltablaY - YKoordinata, 2));
        }

        public double Pontszam(float CeltablaX, float CeltablaY)
        {
            double Pontszam = Math.Round(10 - this.Tavolsag(CeltablaX, CeltablaY), 2);            
            if (Pontszam <0)
            {
                return 0;
            }
            return Pontszam;
        }
    }

}
