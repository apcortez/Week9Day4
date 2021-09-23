using System;
using Week9Day4.Esercizio1.AdoRepository;
using Week9Day4.Esercizio1.Core;
using Week9Day4.Esercizio1.Core.Entities;

namespace Week9Day4.Esercizio1
{
    public class Menu
    {
        private static readonly IBusinessLayer bl = new BusinessLayer(new RepositoryEsalazione(), new RepositoryMisurazioneTemperatura());
        internal static void Start()
        {
            
                bool continuare = true;
                Console.WriteLine("################# BENVENUTO! ################");
                do
                {
                    Console.WriteLine();
                    Console.WriteLine("#############################################");
                    Console.WriteLine("Selezionare un operazione da eseguire:");
                    Console.WriteLine("1 - Inserisci dati");
                    Console.WriteLine("2 - Calcola dati");
                    Console.WriteLine("0 - Per uscire");
                    Console.WriteLine("#############################################");

                    Console.WriteLine();
                    Console.WriteLine("Quale operazione vuoi scegliere?");
                    string scelta = Console.ReadLine();

                    switch (scelta)
                    {
                        case "1":
                            InsertInfo();
                            break;
                        case "2":
                            CalcolaDati();
                            break;
                        case "0":
                            Console.WriteLine("Arrivederci. A presto!");
                            continuare = false;
                            break;
                        default:
                            Console.WriteLine("Scelta sbagliata riprova");
                            break;
                    }
                } while (continuare);
            }

        private static void InsertInfo()
        {
          double newesalazione= ChiediEsalazione();
          double newtemp = ChiediTemperatura();

            bl.InserisciDati(newesalazione, newtemp);
        }

        private static double ChiediTemperatura()
        {
            double temp;
            bool isDouble;
            do
            {
                Console.WriteLine("Inserisci la temperatura: ");

                isDouble = Double.TryParse(Console.ReadLine(), out temp);

            } while (!isDouble);
            return temp;
        }

        private static double ChiediEsalazione()
        {
            double esal;
            bool isDouble;
            do
            {
                Console.WriteLine("Inserisci l'esalazione: ");

                isDouble = Double.TryParse(Console.ReadLine(), out esal);

            } while (!isDouble);
            return esal;
        }

        private static void CalcolaDati()
        {
            bl.EseguiCalcoli();
        }
    }
    
}