using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week9Day4.Esercizio1.Core.Interfaces
{
    public interface IRepository<T>
    {
        List<T> GetItemsWithOutState();
        void Update(T item);
        void Insert(T item);
    }
}
