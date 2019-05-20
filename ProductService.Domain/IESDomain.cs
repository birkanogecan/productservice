using ProductService.Model.ApiModel;
using ProductService.Model.QueryModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.Domain
{
    public interface IESDomain
    {
        ESGetQueryResult GetProductsByQuery(ESGetQuery eSGetQuery);
    }
}
