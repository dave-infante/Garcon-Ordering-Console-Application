using Garcon.Business.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System;
using Garcon.Business.Provider.Interface;

namespace Garcon.Business.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private IProvider<T> _provider;
        private ICollection<T> _entity;

        protected ICollection<T> entity
        {
            get => this._entity;
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException("Entity value cannot be null.");
                }

                this._entity = value;
            }
        }

        public Repository(IProvider<T> provider)
        {
            if (provider is null)
            {
                throw new ArgumentNullException("Provider cannot be null.");
            }

            this._provider = provider;
            entity = this._provider.GetEntityFromSource();
        }

        public virtual ICollection<T> GetEntityList()
        {
            entity = this._provider.GetEntityFromSource();
            return entity;
        }

        public virtual int GetEntityCount()
            => GetEntityList().Count();

        public virtual void AddEntity(T entity)
            => this.entity.Add(entity);

        public abstract void UpdateEntity(T Entity);

        public void SaveEntity()
            => this._provider.SaveChanges(entity.ToList());
    }
}
