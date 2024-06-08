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
public class ListOrdersTest
{
    private InventoryRepository inventoryRepository;
    private OrderRepository orderRepository;

    public ListOrdersTest(ITestOutputHelper output)
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
        orderRepository.ReplaceAll(new List<Order> {
            new Order(Item: "Furnace", Quantity: 1, Components: new List<LineItem>() {
                new LineItem(Item: "Iron", Quantity: 1)

            })
        });

        ListOrdersQueryHandler query = new ListOrdersQueryHandler(orderRepository);
        List<OrderDto> items = query.Handle(new ListOrdersQuery());
        Assert.That(items, Is.OfLength(1));
        Assert.That(items, Has.Items(BasekeeperMatchers.EqualToOrderDto(new OrderDto(Item: "Furnace", Quantity: 1, new List<LineItemDto>() {
            new LineItemDto(Item: "Iron", Quantity: 1)
        }))));
    }

    [Fact]
    public void OrdersFileIsEmpty()
    {
        // orderRepository.ReplaceAll(new List<Order> {
        //     new Order(Item: "Furnace", Quantity: 1, Components: new List<LineItem>() {
        //         new LineItem(Item: "Iron", Quantity: 1)

        //     })
        // });

        File.WriteAllText("orders.yaml", "");
        //   using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "WriteLines.txt"), true))
        //         {
        //             outputFile.WriteLine("Fourth Line");
        //         }

        ListOrdersQueryHandler query = new ListOrdersQueryHandler(orderRepository);
        List<OrderDto> items = query.Handle(new ListOrdersQuery());
        Assert.That(items, Is.OfLength(0));
        // Assert.That(items, Has.Items(BasekeeperMatchers.EqualToOrderDto(new OrderDto(Item: "Furnace", Quantity: 1, new List<LineItemDto>() {
        //     new LineItemDto(Item: "Iron", Quantity: 1)
        // }))));
    }
}
