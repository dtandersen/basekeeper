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
        public void Post([FromBody] CreateOrderCommand command)
        {
            Console.WriteLine($"Creating {command.Item} with quantity {command.Quantity}");
            var handler = commandFactory.CreateOrder();
            handler.Handle(new CreateOrderCommand(Item: command.Item, Quantity: command.Quantity));
        }

        [HttpDelete("{item}")]
        public void Delete(string item)
        {
            Console.WriteLine($"Deleting {item}");
            var handler = commandFactory.DeleteOrder();
            handler.Handle(new DeleteOrderCommand(Item: item));
        }
    }
}
