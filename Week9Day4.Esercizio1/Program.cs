using System;
using Week9Day4.Esercizio1.AdoRepository;
using Week9Day4.Esercizio1.Core;
using Week9Day4.Esercizio1.Core.Interfaces;

namespace Week9Day4.Esercizio1
{
    class Program
    {
      
        static void Main(string[] args)
        {
            try 
            {
                //bl.EseguiCalcoli(); 
                Menu.Start();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
