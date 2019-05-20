using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.Model.QueryModel
{
    public class ESGetQuery : IRequest<ESGetQueryResult>
    {
        public string Query { get; set; }
    }
}
