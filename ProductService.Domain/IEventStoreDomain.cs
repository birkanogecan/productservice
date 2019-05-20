using ProductService.Model.CommandModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.Domain
{
    public interface IEventStoreDomain
    {
        BaseCommandResult CreateEvent(ProductCreateCommand productCreateCommand);
    }
}
