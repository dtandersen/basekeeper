using Basekeeper.Command;
using Basekeeper.Repository;

public class CommandFactory
{
    private readonly InventoryRepository inventoryRepository;
    private readonly OrderRepository orderRepository;

    public CommandFactory(
        InventoryRepository inventoryRepository,
        OrderRepository orderRepository)
    {
        this.inventoryRepository = inventoryRepository;
        this.orderRepository = orderRepository;
    }

    public ListOrdersQueryHandler ListOrders()
    {
        return new ListOrdersQueryHandler(orderRepository);
    }

    public ListInventoryQueryHandler ListInventory()
    {
        return new ListInventoryQueryHandler(inventoryRepository, orderRepository);
    }

    public DeleteInventoryCommandHandler DeleteInventory()
    {
        return new DeleteInventoryCommandHandler(inventoryRepository);
    }

    internal CreateInventoryCommandHandler CreateInventory()
    {
        return new CreateInventoryCommandHandler(inventoryRepository);
    }
}
