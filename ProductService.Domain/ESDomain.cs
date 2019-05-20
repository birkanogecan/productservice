using System;
using System.Collections.Generic;
using System.Text;
using ProductService.Data.Entity.ESEntity;
using ProductService.Data.Repository.ESRepository;
using ProductService.Model.ApiModel;
using ProductService.Model.QueryModel;

namespace ProductService.Domain
{
    public class ESDomain : IESDomain
    {
        private readonly IESRepository<ESProduct> _productrepository;
        public ESDomain(IESRepository<ESProduct> productRepository)
        {
            _productrepository = productRepository;
        }
        public ESGetQueryResult GetProductsByQuery(ESGetQuery eSGetQuery)
        {
            List<ProductApiModel> productApiModels = new List<ProductApiModel>();
            var product = _productrepository.Search(eSGetQuery.Query);
            foreach (var item in product)
            {
                ProductApiModel productApiModel = new ProductApiModel()
                {
                    Id = item.ProductId,
                    Brand = item.Brand,
                    Name = item.Name,
                    Price = item.Price
                };
                productApiModels.Add(productApiModel);
            }
          
            return new ESGetQueryResult() { productApiModel = productApiModels };
        }
    }
}
