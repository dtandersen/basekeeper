using Basekeeper.Command;
using Basekeeper.Entity;
using Basekeeper.Repository;
using Basekeeper.Repository.Yaml;

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

        UpdateInventoryCommandHandler query = new UpdateInventoryCommandHandler(inventoryRepository);
        query.Handle(new UpdateInventoryCommand(new List<LineItem> {
            new LineItem(Item: "Iron", Quantity: 1)
        }));
        List<LineItem> items = inventoryRepository.All();
        Assert.That(items, Has.Items(Is.EqualTo(new LineItem(Item: "Iron", Quantity: 1))));
        Assert.That(items, Is.OfLength(1));
    }
}
