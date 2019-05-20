using MediatR;
using ProductService.Domain;
using ProductService.Model.QueryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProductService.API.Handler
{
    public class ProductQueryHandler : IRequestHandler<ESGetQuery, ESGetQueryResult>, IRequestHandler<MongoGetQuery, MongoGetQueryResult>
    {
        private readonly IESDomain _eSDomain;
        private readonly IMongoDomain _mongoDomain;
        public ProductQueryHandler(IESDomain eSDomain, IMongoDomain mongoDomain)
        {
            _eSDomain = eSDomain;
            _mongoDomain = mongoDomain;
        }

        public Task<ESGetQueryResult> Handle(ESGetQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_eSDomain.GetProductsByQuery(request));
        }

        public Task<MongoGetQueryResult> Handle(MongoGetQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_mongoDomain.GetProductById(request));
        }
    }
}
