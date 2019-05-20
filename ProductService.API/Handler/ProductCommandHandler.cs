using MediatR;
using ProductService.Domain;
using ProductService.Model.CommandModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductService.API.Handler
{
    public class ProductCommandHandler : IRequestHandler<ProductCreateCommand, BaseCommandResult>
    {
        private readonly IEventStoreDomain _eventStoreDomain;
        public ProductCommandHandler(IEventStoreDomain eventStoreDomain)
        {
            _eventStoreDomain = eventStoreDomain;
        }
        public Task<BaseCommandResult> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_eventStoreDomain.CreateEvent(request));
        }
    }
}
