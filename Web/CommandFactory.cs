using Basekeeper.Command;
using Basekeeper.Repository;

public class CommandFactory
{
    private readonly InventoryRepository inventoryRepository;
    private readonly OrderRepository orderRepository;
    private readonly RecipeRepository recipeRepository;

    public CommandFactory(
        InventoryRepository inventoryRepository,
        OrderRepository orderRepository,
        RecipeRepository recipeRepository)
    {
        this.inventoryRepository = inventoryRepository;
        this.orderRepository = orderRepository;
        this.recipeRepository = recipeRepository;
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

    public DeleteOrderCommandHandler DeleteOrder()
    {
        return new DeleteOrderCommandHandler(orderRepository);
    }

    public CreateOrderCommandHandler CreateOrder()
    {
        return new CreateOrderCommandHandler(orderRepository, recipeRepository);
    }
}
