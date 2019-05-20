using ProductService.Data.Entity.ESEntity;
using System;
using System.Collections.Generic;

namespace ProductService.Data.Repository.ESRepository
{
    public interface IESRepository<T> where T : ESBaseEntity
    {
        Guid Save(T entity);
        T Get(Guid id);
        IEnumerable<T> Search(string query);
    }
}
