using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ktu9_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] file = System.IO.File.ReadAllLines(@"C:\Users\Andrius\Documents\Visual Studio 2017\ktu\ktu9-2\ktu9-2\duomenys.txt");
            string pirma = file[0];
            string[] pradLaikas = pirma.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            int miestuSkaicius = Convert.ToInt32(file[1]);

            Duomenys[] duomenys = gautiDuomenis(file, miestuSkaicius);

            Laikai[] laikai = gautiLaikus(pradLaikas, miestuSkaicius, duomenys);

            spausdintiLaikus(laikai);
        }

        private static void spausdintiLaikus(Laikai[] laikai)
        {
            using (System.IO.StreamWriter failas = new System.IO.StreamWriter(@"C:\Users\Andrius\Documents\Visual Studio 2017\ktu\ktu9-2\ktu9-2\rezultatai.txt"))
            {
                foreach (Laikai laik in laikai)
                {
                    failas.WriteLine(laik.numeris + " mieste - " + laik.valanda + ":" + laik.minute);
                }
            }
        }

        private static Laikai[] gautiLaikus(string[] pradLaikas, int miestuSkaicius, Duomenys[] duomenys)
        {
            Laikai[] laikai = new Laikai[miestuSkaicius];
            int valanda = Convert.ToInt32(pradLaikas[0]);
            int minute = Convert.ToInt32(pradLaikas[1]);
            double laikasNuoPradzios = 0;
            for (int i = 0; i < miestuSkaicius; i++)
            {
                int atstumas = duomenys[i].atstumas;
                int greitis = duomenys[i].greitis;

                laikasNuoPradzios = laikasNuoPradzios + Convert.ToDouble(atstumas) / Convert.ToDouble(greitis);
                int val = Convert.ToInt32(laikasNuoPradzios);
                int min = Convert.ToInt32((laikasNuoPradzios - (double)val) * 60);
                int minute1 = (minute + min) % 60;
                int valanda1 = (valanda + val + (minute + min) / 60) % 24;
                laikai[i] = new Laikai(i + 1, valanda1, minute1);
            }
            return laikai;
        }

        public static Duomenys[] gautiDuomenis(string[] file, int miestuSkaicius)
        {
            Duomenys[] duomenys = new Duomenys[miestuSkaicius];
            for (int i = 0; i < miestuSkaicius; i++)
            {
                string eilute = file[i + 2];
                string[] laikas = eilute.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                int atstumas = Convert.ToInt32(laikas[0]);
                int greitis = Convert.ToInt32(laikas[1]);
                duomenys[i] = new Duomenys(atstumas, greitis);
            }
            return duomenys;
        }
    }
}
