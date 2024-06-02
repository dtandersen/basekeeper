using System.Text.Json;
using Basekeeper.Command;
using Basekeeper.Entity;
using Basekeeper.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly InventoryRepository inventoryRepository;
    public string Json { get; set; }
    private UpdateInventoryCommandHandler updateInventoryCommandHandler;
    private ListInventoryQueryHandler listInventoryQueryHandler;

    public IndexModel(ILogger<IndexModel> logger, InventoryRepository inventoryRepository)
    {
        _logger = logger;
        this.inventoryRepository = inventoryRepository;
        Json = "[]";
        updateInventoryCommandHandler = new UpdateInventoryCommandHandler(inventoryRepository);
        listInventoryQueryHandler = new ListInventoryQueryHandler(inventoryRepository);
    }

    public void OnGet()
    {
        var Items = listInventoryQueryHandler.Handle(new ListInventoryQuery());
        Json = JsonSerializer.Serialize(Items);
        // Console.WriteLine($"{Json}");
    }

    public IActionResult OnPost(InventoryFormModel model)
    {
        UpdateInventoryCommand ModelToCommand(InventoryFormModel model)
        {
            // Console.WriteLine($"convert model {model}");
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

            return new UpdateInventoryCommand(lineItems);
        }

        UpdateInventoryCommand command = ModelToCommand(model);
        updateInventoryCommandHandler.Handle(command);
        // Console.WriteLine($"{model}");

        return RedirectToPage("./Index");
    }
}

public class InventoryFormModel
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
