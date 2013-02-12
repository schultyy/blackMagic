using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace blackMagic.Repositories
{
    public interface IRepository<T>
        where T : class
    {
        void Insert(T item);
        void Update(T item);
        IEnumerable<T> GetAll();
        T GetSpecific(Predicate<T> predicate);
        void Remove(T item);
    }
}
