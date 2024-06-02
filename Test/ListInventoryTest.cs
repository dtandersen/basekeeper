using Basekeeper.Command;
using Basekeeper.Entity;
using Basekeeper.Repository;
using Basekeeper.Repository.Yaml;

namespace Basekeeper.Tests;

//https://stackoverflow.com/questions/1408175/execute-unit-tests-serially-rather-than-in-parallel
[Collection("Sequential")]
public class ListInventoryTest
{
    private InventoryRepository inventoryRepository;

    public ListInventoryTest()
    {
        inventoryRepository = new YamlInventoryRepository();
        inventoryRepository.Reset();
    }

    [Fact]
    public void Test1()
    {
        var x = new List<LineItem> {
            new LineItem(Item: "Iron", Quantity: 1)
        };
        inventoryRepository.Save(x);

        ListInventoryQueryHandler query = new ListInventoryQueryHandler(inventoryRepository);
        List<LineItem> items = query.Handle(new ListInventoryQuery());
        Assert.That(items, Has.Items(Is.EqualTo(new LineItem(Item: "Iron", Quantity: 1))));
        Assert.That(items, Is.OfLength(1));
    }
}
