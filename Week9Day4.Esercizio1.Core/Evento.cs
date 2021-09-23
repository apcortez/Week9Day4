using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week9Day4.Esercizio1.Core.Entities;

namespace Week9Day4.Esercizio1.Core
{
   public  class Evento
    {
        public delegate void ScriviSuFile(Evento evento, Esalazione esalazione, MisurazioneTemperatura mt);

        public event ScriviSuFile MandaLaNotifica;
        public void SeSogliaSuperata(Esalazione esalazione , MisurazioneTemperatura mt)
        {
            if(MandaLaNotifica != null)
            {
                MandaLaNotifica(this, esalazione, mt);
            }
        }

        
    }
}
