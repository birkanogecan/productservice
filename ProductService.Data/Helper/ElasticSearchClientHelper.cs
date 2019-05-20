using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.Data.Helper
{
    public class ElasticSearchClientHelper
    {
        private readonly string _connectionString;
        public ElasticSearchClientHelper(string connectionString)
        {
            _connectionString = connectionString;
        }
        public ElasticClient CreateElasticClient()
        {
            var node = new SingleNodeConnectionPool(new Uri(_connectionString));
            var settings = new ConnectionSettings(node);
            return new ElasticClient(settings);
        }

        public void CheckIndex<T>(ElasticClient client, string indexName) where T : class
        {
            var response = client.IndexExists(indexName);
            if (!response.Exists)
            {
                client.CreateIndex(indexName, index =>
                   index.Mappings(ms =>
                       ms.Map<T>(x => x.AutoMap())));
            }
        }
    }
}
