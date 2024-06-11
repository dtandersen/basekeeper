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

        [HttpPost]
        public void Post([FromBody] CreateOrderCommand order)
        {
            var handler = commandFactory.CreateOrder();
            handler.Handle(new CreateOrderCommand(Item: order.Item, Quantity: order.Quantity));
        }

        [HttpDelete]
        public void Delete(string item)
        {
            var handler = commandFactory.DeleteOrder();
            handler.Handle(new DeleteOrderCommand(Item: item));
        }
    }
}
