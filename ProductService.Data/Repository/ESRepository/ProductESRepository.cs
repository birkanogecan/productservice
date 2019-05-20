using Microsoft.Extensions.Configuration;
using Nest;
using ProductService.Data;
using ProductService.Data.Entity.ESEntity;
using ProductService.Data.Helper;
using ProductService.Data.Repository.ESRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESProductService.Data.Repository.ESRepository
{
    public class ProductESRepository : IESRepository<ESProduct>
    {
        private readonly ElasticClient _elasticClient;
        private readonly ElasticSearchClientHelper _elasticClientHelper;
        private readonly string _indexName;

        public ProductESRepository()
        {
            _elasticClient = new ElasticSearchClientHelper(AppSettings.ESConnectionString).CreateElasticClient();
            _elasticClientHelper = new ElasticSearchClientHelper(AppSettings.ESConnectionString);
            _indexName = "products-index";
        }

        public ESProduct Get(Guid id)
        {
            var result = _elasticClient.Get<ESProduct>(id.ToString(), idx => idx.Index(_indexName));
            if (!result.IsValid)
            {
                throw new Exception(result.OriginalException.Message);
            }
            return result.Source;
        }

        public Guid Save(ESProduct entity)
        {
            _elasticClientHelper.CheckIndex<ESProduct>(_elasticClient, _indexName);

            entity.Id = Guid.NewGuid();
            var result = _elasticClient.Index(entity, idx => idx.Index(_indexName));
            if (!result.IsValid)
            {
                throw new Exception(result.OriginalException.Message);
            }
            return entity.Id;
        }

        public IEnumerable<ESProduct> Search(string query)
        {
            var result = _elasticClient.Search<ESProduct>(s => s
                                       .Index(_indexName)
                                        .Query(q => q
                                        .Regexp(c => c
                                        .Name("named_query")
                                        .Boost(1.1)
                                        .Field(p => p.Name)
                                        .Value(".*" + query + ".*")
                                        )));


            if (!result.IsValid)
            {
                throw new Exception(result.OriginalException.Message);
            }

            return result.Documents;
        }


    }
}
