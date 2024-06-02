namespace Core;

public interface Command
{
    void Execute();
}

public class ListInventoryQuery
{
    private readonly InventoryRepository inventoryRepository;

    public ListInventoryQuery(InventoryRepository inventoryRepository)
    {
        this.inventoryRepository = inventoryRepository;
    }

    public List<LineItem> Execute()
    {
        // List<LineItem> lineItems = new List<LineItem>();
        // var p = new LineItem(Item: "Iron", Quantity: 1);
        // lineItems.Add(p);
        var items = inventoryRepository.All();
        // foreach (var item in items)
        // {
        //     Console.WriteLine(item.ToString());
        // }
        return items;
        // return lineItems;
    }
}

public record LineItem(String Item, int Quantity)
{
    public LineItem() : this("", 0)
    {
    }
    public override string ToString() => $"LineItem {{ Item={Item}, Quantity={Quantity} }}";
};
