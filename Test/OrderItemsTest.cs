using Basekeeper.Command;
using Basekeeper.Entity;
using Basekeeper.Matcher;
using Basekeeper.Repository;
using Basekeeper.Repository.Yaml;
using TelemRec;
using Xunit.Abstractions;

namespace Basekeeper.Tests;

//https://stackoverflow.com/questions/1408175/execute-unit-tests-serially-rather-than-in-parallel
[Collection("Sequential")]
public class OrderItemsTest
{
    private OrderRepository requisitionRepository;

    public OrderItemsTest(ITestOutputHelper output)
    {
        XunitLogger.Init(output);
        requisitionRepository = new YamlOrderRepository();
        requisitionRepository.Reset();
    }

    [Fact]
    public void Test1()
    {
        var command = new OrderItemsCommand(new List<Order> { new Order(Item: "actual", Quantity: 1, Components: new List<LineItem>()) });
        var handler = new OrderItemsCommandHandler(requisitionRepository);
        handler.Handle(command);

        // ListInventoryQueryHandler query = new ListInventoryQueryHandler(inventoryRepository);
        // List<LineItem> items = query.Handle(new ListInventoryQuery());
        var items = requisitionRepository.All();
        Assert.That(items, Is.OfLength(1));
        Assert.That(items, Has.Items(BasekeeperMatchers.EqualToOrder(new Order(Item: "actual", Quantity: 1, Components: new List<LineItem>()))));

        // Assert.That(new int[] { 1, 2, 3 }, Has.Items(Is.EqualTo(2), Is.EqualTo(3), Is.EqualTo(4)));
        // Assert.That(items[0], BasekeeperMatchers.EqualToOrder(new Order(Item: "expected", Quantity: 1, Components: new List<LineItem>())));
    }
}
