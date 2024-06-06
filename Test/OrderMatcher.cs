using Basekeeper.Entity;
using NHamcrest.Core;

namespace Basekeeper.Matcher;

public class OrderMatcher : Matcher<Order>, IMatcher<Order>
{
    private readonly Order expected;

    public OrderMatcher(Order expected)
    {
        this.expected = expected;
    }

    public override void DescribeTo(IDescription description)
    {
        description.AppendText(expected.ToString());
    }

    public override bool Matches(Order actual)
    {
        List<IMatcher<LineItem>> matchers = expected.Components.Select(x => Is.EqualTo(x)).ToList();

        return actual.Item == expected.Item
        && actual.Quantity == expected.Quantity
        && actual.Components.Count == expected.Components.Count
        && Has.Items(matchers.ToArray()).Matches(actual.Components);
    }
}
