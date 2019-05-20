    using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.Data
{
    public static class AppSettings
    {
        public static string ESConnectionString { get => "http://localhost:9200"; }
        public static string MongoConnectionString { get => "mongodb://localhost:27017"; }
    }
}
