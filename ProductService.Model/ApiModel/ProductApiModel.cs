using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.Model.ApiModel
{
    public class ProductApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
    }
}
