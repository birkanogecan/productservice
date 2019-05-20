using ProductService.Model.ApiModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.Model.QueryModel
{
    public class ESGetQueryResult 
    {
        public List<ProductApiModel> productApiModel { get; set; }
    }
}
