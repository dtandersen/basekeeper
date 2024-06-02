

using Basekeeper.Entity;
using Basekeeper.Repository;

namespace Basekeeper.Command;

public record UpdateInventoryCommand(List<LineItem> LineItems);

public class UpdateInventoryCommandHandler : CommandHandler<UpdateInventoryCommand>
{
    private InventoryRepository inventoryRepository;

    public UpdateInventoryCommandHandler(InventoryRepository inventoryRepository)
    {
        this.inventoryRepository = inventoryRepository;
    }

    public void Handle(UpdateInventoryCommand command)
    {
        inventoryRepository.Save(command.LineItems);
    }
}