using System.Text.Json;
using Basekeeper.Command;
using Basekeeper.Entity;
using Basekeeper.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages;

public class OrdersModel : PageModel
{
    private readonly ILogger<InventoryModel> _logger;
    // private readonly InventoryRepository inventoryRepository;
    // private readonly OrderRepository orderRepository;
    public string Json { get; set; }
    private OrderItemsCommandHandler updateInventoryCommandHandler;
    private ListOrdersQueryHandler listOrdersQueryHandler;
    private CreateOrderCommandHandler createOrderCommandHandler;

    public OrdersModel(ILogger<InventoryModel> logger, InventoryRepository inventoryRepository, OrderRepository orderRepository, RecipeRepository recipeRepository)
    {
        _logger = logger;
        Json = "[]";
        updateInventoryCommandHandler = new OrderItemsCommandHandler(orderRepository);
        listOrdersQueryHandler = new ListOrdersQueryHandler(orderRepository);
        createOrderCommandHandler = new CreateOrderCommandHandler(orderRepository, recipeRepository);
    }

    public void OnGet()
    {
        var Items = listOrdersQueryHandler.Handle(new ListOrdersQuery());
        Json = JsonSerializer.Serialize(Items);
    }

    public IActionResult OnPost(OrderFormModel model)
    {
        CreateOrderCommand ModelToCommand(OrderFormModel model)
        {
            Console.WriteLine($"convert model {model}");
            return new CreateOrderCommand(Item: model.Item, Quantity: Int32.Parse(model.Quantity));
        }

        var command = ModelToCommand(model);
        createOrderCommandHandler.Handle(command);

        return RedirectToPage("./Orders");
    }
}

public class OrdersFormModel
{
    [BindProperty]
    public List<string> Item { get; set; } = new List<string>();

    [BindProperty]
    public List<string> Quantity { get; set; } = new List<string>();

    public override string ToString()
    {
        return $"FormModel {{ Item={String.Join(",", Item)}, Quantity={String.Join(",", Quantity)} }}";
    }
}

public class OrderFormModel
{
    [BindProperty]
    public string Item { get; set; }

    [BindProperty]
    public string Quantity { get; set; }

    public override string ToString()
    {
        return $"FormModel {{ Item={String.Join(",", Item)}, Quantity={String.Join(",", Quantity)} }}";
    }
}
