using System;
using System.Collections.Generic;
using System.Text;
using ProductService.Data.Repository.MongoRepository;
using ProductService.Model.ApiModel;
using ProductService.Model.QueryModel;

namespace ProductService.Domain
{
    public class MongoDomain : IMongoDomain
    {
        private readonly IProductMongoRepository _productrepository;
        public MongoDomain(IProductMongoRepository productRepository)
        {
            _productrepository = productRepository;
        }
        public MongoGetQueryResult GetProductById(MongoGetQuery mongoGetQuery)
        {
            var product = _productrepository.Get(mongoGetQuery.Id);
            ProductApiModel productApiModel = new ProductApiModel() {
                Id = product.ProductId,
                Brand = product.Brand,
                Name = product.Name,
                Price = product.Price
            };
            return new MongoGetQueryResult() { productApiModel = productApiModel };
        }
    }
}
