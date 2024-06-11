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
public class DeleteInventoryTest
{
    private InventoryRepository inventoryRepository;
    private OrderRepository orderRepository;

    public DeleteInventoryTest(ITestOutputHelper output)
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

        DeleteInventoryCommand command = new DeleteInventoryCommand(Item: "Iron");

        DeleteInventoryCommandHandler handler = new DeleteInventoryCommandHandler(inventoryRepository);
        handler.Handle(command);

        List<LineItem> items = inventoryRepository.All();
        Assert.That(items, Is.OfLength(0));
        // Assert.That(items, Has.Items(BasekeeperMatchers.EqualToLineItem(new LineItem(Item: "Iron", Quantity: 1))));
    }
}
