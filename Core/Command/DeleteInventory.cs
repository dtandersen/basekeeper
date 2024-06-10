using Basekeeper.Repository;

namespace Basekeeper.Command;

public record DeleteInventoryCommand(string Item)
{
}

public class DeleteInventoryCommandHandler : CommandHandler<DeleteInventoryCommand>
{
    private InventoryRepository inventoryRepository;

    public DeleteInventoryCommandHandler(InventoryRepository inventoryRepository)
    {
        this.inventoryRepository = inventoryRepository;
    }

    public void Handle(DeleteInventoryCommand command)
    {
        inventoryRepository.Delete(command.Item);
    }
}
