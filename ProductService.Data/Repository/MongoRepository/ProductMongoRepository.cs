using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ProductService.Data.Entity.MongoEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.Data.Repository.MongoRepository
{
    public class ProductMongoRepository  : IProductMongoRepository
    {
        private readonly IMongoCollection<MongoProduct> _products;
        public ProductMongoRepository()
        {
            var client = new MongoClient(AppSettings.MongoConnectionString);
            var database = client.GetDatabase("productDb");
            _products = database.GetCollection<MongoProduct>("products");

        }
        public MongoProduct Get(int id)
        {
            return _products.Find<MongoProduct>(product => product.ProductId == id).FirstOrDefault();
        }

        public MongoProduct Create(MongoProduct product)
        {
            _products.InsertOne(product);
            return product;
        }

        public void Update(int id, MongoProduct productIn)
        {
            _products.ReplaceOne(product => product.ProductId == id, productIn);
        }

        public void Remove(MongoProduct productIn)
        {
            _products.DeleteOne(product => product.Id == productIn.Id);
        }

        public void Remove(int id)
        {
            _products.DeleteOne(product => product.ProductId == id);
        }

      
    }
}


