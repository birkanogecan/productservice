using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Model.ApiModel;
using ProductService.Model.CommandModel;
using ProductService.Model.QueryModel;

namespace ProductService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET api/Product
        [HttpGet]
        public ActionResult<IEnumerable<ProductApiModel>> Get([FromQuery(Name = "query")] string query)
        {
            var result = _mediator.Send(new ESGetQuery() { Query = query}).Result;
            return result.productApiModel;
        }

        // GET api/Product/5
        [HttpGet("{id}")]
        public ActionResult<ProductApiModel> Get(int id)
        {
            var result = _mediator.Send(new MongoGetQuery() { Id = id }).Result;
            return result.productApiModel;
        }

        // POST api/Product
        [HttpPost]
        public void Post([FromBody] ProductApiModel model)
        {
            var result = _mediator.Send(new ProductCreateCommand() { productApiModel = model}).Result;
        }

        // PUT api/Product/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/Product/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
