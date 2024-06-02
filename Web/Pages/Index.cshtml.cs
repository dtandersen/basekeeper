using System.Diagnostics;
using System.Text.Json;
using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly InventoryRepository _emailService;
    public List<LineItem> Items { get; set; }
    public string Json2 { get; set; }

    public IndexModel(ILogger<IndexModel> logger, InventoryRepository emailService)
    {
        _logger = logger;
        _emailService = emailService;
        Items = new List<LineItem>();
    }

    public void OnGet()
    {
        // Console.WriteLine("OnGet");
        ListInventoryQuery x = new ListInventoryQuery(_emailService);
        Items = x.Execute();
        Json2 = JsonSerializer.Serialize(Items);
        Console.WriteLine($"{Json2}");
    }

    // public void OnPost()
    public IActionResult OnPost(FormModel model)
    {
        var handler = new UpdateInventoryCommand(_emailService);
        List<LineItem> lineItems = convertModel(model);
        handler.Execute(lineItems);
        Console.WriteLine($"{model}");

        return RedirectToPage("./Index");
    }

    private List<LineItem> convertModel(FormModel model)
    {
        List<LineItem> lineItems = new List<LineItem>();
        for (int i = 0; i < model.Item.Count; i++)
        {
            if (model.Item[i] != null && model.Quantity[i] != null)
            {
                int qty = (int)model.Quantity[i];
                lineItems.Add(new LineItem(Item: model.Item[i], Quantity: qty));
            }
        }
        return lineItems;
    }
}

public class FormModel
{
    [BindProperty]
    public List<string?> Item { get; set; }

    [BindProperty]
    public List<int?> Quantity { get; set; }

    public override string ToString()
    {
        return $"FormModel {{ Item={String.Join(",", Item)}, Quantity={String.Join(",", Quantity)} }}";
    }
}