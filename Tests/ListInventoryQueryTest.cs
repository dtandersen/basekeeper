using Core;

namespace Basekeeper.Tests;

public class ListInventoryQueryTest
{
    [Fact]
    public void Test1()
    {
        ListInventoryQuery q = new ListInventoryQuery(new YamlInventoryRepository());
        List<LineItem> z = q.Execute();
        Assert.That(z, Has.Items(Is.EqualTo(new LineItem(Item: "Iron", Quantity: 1))));
    }
}