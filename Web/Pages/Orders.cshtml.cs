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

    public OrdersModel(ILogger<InventoryModel> logger, InventoryRepository inventoryRepository, OrderRepository orderRepository)
    {
        _logger = logger;
        // this.inventoryRepository = inventoryRepository;
        Json = "[]";
        updateInventoryCommandHandler = new OrderItemsCommandHandler(orderRepository);
        listOrdersQueryHandler = new ListOrdersQueryHandler(orderRepository);
    }

    public void OnGet()
    {
        var Items = listOrdersQueryHandler.Handle(new ListOrdersQuery());
        Json = JsonSerializer.Serialize(Items);
        // Console.WriteLine($"{Json}");
    }

    public IActionResult OnPost(OrdersFormModel model)
    {
        OrderItemsCommand ModelToCommand(OrdersFormModel model)
        {
            Console.WriteLine($"convert model {model}");
            List<LineItem> lineItems = new List<LineItem>();
            var items = model.Item;
            for (int i = 0; i < items.Count; i++)
            {
                var thisitem = items[i];
                if (thisitem != null)
                {
                    int qty = model.Quantity == null ? 0 : Int32.Parse(model.Quantity[i] ?? "0");
                    lineItems.Add(new LineItem(Item: thisitem, Quantity: qty));
                }
            }
            var orders = lineItems.Select(x => new Order(Item: x.Item, Quantity: x.Quantity, Components: new List<LineItem>())).ToList();
            return new OrderItemsCommand(orders);
        }

        var command = ModelToCommand(model);
        updateInventoryCommandHandler.Handle(command);
        // Console.WriteLine($"{model}");

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
