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
public class DeleteOrderTest
{
    private OrderRepository orderRepository;

    public DeleteOrderTest(ITestOutputHelper output)
    {
        XunitLogger.Init(output);
        orderRepository = new YamlOrderRepository();

        orderRepository.Reset();
    }

    [Fact]
    public void Test1()
    {
        orderRepository.Create(new Order(Item: "Iron", Quantity: 1, Components: new List<LineItem> {
            new LineItem(Item: "Iron", Quantity: 1)
        }));

        DeleteOrderCommand command = new DeleteOrderCommand(Item: "Iron");

        DeleteOrderCommandHandler handler = new DeleteOrderCommandHandler(orderRepository);
        handler.Handle(command);

        var items = orderRepository.All();
        Assert.That(items, Is.OfLength(0));
    }
}
