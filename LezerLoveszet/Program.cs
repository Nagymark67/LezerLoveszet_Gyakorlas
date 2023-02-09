using System.Drawing;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;

namespace LezerLoveszet
{
    internal class Program
    {
        static void Main()
        {                        
            JatekosLovese Jatekos;
            List<JatekosLovese> Lovesek = new();
            float CeltablaX=0;
            float CeltablaY=0;

            using StreamReader sr = new(path:"../../../src/lovesek.txt");
            string? sor = sr.ReadLine();
            if (sor is not null)
            {                
                string[] KPsor = sor.Split(";");
                CeltablaX = float.Parse(KPsor[0]);
                CeltablaY = float.Parse(KPsor[1]);
            }
            int Sorszam = 1;
            while (!sr.EndOfStream)
            {
                sor= sr.ReadLine();
                if (sor is not null)
                {
                    string[] sordarabok = sor.Split(";");
                    Jatekos = new JatekosLovese(sordarabok[0], float.Parse(sordarabok[1]), float.Parse(sordarabok[2]), Sorszam++);
                    Lovesek.Add(Jatekos);
                }                
            }            
            OsszLoves(Lovesek);
            LegpontosabbLoves(CeltablaX, CeltablaY, Lovesek);
            NullaPontos(CeltablaX, CeltablaY, Lovesek);
            JatekosokSzama(Lovesek);
            LovesPerJatekos(Lovesek);
            AtlagPontszam(CeltablaX, CeltablaY, Lovesek);
            JatekNyertese(CeltablaX, CeltablaY, Lovesek);
        }
        private static void OsszLoves(List<JatekosLovese> lista)
        {
            Console.WriteLine("5. feladat: Lövések száma: {0} db", lista.Count);
        }
        private static void LegpontosabbLoves(float CeltablaX,float CeltablaY, List<JatekosLovese> lista)
        {
            double MinTavolsag = double.MaxValue;
            JatekosLovese MinJatekosLovese= null;
            foreach (var item in lista)
            {
                double AktTavolsag = item.Tavolsag(CeltablaX, CeltablaY);
                if (AktTavolsag < MinTavolsag)
                {
                    MinTavolsag = AktTavolsag;
                    MinJatekosLovese = item;
                }
            }
            Console.WriteLine("7. feladat: Legpontosabb lövés:\n\t{0}.; {1}; x={2:,0.00}; y={3:0.00}; távolság:{4}", MinJatekosLovese.LovesSorszam, MinJatekosLovese.Nev, MinJatekosLovese.XKoordinata, MinJatekosLovese.YKoordinata, MinTavolsag);
        }
        private static void NullaPontos(float CeltablaX, float CeltablaY, List<JatekosLovese> lista)
        {
            int db = 0;
            foreach (var item in lista) 
            {
                double JatekosPontszam = item.Pontszam(CeltablaX, CeltablaY);
                if (JatekosPontszam<0)
                {
                    db++;
                }
            }
            Console.WriteLine("9. feladat: Nulla pontos lövések száma: {0} db", db);
        }

        private static void JatekosokSzama(List<JatekosLovese> lista)
        {
            List<string> Nevek = new();
            for (int i = 0; i < lista.Count; i++)
            {
                if (!Nevek.Contains(lista[i].Nev))
                {
                    Nevek.Add(lista[i].Nev);
                }
            }
            Console.WriteLine("10. feladat: Játékosok száma: {0}", Nevek.Count);
        }
        
        private static void LovesPerJatekos(List<JatekosLovese> lista)
        {
            Dictionary<string, int> JatekosLovesDb= new Dictionary<string, int>();
            foreach (var item in lista)
            {
                if (!JatekosLovesDb.ContainsKey(item.Nev))
                {
                    JatekosLovesDb.Add(item.Nev, 1);
                } else 
                {
                    JatekosLovesDb[item.Nev]++;
                }
            }
            Console.WriteLine("11. feladat: Lövések száma");
            foreach (var item in JatekosLovesDb.Keys)
            {
                Console.WriteLine("\t{0} - {1} db", item, JatekosLovesDb[item]);
            }
        }

        private static void AtlagPontszam(float CeltablaX, float CeltablaY, List<JatekosLovese> lista)
        {
            //Összes kilistázása debug céljából.
            /*
            foreach (var item in lista)
            {
                Console.WriteLine("{0} - {1}", item.Nev, item.Pontszam(CeltablaX, CeltablaY));
            }
            */
            Dictionary<string, int> JatekosLovesDb = new Dictionary<string, int>();
            foreach (var item in lista)
            {
                if (!JatekosLovesDb.ContainsKey(item.Nev))
                {
                    JatekosLovesDb.Add(item.Nev, 1);
                }
                else
                {
                    JatekosLovesDb[item.Nev]++;
                }
            }
            Dictionary<string, double> JatekosOsszPontszam= new Dictionary<string, double>();
            foreach (var item in lista)
            {
                if (!JatekosOsszPontszam.ContainsKey(item.Nev))
                {
                    JatekosOsszPontszam.Add(item.Nev, item.Pontszam(CeltablaX, CeltablaY));
                }
                else
                {
                    JatekosOsszPontszam[item.Nev]+= item.Pontszam(CeltablaX, CeltablaY);
                }
            }
            Console.WriteLine("12. feladat: Átlagpontszámok");
            foreach (var item in JatekosLovesDb.Keys)
            {
                Console.WriteLine("\t{0} - {1}", item, JatekosOsszPontszam[item] / JatekosLovesDb[item]);
            }
        }

        private static void JatekNyertese(float CeltablaX, float CeltablaY, List<JatekosLovese> lista)
        {
            Dictionary<string, int> JatekosLovesDb = new Dictionary<string, int>();
            foreach (var item in lista)
            {
                if (!JatekosLovesDb.ContainsKey(item.Nev))
                {
                    JatekosLovesDb.Add(item.Nev, 1);
                }
                else
                {
                    JatekosLovesDb[item.Nev]++;
                }
            }
            Dictionary<string, double> JatekosOsszPontszam = new Dictionary<string, double>();
            foreach (var item in lista)
            {
                if (!JatekosOsszPontszam.ContainsKey(item.Nev))
                {
                    JatekosOsszPontszam.Add(item.Nev, item.Pontszam(CeltablaX, CeltablaY));
                }
                else
                {
                    JatekosOsszPontszam[item.Nev] += item.Pontszam(CeltablaX, CeltablaY);
                }
            }
            Dictionary<string, double> JatekosAtlaga= new Dictionary<string, double>();
            foreach (var item in JatekosLovesDb.Keys)
            {
                JatekosAtlaga.Add(item, JatekosOsszPontszam[item] / JatekosLovesDb[item]);                
            }
            double NyertesPont = JatekosAtlaga.Values.Max();
            foreach (var item in JatekosAtlaga)
            {
                if (item.Value == NyertesPont)
                {
                    Console.WriteLine("13. feladat: Nyertes játékos: {0}", item.Key);
                    break;
                }
            }
        }
    }
}