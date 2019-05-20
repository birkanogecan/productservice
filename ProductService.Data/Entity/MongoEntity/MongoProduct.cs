using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.Data.Entity.MongoEntity
{
    public class MongoProduct : MongoBaseEntity
    {
        [BsonElement("ProductId")]
        public int ProductId { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Brand")]
        public string Brand { get; set; }
        [BsonElement("Price")]
        public decimal Price { get; set; }
        [BsonElement("CreatedDate")]
        public DateTime CreatedDate { get; set; }
        [BsonElement("IsDeleted")]
        public bool IsDeleted { get; set; }
    }
}
