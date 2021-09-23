using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Week9Day4.Esercizio1.Core.Entities;
using Week9Day4.Esercizio1.Core.Interfaces;

namespace Week9Day4.Esercizio1.Core
{
    public class BusinessLayer : IBusinessLayer
    {
        private readonly IRepositoryEsalazione esalazioneRepo;
        private readonly IRepositoryMisurazioneTemperatura misurazioneTempRepo;

        public BusinessLayer(IRepositoryEsalazione esalazioneRepository, IRepositoryMisurazioneTemperatura misurazioneTemperaturaRepository)
        {
            esalazioneRepo = esalazioneRepository;
            misurazioneTempRepo = misurazioneTemperaturaRepository;
        }

        public void EseguiCalcoli()
        {
            List<Esalazione> esalazioni = new List<Esalazione>();
            List<MisurazioneTemperatura> temperature = new List<MisurazioneTemperatura>();
            try {
                 esalazioni = esalazioneRepo.GetItemsWithOutState();
                temperature = misurazioneTempRepo.GetItemsWithOutState();
           
            
            if(esalazioni.Count()>0 && temperature.Count() > 0)
            {
                foreach(var esalazione in esalazioni)
                {
                    if(esalazione.ConcentrazionePpm > 10)
                    {
                        esalazione.Stato = true;
                    }
                    else
                    {
                        esalazione.Stato = false;
                    }

                    MisurazioneTemperatura mt = temperature.Where(t => t.DataMisurazione == esalazione.DataMisurazione && t.OraMisurazione == esalazione.OraMisurazione).FirstOrDefault();
                    
                    if(mt.Temperatura > 30)
                    {
                        mt.Stato = true;
                    }
                    else
                    {
                        mt.Stato = false;
                    }

                    esalazioneRepo.Update(esalazione);
                    misurazioneTempRepo.Update(mt);

                    Evento evento = new Evento();
                    evento.MandaLaNotifica += ScriviSuFile;
                    if (esalazione.Stato == true || mt.Stato == true)
                    {
                        evento.SeSogliaSuperata(esalazione, mt);
                    }
                
                }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InserisciDati(double esalazione, double mt)
        {
            Esalazione newEsalazione = new Esalazione();
            MisurazioneTemperatura newTemp = new MisurazioneTemperatura();

            DateTime dt = DateTime.Now;
            TimeSpan ts = new TimeSpan(dt.Hour, dt.Minute, dt.Second);
            newEsalazione.DataMisurazione = dt;
            newEsalazione.OraMisurazione = ts;
            newEsalazione.ConcentrazionePpm = esalazione;

            newTemp.DataMisurazione = dt;
            newTemp.OraMisurazione = ts;
            newTemp.Temperatura = mt;

            esalazioneRepo.Insert(newEsalazione);
            misurazioneTempRepo.Insert(newTemp);
        }

        private void ScriviSuFile(Evento evento, Esalazione esalazione, MisurazioneTemperatura mt)
        {
            string path = @"C:\Users\angelica.cortez\source\repos\Week9Day4\SuperamentoSoglia.txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine($"{esalazione.DataMisurazione}, {esalazione.OraMisurazione} - Temperatura: {mt.Temperatura} °C - Esalazione: {esalazione.ConcentrazionePpm} ppm");
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
