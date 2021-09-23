using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week9Day4.Esercizio1.Core.Entities;

namespace Week9Day4.Esercizio1.Core.Interfaces
{
    public interface IRepositoryMisurazioneTemperatura : IRepository<MisurazioneTemperatura>
    {
        List<MisurazioneTemperatura> GetItemsWithOutState();
    }
}
