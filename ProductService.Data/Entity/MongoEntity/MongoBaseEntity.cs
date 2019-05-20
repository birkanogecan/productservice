using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.Data.Entity.MongoEntity
{
    public abstract class MongoBaseEntity
    {
        public ObjectId Id { get; set; }
    }
}
