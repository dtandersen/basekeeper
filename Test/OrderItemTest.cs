using Basekeeper.Command;
using Basekeeper.Entity;
using Basekeeper.Repository;
using Basekeeper.Repository.Yaml;
using TelemRec;
using Xunit.Abstractions;

namespace Basekeeper.Tests;

//https://stackoverflow.com/questions/1408175/execute-unit-tests-serially-rather-than-in-parallel
[Collection("Sequential")]
public class OrderItemTest
{
    private OrderRepository orderRepository;

    public OrderItemTest(ITestOutputHelper output)
    {
        XunitLogger.Init(output);
        orderRepository = new YamlOrderRepository();
        orderRepository.Reset();
    }

    [Fact]
    public void SavesTheItem()
    {
        var command = new OrderItemCommand(Item: "Iron", Quantity: 1);
        var handler = new OrderItemCommandHandler(orderRepository);
        handler.Handle(command);

        var items = orderRepository.All();
        Assert.That(items, Is.OfLength(1));
        Assert.That(items, Has.Items(Is.EqualTo(new LineItem(Item: "Iron", Quantity: 1))));
    }

    [Fact]
    public void PreservesItems()
    {
        orderRepository.Save(new List<LineItem> { new LineItem(Item: "Iron", Quantity: 1) });

        var command = new OrderItemCommand(Item: "Lead", Quantity: 2);
        var handler = new OrderItemCommandHandler(orderRepository);
        handler.Handle(command);

        var items = orderRepository.All();
        Assert.That(items, Is.OfLength(2));
        Assert.That(items, Has.Items(Is.EqualTo(new LineItem(Item: "Iron", Quantity: 1))));
        Assert.That(items, Has.Items(Is.EqualTo(new LineItem(Item: "Lead", Quantity: 2))));
    }
}
