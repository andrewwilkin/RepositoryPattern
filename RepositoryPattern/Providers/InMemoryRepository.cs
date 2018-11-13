using System;
using System.Collections.Generic;
using System.Text;
using RepositoryPattern.Interfaces;

namespace RepositoryPattern.Providers
{
    public sealed class InMemoryRepository<T> : IRepository<T> where T : IStoreable
    {
        private readonly List<T> _store;

        /// <summary>
        /// Creates a new in memory repository which can store objects that implement the IStoreable interface
        /// This can be done by inheriting from the abstract Storeable class
        /// </summary>
        public InMemoryRepository()
        {
            _store = new List<T>();
        }

        /// <summary>
        /// Get an enumerable collection of items from the repository
        /// As you can run LINQ queries against this
        /// </summary>
        /// <returns>
        /// IEnumerable Collection of T
        /// </returns>
        public IEnumerable<T> All()
        {
            return _store;
        }

        /// <summary>
        /// Remove an item from the repository that matches the 
        /// </summary>
        /// <returns>
        /// void
        /// </returns>
        /// <exception cref="ArgumentNullException">If argument is null</exception>
        /// <remarks>
        /// Does not throw an exception if the id is not present in the repository (by design)
        /// </remarks>
        public void Delete(IComparable id)
        {
            CheckIdNotNull(id);
            _store.RemoveAll(item => item.Id.Equals(id));
        }


        /// <summary>
        /// Find an item that matches an id
        /// </summary>
        /// <returns>
        /// Single item of T (or null)
        /// </returns>
        /// <exception cref="ArgumentNullException">If argument is null</exception>
        /// <remarks>
        /// Does not throw an exception if the id is not present in the repository (by design)
        /// </remarks>
        public T FindById(IComparable id)
        {
            CheckIdNotNull(id);
            return _store.Find(item => item.Id.Equals(id));
        }

        /// <summary>
        /// Save an item to the repository
        /// </summary>
        /// <returns>
        /// void
        /// </returns>
        /// <exception cref="NullReferenceException">If item is null</exception>
        /// <exception cref="ArgumentNullException">If Id property is null</exception>
        /// <remarks>
        /// Does not throw an exception if the id is not present in the repository (by design)
        /// </remarks>
        public void Save(T item)
        {
            CheckIdNotNull(item.Id);
            Delete(item.Id);
            _store.Add(item);
        }

        private void CheckIdNotNull(IComparable id)
        {
            if (id == null)
                throw new ArgumentNullException("Id must be initialized");
        }
    }
}
