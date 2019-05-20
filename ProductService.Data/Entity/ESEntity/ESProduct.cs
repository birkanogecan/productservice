using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.Data.Entity.ESEntity
{
    public class ESProduct : ESBaseEntity
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
