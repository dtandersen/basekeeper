using Basekeeper.Command;
using Microsoft.AspNetCore.Mvc;

namespace Basekeeper.Controller
{
    [Route("api/orders")]
    [ApiController]
    public class OrderApiController : ControllerBase
    {
        private readonly CommandFactory commandFactory;

        public OrderApiController(CommandFactory commandFactory)
        {
            this.commandFactory = commandFactory;
        }

        [HttpGet]
        public List<OrderDto> Get()
        {
            var handler = commandFactory.ListOrders();
            return handler.Handle(new ListOrdersQuery());
        }
    }
}
