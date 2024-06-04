using Basekeeper.Command;
using Basekeeper.Entity;
using Basekeeper.Matcher;
using Basekeeper.Repository;
using Basekeeper.Repository.Yaml;
using NHamcrest.Core;
using TelemRec;
using Xunit.Abstractions;

namespace Basekeeper.Tests;

//https://stackoverflow.com/questions/1408175/execute-unit-tests-serially-rather-than-in-parallel
[Collection("Sequential")]
public class CreateOrderTest
{
    private OrderRepository orderRepository;
    private RecipeRepository recipeRepostory;

    public CreateOrderTest(ITestOutputHelper output)
    {
        XunitLogger.Init(output);
        orderRepository = new YamlOrderRepository();
        recipeRepostory = new YamlRecipeRepository();
        orderRepository.Reset();
    }

    [Fact]
    public void SavesTheItem()
    {
        recipeRepostory.Create(new Recipe(Product: "Furnace", Ingredients: new List<LineItem>() {
            new LineItem(Item: "Iron", Quantity: 30),
            new LineItem(Item: "Copper", Quantity: 10)
        }));

        WhenItemOrdered(new CreateOrderCommand(Item: "Furnace", Quantity: 1));

        var items = orderRepository.All();
        Assert.That(items, Is.OfLength(1));
        Assert.That(items, Has.Items(BasekeeperMatchers.EqualToOrder(new Order(Item: "Furnace", Quantity: 1, Components: new List<LineItem>() {
            new LineItem(Item: "Iron", Quantity: 30),
            new LineItem(Item: "Copper", Quantity: 10)
        }))));
    }

    [Fact]
    public void IngredientsMultipledByQuantity()
    {
        recipeRepostory.Create(new Recipe(Product: "Battery", Ingredients: new List<LineItem>() {
            new LineItem(Item: "Gold", Quantity: 20),
            new LineItem(Item: "Copper", Quantity: 20),
            new LineItem(Item: "Steel", Quantity: 20)
        }));

        WhenItemOrdered(new CreateOrderCommand(Item: "Battery", Quantity: 2));

        var items = orderRepository.All();
        Assert.That(items, Is.OfLength(1));
        Assert.That(items, Has.Items(BasekeeperMatchers.EqualToOrder(new Order(Item: "Battery", Quantity: 2, Components: new List<LineItem>() {
            new LineItem(Item: "Gold", Quantity: 40),
            new LineItem(Item: "Copper", Quantity: 40),
            new LineItem(Item: "Steel", Quantity: 40)
        }))));
    }

    [Fact]
    public void PreservesItems()
    {
        orderRepository.Create(new Order(Item: "Iron", Quantity: 1, Components: new List<LineItem>()));

        WhenItemOrdered(new CreateOrderCommand(Item: "Lead", Quantity: 2));

        var items = orderRepository.All();
        Assert.That(items, Is.OfLength(2));
        Assert.That(items, Has.Item(BasekeeperMatchers.EqualToOrder(new Order(Item: "Iron", Quantity: 1, Components: new List<LineItem>()))));
        Assert.That(items, Has.Item(BasekeeperMatchers.EqualToOrder(new Order(Item: "Lead", Quantity: 2, Components: new List<LineItem>()))));
    }

    void WhenItemOrdered(CreateOrderCommand command)
    {
        var handler = new CreateOrderCommandHandler(orderRepository, recipeRepostory);
        handler.Handle(command);
    }
}
