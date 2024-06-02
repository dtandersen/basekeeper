using Basekeeper.Command;
using Basekeeper.Entity;
using Basekeeper.Repository;
using Basekeeper.Repository.Yaml;

namespace Basekeeper.Tests;

//https://stackoverflow.com/questions/1408175/execute-unit-tests-serially-rather-than-in-parallel
[Collection("Sequential")]
public class OrderItemsTest
{
    private OrderRepository requisitionRepository;

    public OrderItemsTest()
    {
        requisitionRepository = new YamlOrderRepository();
        requisitionRepository.Reset();
    }

    [Fact]
    public void Test1()
    {
        var command = new OrderItemsCommand(new List<LineItem> { new LineItem(Item: "Iron", Quantity: 1) });
        var handler = new OrderItemsCommandHandler(requisitionRepository);
        handler.Handle(command);

        // ListInventoryQueryHandler query = new ListInventoryQueryHandler(inventoryRepository);
        // List<LineItem> items = query.Handle(new ListInventoryQuery());
        var items = requisitionRepository.All();
        Assert.That(items, Is.OfLength(1));
        Assert.That(items, Has.Items(Is.EqualTo(new LineItem(Item: "Iron", Quantity: 1))));
    }
}
