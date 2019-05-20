using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.Model.QueryModel
{
    public class MongoGetQuery : IRequest<MongoGetQueryResult>
    {
        public int Id { get; set; }
    }
}
