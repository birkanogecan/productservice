using MediatR;
using ProductService.Model.ApiModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductService.Model.CommandModel
{
    public class ProductCreateCommand  : IRequest<BaseCommandResult>
    {
        public ProductApiModel productApiModel { get; set; }
    }
}
