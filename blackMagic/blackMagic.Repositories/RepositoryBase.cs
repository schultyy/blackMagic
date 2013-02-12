using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Outlook;

namespace blackMagic.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T>
        where T : class
    {
        protected Application application;

        protected RepositoryBase(Application application)
        {
            this.application = application;
        }

        public abstract void Insert(T item);
        public abstract void Update(T item);
        public abstract IEnumerable<T> GetAll();
        public abstract T GetSpecific(Predicate<T> predicate);
        public abstract void Remove(T item);
    }
}
