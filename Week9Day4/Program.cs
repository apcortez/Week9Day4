using System;
using System.Collections.Generic;
using System.Linq;

namespace Week9Day4
{
    class Program
    {
        static List<int> listaNumeri = new List<int>();
        static void Main(string[] args)
        {
            Calcolatore calc = new Calcolatore();
            calc.MediaSuperata += QuandoRicevoLaNotifica;
            while (true)
            {
                bool conversioneRiuscita = int.TryParse(Console.ReadLine(), out int num);
                if (conversioneRiuscita)
                {
                    listaNumeri.Add(num);
                    double media = CalcolaMedia(listaNumeri);
                    if(media < 10)
                    {
                        calc.AlSuperamentoDellaMedia(media);
                    }
                }
            
            }
        }

        private static double CalcolaMedia(List<int> listaNumeri)
        {
            return listaNumeri.Sum() / listaNumeri.Count();
        }

        public static void QuandoRicevoLaNotifica(Calcolatore calc, double media)
        {
            Console.WriteLine("Attenzione la media è {media} che è inferiore alla soglia di critica di 10");
        }
        
    }
}
