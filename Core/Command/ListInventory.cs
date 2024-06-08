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

        var components = orders.SelectMany(order => order.Components).ToList();
        //group the sum of components by item
        var componentsum = components.GroupBy(component => component.Item).Select(group => new { Item = group.Key, Quantity = group.Sum(component => component.Quantity) }).ToList();
        // var items = inventory.Select(item =>
        //     new InventoryItemDto(item.Item, item.Quantity, item.Quantity - components.Where(component => component.Item == item.Item).Sum(component => component.Quantity)))
        //     .ToList();

        var items2 = inventory.Select(item =>
            new InventoryItemDto(item.Item, item.Quantity, item.Quantity - componentsum.Where(order => order.Item == item.Item).Sum(order => order.Quantity)))
            .ToList();


        // add components that are not in inventory
        var missing = componentsum.Where(order => !items2.Any(item => item.Item == order.Item)).Select(order => new InventoryItemDto(order.Item, 0, -order.Quantity)).ToList();

        //combine the two lists
        return items2.Concat(missing).ToList();

        // return items2.AppendRange(missing).ToList();
    }
}

public record InventoryItemDto(string Item, float Quantity, float Available);
