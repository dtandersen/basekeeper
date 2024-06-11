using Basekeeper.Command;
using Basekeeper.Entity;
using Basekeeper.Matcher;
using Basekeeper.Repository;
using Basekeeper.Repository.Yaml;
using NHamcrest.Core;
using TelemRec;
using Xunit.Abstractions;

namespace Basekeeper.Tests;

[Collection("Sequential")]
public class CreateInventoryTest
{
    private InventoryRepository inventoryRepository;

    public CreateInventoryTest(ITestOutputHelper output)
    {
        XunitLogger.Init(output);
        inventoryRepository = new YamlInventoryRepository();
        inventoryRepository.Reset();
    }

    [Fact]
    public void SavesTheItem()
    {
        WhenInventoryCreated(new CreateInventoryCommand(Item: "Iron", Quantity: 1));

        var items = inventoryRepository.All();
        Assert.That(items, Is.OfLength(1));
        Assert.That(items, Has.Items(BasekeeperMatchers.EqualToLineItem(new LineItem(Item: "Iron", Quantity: 1))));
    }


    void WhenInventoryCreated(CreateInventoryCommand command)
    {
        var handler = new CreateInventoryCommandHandler(inventoryRepository);
        handler.Handle(command);
    }
}
