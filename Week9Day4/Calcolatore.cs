using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week9Day4
{
    class Calcolatore
    {
        public delegate void GestoreEventi(Calcolatore calc, double media); //puntatore

        public event GestoreEventi MediaSuperata; //evento - chiamato

        public void AlSuperamentoDellaMedia(double media) // chiamante
        {
            if (MediaSuperata != null)
            {
                MediaSuperata(this, media);
            }
        }
    }
}
