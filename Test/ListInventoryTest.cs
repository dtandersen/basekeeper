using Basekeeper.Command;
using Basekeeper.Entity;
using Basekeeper.Repository;
using Basekeeper.Repository.Yaml;
using TelemRec;
using Xunit.Abstractions;

namespace Basekeeper.Tests;

//https://stackoverflow.com/questions/1408175/execute-unit-tests-serially-rather-than-in-parallel
[Collection("Sequential")]
public class ListInventoryTest
{
    private InventoryRepository inventoryRepository;
    private OrderRepository orderRepository;

    public ListInventoryTest(ITestOutputHelper output)
    {
        XunitLogger.Init(output);
        inventoryRepository = new YamlInventoryRepository();
        orderRepository = new YamlOrderRepository();

        inventoryRepository.Reset();
        orderRepository.Reset();
    }

    [Fact]
    public void Test1()
    {
        inventoryRepository.Save(new List<LineItem> {
            new LineItem(Item: "Iron", Quantity: 1)
        });

        ListInventoryQueryHandler query = new ListInventoryQueryHandler(inventoryRepository, orderRepository);
        List<InventoryItemDto> items = query.Handle(new ListInventoryQuery());
        Assert.That(items, Is.OfLength(1));
        Assert.That(items, Has.Items(Is.EqualTo(new InventoryItemDto(Item: "Iron", Quantity: 1, Available: 1))));
    }

    [Fact]
    public void CalculatesAvailable()
    {
        orderRepository.ReplaceAll(new List<Order> {
            new Order(Item: "Iron", Quantity: 1, Components: new List<LineItem>() {
                new LineItem(Item: "Iron", Quantity: 1)
            })
        });
        inventoryRepository.Save(new List<LineItem> {
            new LineItem(Item: "Iron", Quantity: 1)
        });

        ListInventoryQueryHandler query = new ListInventoryQueryHandler(inventoryRepository, orderRepository);
        List<InventoryItemDto> items = query.Handle(new ListInventoryQuery());
        Assert.That(items, Has.Items(Is.EqualTo(new InventoryItemDto(Item: "Iron", Quantity: 1, Available: 0))));
        Assert.That(items, Is.OfLength(1));
    }

    [Fact]
    public void ShowMissingItems()
    {
        orderRepository.ReplaceAll(new List<Order> {
            new Order(Item: "Iron", Quantity: 1, Components: new List<LineItem>() {
                new LineItem(Item: "Iron", Quantity: 1)
            })
        });

        ListInventoryQueryHandler query = new ListInventoryQueryHandler(inventoryRepository, orderRepository);
        List<InventoryItemDto> items = query.Handle(new ListInventoryQuery());
        Assert.That(items, Is.OfLength(1));
        Assert.That(items, Has.Items(Is.EqualTo(new InventoryItemDto(Item: "Iron", Quantity: 0, Available: -1))));
    }
}
