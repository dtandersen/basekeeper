using Basekeeper.Command;
using Basekeeper.Entity;
using Basekeeper.Repository;
using Basekeeper.Repository.Yaml;

namespace Basekeeper.Tests;

//https://stackoverflow.com/questions/1408175/execute-unit-tests-serially-rather-than-in-parallel
[Collection("Sequential")]
public class ListOrdersTest
{
    private InventoryRepository inventoryRepository;
    private OrderRepository orderRepository;

    public ListOrdersTest()
    {
        inventoryRepository = new YamlInventoryRepository();
        orderRepository = new YamlOrderRepository();

        inventoryRepository.Reset();
        orderRepository.Reset();
    }

    [Fact]
    public void Test1()
    {
        orderRepository.ReplaceAll(new List<Order> {
            new Order(Item: "Iron", Quantity: 1, Components: new List<LineItem>())
        });

        ListOrdersQueryHandler query = new ListOrdersQueryHandler(orderRepository);
        List<OrderDto> items = query.Handle(new ListOrdersQuery());
        Assert.That(items, Is.OfLength(1));
        Assert.That(items, Has.Items(Is.EqualTo(new OrderDto(Item: "Iron", Quantity: 1))));
    }
}
