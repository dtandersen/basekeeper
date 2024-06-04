using Basekeeper.Command;
using Basekeeper.Entity;
using Basekeeper.Repository;
using Basekeeper.Repository.Yaml;
using NHamcrest.Core;
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
        Assert.That(items, Has.Items(new OrderMatcher(new Order(Product: "Iron", Quantity: 1, Ingredients: new List<LineItem>()))));
    }

    [Fact]
    public void PreservesItems()
    {
        orderRepository.ReplaceAll(new List<Order> { new Order(Product: "Iron", Quantity: 1, Ingredients: new List<LineItem>()) });

        var command = new OrderItemCommand(Item: "Lead", Quantity: 2);
        var handler = new OrderItemCommandHandler(orderRepository);
        handler.Handle(command);

        // Is.EqualTo("bob").Matches("bob");

        var items = orderRepository.All();
        Assert.That(items, Is.OfLength(2));
        Assert.That(items, Has.Item(new OrderMatcher(new Order(Product: "Iron", Quantity: 1, Ingredients: new List<LineItem>()))));
        Assert.That(items, Has.Item(new OrderMatcher(new Order(Product: "Lead", Quantity: 2, Ingredients: new List<LineItem>()))));
    }
}

public class OrderMatcher : Matcher<Order>
{
    private readonly Order expected;

    public OrderMatcher(Order expected)
    {
        this.expected = expected;
    }

    public override void DescribeTo(IDescription description)
    {
        description.AppendText($"Order(Product: {expected.Product}, Quantity: {expected.Quantity})");
    }

    public override bool Matches(Order actual)
    {
        List<IMatcher<LineItem>> matchers = expected.Ingredients.Select(x => Is.EqualTo(x)).ToList();
        // Contains.
        // return true;
        return actual.Product == expected.Product
        && actual.Quantity == expected.Quantity
        && actual.Ingredients.Count == expected.Ingredients.Count
        && Has.Items(matchers.ToArray()).Matches(actual.Ingredients);
    }
}
