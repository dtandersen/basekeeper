using Core;

namespace Basekeeper.Tests;

//https://stackoverflow.com/questions/1408175/execute-unit-tests-serially-rather-than-in-parallel
[Collection("Sequential")]
public class ListInventoryQueryTest
{
    private InventoryRepository inventoryRepository;

    public ListInventoryQueryTest()
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

        ListInventoryQuery query = new ListInventoryQuery(inventoryRepository);
        List<LineItem> items = query.Execute();
        Assert.That(items, Has.Items(Is.EqualTo(new LineItem(Item: "Iron", Quantity: 1))));
        Assert.That(items, Is.OfLength(1));
    }
}
