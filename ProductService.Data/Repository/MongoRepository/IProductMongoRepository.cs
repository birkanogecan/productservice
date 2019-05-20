using ProductService.Data.Entity.MongoEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.Data.Repository.MongoRepository
{
    public interface IProductMongoRepository
    {
        MongoProduct Get(int id);
        MongoProduct Create(MongoProduct MongoProduct);
        void Update(int id, MongoProduct MongoProductIn);
        void Remove(MongoProduct MongoProductIn);
        void Remove(int id);
    }
}
