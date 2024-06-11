using Basekeeper.Command;
using Microsoft.AspNetCore.Mvc;

namespace Basekeeper.Controller
{
    [Route("api/inventory")]
    [ApiController]
    public class InventoryApiController : ControllerBase
    {
        private readonly CommandFactory commandFactory;

        public InventoryApiController(CommandFactory commandFactory)
        {
            this.commandFactory = commandFactory;
        }

        [HttpGet]
        public IEnumerable<InventoryItemDto> Get()
        {
            var handler = commandFactory.ListInventory();
            return handler.Handle(new ListInventoryQuery());
        }

        [HttpPost]
        public void Post([FromBody] CreateInventoryCommand command)
        {
            Console.WriteLine($"Creating {command.Item} with quantity {command.Quantity}");
            var handler = commandFactory.CreateInventory();
            handler.Handle(command);
        }

        [HttpDelete("{item}")]
        public void Delete(string item)
        {
            Console.WriteLine($"Deleting {item}");
            var handler = commandFactory.DeleteInventory();
            handler.Handle(new DeleteInventoryCommand(item));
        }
    }
}
