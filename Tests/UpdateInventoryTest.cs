using Core;

namespace Basekeeper.Tests;

[Collection("Sequential")]
public class UpdateInventoryTest
{
    private InventoryRepository inventoryRepository;

    public UpdateInventoryTest()
    {
        inventoryRepository = new YamlInventoryRepository();
        inventoryRepository.Reset();
    }

    [Fact]
    public void Test1()
    {
        inventoryRepository.Save(new List<LineItem> {
            new LineItem(Item: "Iron", Quantity: 1)
        });

        UpdateInventoryCommand query = new UpdateInventoryCommand(inventoryRepository);
        query.Execute(new List<LineItem> {
            new LineItem(Item: "Iron", Quantity: 1)
        });
        List<LineItem> items = inventoryRepository.All();
        Assert.That(items, Has.Items(Is.EqualTo(new LineItem(Item: "Iron", Quantity: 1))));
        Assert.That(items, Is.OfLength(1));
    }
}
