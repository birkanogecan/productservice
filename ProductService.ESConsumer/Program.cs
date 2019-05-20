using ESProductService.Data.Repository.ESRepository;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProductService.Data.Entity.ESEntity;
using ProductService.Data.Repository.ESRepository;
using ProductService.Model.CommandModel;
using System;
using System.Net;
using System.Text;

namespace ProductService.ESConsumer
{
    class Program
    {
        private static ServiceProvider ServiceProvider;
        static void Main(string[] args)
        {
            //DI
            ServiceProvider = new ServiceCollection()
            .AddScoped<IESRepository<ESProduct>, ProductESRepository>()
            .BuildServiceProvider();
            //DI

            var connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));
            connection.ConnectAsync().Wait();
            UserCredentials userCredentials = new UserCredentials("admin", "changeit");
            PersistentSubscriptionSettings settings = PersistentSubscriptionSettings.Create().DoNotResolveLinkTos().StartFromCurrent();
            connection.SubscribeToStreamAsync("product-stream", true, EventAppeared, SubscriptionDropped, userCredentials);
            Console.WriteLine("ES Consumer Listening...");
            Console.ReadLine();
        }

        static void SubscriptionDropped(EventStoreSubscription eventStoreSubscription, SubscriptionDropReason subscriptionDropReason, Exception ex)
        {

        }
        static void EventAppeared(EventStoreSubscription eventStoreSubscription, ResolvedEvent resolvedEvent)
        {
            var data = JsonConvert.DeserializeObject<ProductCreateCommand>(Encoding.ASCII.GetString(resolvedEvent.Event.Data));
            Console.WriteLine("ES Product Recieved Id:" + data.productApiModel.Id);

            var repository = ServiceProvider.GetService<IESRepository<ESProduct>>();
            Guid createdProductId = repository.Save(new ESProduct()
            {
                Id = new Guid(),
                ProductId = data.productApiModel.Id,
                Brand = data.productApiModel.Brand,
                Name = data.productApiModel.Name,
                Price = data.productApiModel.Price,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            });
            Console.WriteLine("ES Product Saved Id:" + createdProductId);
        }

    }
}
