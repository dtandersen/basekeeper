using Basekeeper.Entity;
using Basekeeper.Repository;

namespace Basekeeper.Command;

public record CreateInventoryCommand(string Item, float Quantity)
{
}

public class CreateInventoryCommandHandler : CommandHandler<CreateInventoryCommand>
{
    private InventoryRepository inventoryRepository;

    public CreateInventoryCommandHandler(InventoryRepository inventoryRepository)
    {
        this.inventoryRepository = inventoryRepository;
    }

    public void Handle(CreateInventoryCommand command)
    {
        inventoryRepository.Save(new LineItem(Item: command.Item, Quantity: command.Quantity));
    }
}

