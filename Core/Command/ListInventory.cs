using Basekeeper.Entity;
using Basekeeper.Repository;

namespace Basekeeper.Command;

public class ListInventoryQuery { }


public class ListInventoryQueryHandler : QueryHandler<ListInventoryQuery, List<InventoryItemDto>>
{
    private readonly InventoryRepository inventoryRepository;
    private readonly OrderRepository orderRepository;

    public ListInventoryQueryHandler(InventoryRepository inventoryRepository, OrderRepository orderRepository)
    {
        this.inventoryRepository = inventoryRepository;
        this.orderRepository = orderRepository;
    }

    public List<InventoryItemDto> Handle(ListInventoryQuery query)
    {
        var orders = orderRepository.All();
        var inventory = inventoryRepository.All();
        var items2 = inventory.Select(item =>
            new InventoryItemDto(item.Item, item.Quantity, item.Quantity - orders.Where(order => order.Product == item.Item).Sum(order => order.Quantity)))
            .ToList();

        return items2;
    }
}

public record InventoryItemDto(string Item, int Quantity, int Available);
