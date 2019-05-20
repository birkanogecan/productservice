using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProductService.Data.Entity.MongoEntity;
using ProductService.Data.Repository.MongoRepository;
using ProductService.Model.CommandModel;
using System;
using System.Net;
using System.Text;

namespace ProductService.MongoConsumer
{
    class Program
    {
        private static ServiceProvider ServiceProvider;
        public static void Main(string[] args)
        {
            //DI
            ServiceProvider = new ServiceCollection()
            .AddScoped<IProductMongoRepository, ProductMongoRepository>()
            .BuildServiceProvider();
            //DI

            var connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));
            connection.ConnectAsync().Wait();
            UserCredentials userCredentials = new UserCredentials("admin", "changeit");
            PersistentSubscriptionSettings settings = PersistentSubscriptionSettings.Create().DoNotResolveLinkTos().StartFromCurrent();
            //var _result = connection.CreatePersistentSubscriptionAsync("test-stream", "agroup", settings, userCredentials);
            connection.SubscribeToStreamAsync("product-stream", true, EventAppeared, SubscriptionDropped, userCredentials);
            Console.WriteLine("Mongo Consumer Listening...");
            Console.ReadLine();
        }
        static void SubscriptionDropped(EventStoreSubscription eventStoreSubscription, SubscriptionDropReason subscriptionDropReason, Exception ex)
        {

        }
        static void EventAppeared(EventStoreSubscription eventStoreSubscription, ResolvedEvent resolvedEvent)
        {
            var data = JsonConvert.DeserializeObject<ProductCreateCommand>(Encoding.ASCII.GetString(resolvedEvent.Event.Data));
            Console.WriteLine("Mongo Product Recieved Id:" + data.productApiModel.Id);
            try
            {
                var repository = ServiceProvider.GetService<IProductMongoRepository>();
                MongoProduct createdProduct = repository.Create(new MongoProduct()
                {
                    ProductId = data.productApiModel.Id,
                    Brand = data.productApiModel.Brand,
                    Name = data.productApiModel.Name,
                    Price = data.productApiModel.Price,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                });

                Console.WriteLine("Mongo Product Saved Id:" + createdProduct.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Mongo Product Save Error: " + ex.Message);
            }


           
        }
    }
}
