using Basekeeper.Entity;
using Basekeeper.Tests;

namespace Basekeeper.Matcher;

public class BasekeeperMatchers
{
    public static OrderMatcher EqualToOrder(Order expected)
    {
        return new OrderMatcher(expected);
    }
}