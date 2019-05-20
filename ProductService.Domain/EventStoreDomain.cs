using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProductService.Domain;
using ProductService.Model.CommandModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ProductService.Domain
{
    public class EventStoreDomain : IEventStoreDomain
    {
        private readonly IConfiguration _configuration;
        public EventStoreDomain(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public BaseCommandResult CreateEvent(ProductCreateCommand productCreateCommand)
        {
            var streamName = _configuration["StreamName"];
            var connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));
            connection.ConnectAsync().Wait();

            var myEvent = new EventData(Guid.NewGuid(), "createProductEvent", false,
                           Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(productCreateCommand)),
                           Encoding.UTF8.GetBytes("created product event from producer"));
            var result = connection.AppendToStreamAsync(streamName, ExpectedVersion.Any, myEvent);

            return new BaseCommandResult() { Id = productCreateCommand.productApiModel.Id.ToString(), Status = result.IsCompletedSuccessfully ? "Created" : "NotCreated"};
        }
    }
}
