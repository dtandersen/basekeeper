using Basekeeper.Entity;
using Basekeeper.Repository;

namespace Basekeeper.Command;

public class ListInventoryQuery { }


public class ListInventoryQueryHandler : QueryHandler<ListInventoryQuery, List<LineItem>>
{
    private readonly InventoryRepository inventoryRepository;

    public ListInventoryQueryHandler(InventoryRepository inventoryRepository)
    {
        this.inventoryRepository = inventoryRepository;
    }

    public List<LineItem> Handle(ListInventoryQuery query)
    {
        var items = inventoryRepository.All();

        return items;
    }
}

